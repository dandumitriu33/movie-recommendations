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
            //string requestEmail = Request.Form["userEmail"];
            _repository.AddToHistory(userEmail, movieId);
            return RedirectToAction("details", "home", new { movieId = movieId });
        }

        public async Task<IActionResult> PopulateDb()
        {
            List<Movie> inMemoryTempDb = new List<Movie>();
            using (var reader = new StreamReader(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations\wwwroot\csv\MOCK_DATA.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Movie newMovieToAdd = new Movie
                    {
                        Title = values[0],
                        LengthInMinutes = Convert.ToInt32(values[1]),
                        ReleaseYear = Convert.ToInt32(values[2]),
                        Rating = Convert.ToDouble(values[3]),
                        MainGenre = processGenre(values[4]),
                        SubGenre1 = processGenre(values[5]),
                        SubGenre2 = processGenre(values[6]),
                    };
                    //Thread.Sleep(500);
                    inMemoryTempDb.Add(newMovieToAdd);
                                        
                }
            }
            //await _repository.Add(newMovieToAdd);
            foreach (var movie in inMemoryTempDb)
            {
                await _repository.Add(movie);
            }
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
