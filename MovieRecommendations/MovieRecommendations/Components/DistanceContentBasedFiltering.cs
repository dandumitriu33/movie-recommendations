using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MovieRecommendations.Components
{
    public class DistanceContentBasedFiltering : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DistanceContentBasedFiltering(IRepository repository,
                                             IHttpContextAccessor httpContextAccessor,
                                             IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            // get history last movie and search in that distance
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value.ToString();

            List<History> userHistory = _repository.GetFullHistory(userEmail);
            if (userHistory.Count == 0)
            {
                return View();
            }
            Movie lastMovieWatched = _repository.GetMovieByMovieId(Convert.ToInt32(userHistory[0].MovieId));

            string mainGenre = lastMovieWatched.MainGenre;
            double rating = lastMovieWatched.Rating;

            // limit set to 20, offset 0 because we always want the newest here
            int limit = 20;
            int offset = 0;
            var distanceRecommendationFromDb = _repository.GetDistanceRecommendation(mainGenre, rating, limit, offset).ToList();
            List<MovieViewModel> distanceRecommendation = _mapper.Map<List<Movie>, List<MovieViewModel>>(distanceRecommendationFromDb);

            return View(distanceRecommendation);
        }
    }
}
