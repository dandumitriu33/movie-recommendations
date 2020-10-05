using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRecommendations.Models;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Models;

namespace MovieRecommendations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public HomeController(ILogger<HomeController> logger,
                              IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("contentOffset", "0");
            HttpContext.Response.Cookies.Append("communityOffset", "0");
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
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(movie);
                return RedirectToAction("AddMovie", "Home");
            }
            return View(movie);
        }

        [HttpGet]
        [Route("home/details/{movieId}")]
        public IActionResult Details(int movieId)
        {
            Movie movie = _repository.GetMovieByMovieId(movieId);
            return View(movie);
        }

        [HttpPost]
        public IActionResult AddToHistory(string userEmail, int movieId)
        {
            // first adding to CommunityLikes
            UserLikedMovie databaseUserLikedMovie = _repository.GetCommunityLikedMovieById(movieId);
            if (databaseUserLikedMovie != null)
            {
                _repository.IncrementCommunityLikedMovieScore(movieId);
            }
            else
            {
                _repository.AddToCommunityLikes(movieId);
            }

            // also adding the entry as a next movie to the previous movie in the user's history
            History previousMovieHistoryModel = _repository.GetLatestFromHistory(userEmail);
            int previousMovieId = previousMovieHistoryModel.MovieId;

            List<NextMovie> nextMoviesFromDb = _repository.GetNextMoviesForMovieById(previousMovieId);

            NextMovie entry = nextMoviesFromDb.Where(m => m.NextMovieId == movieId).FirstOrDefault();

            if (entry == null)
            {
                _repository.AddNextMovie(previousMovieId, movieId, 1);
            }
            else
            {
                _repository.UpdateNextMovieScore(previousMovieId, movieId, entry.Score + 1);
            }

            // lastly adding to user history
            //string requestEmail = Request.Form["userEmail"];
            _repository.AddToHistory(userEmail, movieId);



            return RedirectToAction("details", "home", new { movieId = movieId });
        }

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
