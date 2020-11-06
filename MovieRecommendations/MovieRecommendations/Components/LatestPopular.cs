﻿using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<Movie> top20FromDb = _repository.GetAllMoviesTop20();

            // sort by release yar and then by rating descending
            List<Movie> top20FromDbSorted = top20FromDb.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).ToList();

            // using automapper to switch to View Models
            var top20MovieViewModel = _mapper.Map<List<Movie>, List<MovieViewModel>>(top20FromDbSorted);

            //List<MovieViewModel> top20MovieViewModel = new List<MovieViewModel>();
            //foreach (var movie in top20FromDbSorted)
            //{
            //    MovieViewModel tempMovieViewModel = new MovieViewModel
            //    {
            //        Id = movie.Id,
            //        Title = movie.Title,
            //        LengthInMinutes = movie.LengthInMinutes,
            //        ReleaseYear = movie.ReleaseYear,
            //        Rating = movie.Rating,
            //        MainGenre = movie.MainGenre,
            //        SubGenre1 = movie.SubGenre1,
            //        SubGenre2 = movie.SubGenre2
            //    };
            //    top20MovieViewModel.Add(tempMovieViewModel);
            //}
            return View(top20MovieViewModel);
        }
    }
}
