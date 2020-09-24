using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        
    }
}
