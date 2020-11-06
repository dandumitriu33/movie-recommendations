using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MovieRecommendations.Components
{
    public class AllLatestHighestRated : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AllLatestHighestRated(IRepository repository,
                                     IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMovies();
            // sort by release year and then by rating descending
            List<Movie> allMoviesFromDbSorted = allMoviesFromDb.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).ToList();
            List<MovieViewModel> allMovies = _mapper.Map<List<Movie>, List<MovieViewModel>>(allMoviesFromDbSorted);
            return View(allMovies);
        }
    }
}
