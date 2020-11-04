using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Repositories;

namespace MovieRecommendations.Components
{
    public class AllLatestHighestRated : ViewComponent
    {
        private readonly IRepository _repository;

        public AllLatestHighestRated(IRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMovies();
            // sort by release yar and then by rating descending
            List<Movie> allMoviesFromDbSorted = allMoviesFromDb.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).ToList();
            List<MovieViewModel> allMovies = new List<MovieViewModel>();
            foreach (var movie in allMoviesFromDbSorted)
            {
                MovieViewModel tempMovieViewModel = new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    LengthInMinutes = movie.LengthInMinutes,
                    ReleaseYear = movie.ReleaseYear,
                    Rating = movie.Rating,
                    MainGenre = movie.MainGenre,
                    SubGenre1 = movie.SubGenre1,
                    SubGenre2 = movie.SubGenre2
                };
                allMovies.Add(tempMovieViewModel);
            }
            return View(allMovies);
        }
    }
}
