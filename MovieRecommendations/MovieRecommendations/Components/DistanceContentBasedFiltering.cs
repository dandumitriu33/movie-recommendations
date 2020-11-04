using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Repositories;

namespace MovieRecommendations.Components
{
    public class DistanceContentBasedFiltering : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public DistanceContentBasedFiltering(IRepository repository,
                                             IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            List<Movie> userRecommendation = new List<Movie>();
            // get history last movie and search in that distance
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value.ToString();
            Console.WriteLine($"User email: {userEmail}");

            List<History> userHistory = _repository.GetFullHistory(userEmail);
            if (userHistory.Count == 0)
            {
                return View(userRecommendation);
            }
            Movie lastMovieWatched = _repository.GetMovieByMovieId(Convert.ToInt32(userHistory[0].MovieId));

            string mainGenre = lastMovieWatched.MainGenre;
            string subGenre1 = lastMovieWatched.SubGenre1;
            string subGenre2 = lastMovieWatched.SubGenre2;
            double rating = lastMovieWatched.Rating;

            // limit set to 20, offset 0 because we always want the newest here
            int limit = 20;
            int offset = 0;
            var initialRecommendation = _repository.GetDistanceRecommendation(mainGenre, rating, limit, offset);
            foreach (var movie in initialRecommendation)
            {
                Movie newMovie = new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Rating = movie.Rating,
                    ReleaseYear = movie.ReleaseYear,
                    LengthInMinutes = movie.LengthInMinutes,
                    MainGenre = movie.MainGenre,
                    SubGenre1 = movie.SubGenre1,
                    SubGenre2 = movie.SubGenre2
                };
                userRecommendation.Add(movie);
            }

            return View(userRecommendation);
        }
    }
}
