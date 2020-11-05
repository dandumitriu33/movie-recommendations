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
    public class CommunityBasedFiltering : ViewComponent
    {
        private readonly IRepository _repository;

        public CommunityBasedFiltering(IRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            List<MovieViewModel> communityBasedRecommendation = new List<MovieViewModel>();

            // limit set to 20, offset is 0 as we always want to top
            int limit = 20;
            int offset = 0;
            var communityTop = _repository.GetCommunityTop(limit, offset);
            if (communityTop.Count() == 0)
            {
                return View(communityBasedRecommendation);
            }

            // transferring to memory because iterating over a lazy loaded query doesn't close the connection
            List<UserLikedMovie> communityTopMemory = new List<UserLikedMovie>();
            foreach (var entry in communityTop)
            {
                UserLikedMovie newEntry = new UserLikedMovie
                {
                    Id = entry.Id,
                    MovieId = entry.MovieId,
                    Score = entry.Score
                };
                communityTopMemory.Add(newEntry);
            }
            foreach (var likedMovie in communityTopMemory)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(likedMovie.MovieId);
                MovieViewModel newMovieViewModel = new MovieViewModel
                {
                    Id = tempMovie.Id,
                    Title = tempMovie.Title,
                    LengthInMinutes = tempMovie.LengthInMinutes,
                    ReleaseYear = tempMovie.ReleaseYear,
                    Rating = tempMovie.Rating,
                    MainGenre = tempMovie.MainGenre,
                    SubGenre1 = tempMovie.SubGenre1,
                    SubGenre2 = tempMovie.SubGenre2
                };
                communityBasedRecommendation.Add(newMovieViewModel);
            }
            return View(communityBasedRecommendation);
        }
    }
}
