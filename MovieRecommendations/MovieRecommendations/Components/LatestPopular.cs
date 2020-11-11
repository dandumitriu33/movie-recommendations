using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;

namespace MovieRecommendations.Components
{
    public class LatestPopular : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public LatestPopular(IRepository repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            List<Movie> top20FromDb = _repository.GetTop20YearRating();
            var top20MovieViewModel = _mapper.Map<List<Movie>, List<MovieViewModel>>(top20FromDb);
            return View(top20MovieViewModel);
        }
    }
}
