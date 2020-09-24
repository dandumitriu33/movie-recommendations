using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Components
{
    public class LatestPopular : ViewComponent
    {
        private readonly IRepository _repository;

        public LatestPopular(IRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMovies();
            AllMoviesViewModel allMovies = new AllMoviesViewModel
            {
                Movies = allMoviesFromDb.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).ToList()
            };
            return View(allMovies);
        }
    }
}
