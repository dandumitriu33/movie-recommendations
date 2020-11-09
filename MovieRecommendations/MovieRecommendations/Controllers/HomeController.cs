using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommendations.Models;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
                              IRepository repository,
                              IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("contentOffset", "0");
            HttpContext.Response.Cookies.Append("communityOffset", "0");
            HttpContext.Response.Cookies.Append("rabbitHoleOffset", "0");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AllMovies()
        {
            return View();
        }

        public IActionResult AllCommunityLikes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                Movie newEntry = _mapper.Map<MovieViewModel, Movie>(movie);
                _repository.Add(newEntry);
                return View();
            }
            return View(movie);
        }

        [HttpGet]
        [Route("home/details/{movieId}")]
        public IActionResult Details(int movieId)
        {
            Movie movie = _repository.GetMovieByMovieId(movieId);
            MovieViewModel movieViewModel = _mapper.Map<Movie, MovieViewModel>(movie);
            return View(movieViewModel);
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"The action you were trying to complete encountered an error. ErrorCode: {code}";
            return View("~/Views/Shared/HandleError.cshtml");
        }

        /// <summary>
        /// Process a movie after it has been watched > 90%. Add to CommunityLikes, NextMovies and User History.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessWatchedMovie(string userEmail, int movieId)
        {
            // first adding to CommunityLikes
            AddMovieToCommunityLikes(movieId);

            // also adding the entry as a next movie (in the NextMovies table) to the previous movie in the user's history
            AddMovieToNextMovies(userEmail, movieId);

            // lastly adding to user history
            _repository.AddToHistory(userEmail, movieId);

            return RedirectToAction("details", "home", new { movieId = movieId });
        }

        /// <summary>
        /// Utility page with the posters to have links for JavaScript. Simulating links on a CDN.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MoviePosters()
        {
            return View();
        }

        /// <summary>
        /// Adds the movie to the NextMovies table with a const score and if it already exists, it just increments the score.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="movieId"></param>
        private void AddMovieToNextMovies(string userEmail, int movieId)
        {
            const int scoreIncrementation = 1;
            History previousMovieHistoryModel = _repository.GetLatestFromHistory(userEmail);
            if (previousMovieHistoryModel != null)
            {
                int previousMovieId = previousMovieHistoryModel.MovieId;
                List<NextMovie> nextMoviesFromDb = _repository.GetNextMoviesForMovieById(previousMovieId);

                NextMovie entry = nextMoviesFromDb.Where(m => m.NextMovieId == movieId).FirstOrDefault();

                if (entry == null)
                {
                    _repository.AddNextMovie(previousMovieId, movieId, scoreIncrementation);
                }
                else
                {
                    _repository.UpdateNextMovieScore(previousMovieId, movieId, entry.Score + scoreIncrementation);
                }
            }
        }

        /// <summary>
        /// Adds a movie to the Community Likes and if it already exists it increments the score
        /// </summary>
        /// <param name="movieId"></param>
        private void AddMovieToCommunityLikes(int movieId)
        {
            UserLikedMovie databaseUserLikedMovie = _repository.GetCommunityLikedMovieById(movieId);
            if (databaseUserLikedMovie != null)
            {
                _repository.IncrementCommunityLikedMovieScore(movieId);
            }
            else
            {
                _repository.AddToCommunityLikes(movieId);
            }
        }

        /// <summary>
        /// Utility route and method to import data from CSV
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PopulateDb()
        {
            // commented out to not duplicate db data by mistake

            //List<Movie> inMemoryTempDb = new List<Movie>();
            //using (var reader = new StreamReader(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations\wwwroot\csv\MOCK_DATA.csv"))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');

            //        Movie newMovieToAdd = new Movie
            //        {
            //            Title = values[0],
            //            LengthInMinutes = Convert.ToInt32(values[1]),
            //            ReleaseYear = Convert.ToInt32(values[2]),
            //            Rating = Convert.ToDouble(values[3]),
            //            MainGenre = processGenre(values[4]),
            //            SubGenre1 = processGenre(values[5]),
            //            SubGenre2 = processGenre(values[6]),
            //        };
            //        //Thread.Sleep(500);
            //        inMemoryTempDb.Add(newMovieToAdd);
                                        
            //    }
            //}
            ////await _repository.Add(newMovieToAdd);
            //foreach (var movie in inMemoryTempDb)
            //{
            //    await _repository.Add(movie);
            //}
            return View();
        }

        /// <summary>
        /// Utility method to generate genre data during import from CSV
        /// </summary>
        /// <param name="rawGenre"></param>
        /// <returns></returns>
        private string processGenre(string rawGenre)
        {
            string processedGenre = rawGenre;
            string[] currentGenres = { "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Sci-Fi", "Western" };
            if (currentGenres.Contains(rawGenre) == false)
            {
                Random rand = new Random();
                int index = rand.Next(currentGenres.Length);
                processedGenre = currentGenres[index];
            }
            return processedGenre;
        }
    }
}
