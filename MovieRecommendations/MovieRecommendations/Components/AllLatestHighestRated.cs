using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public IViewComponentResult Invoke(int page, int cards)
        {
            List<Movie> allMoviesFromDb = _repository.GetAllMovies(page, cards);            
            List<MovieViewModel> allMovies = _mapper.Map<List<Movie>, List<MovieViewModel>>(allMoviesFromDb);
            return View(allMovies);
        }
    }
}
