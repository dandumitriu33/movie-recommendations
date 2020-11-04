using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Components
{
    public class AllCommunityLikes : ViewComponent
    {
        private readonly IRepository _repository;

        public AllCommunityLikes(IRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            List<Movie> allCommunityBasedRecommendation = new List<Movie>();

            IEnumerable<UserLikedMovie> allCommunityLikesFromDb = _repository.GetAllCommunityLikes();
            if (allCommunityLikesFromDb.Count() == 0)
            {
                return View(allCommunityBasedRecommendation);
            }

            // transferring to memory because iterating over a lazy loaded query doesn't close the connection
            List<UserLikedMovie> allCommunityLikesMemory = new List<UserLikedMovie>();
            foreach (var entry in allCommunityLikesFromDb)
            {
                UserLikedMovie newEntry = new UserLikedMovie
                {
                    Id = entry.Id,
                    MovieId = entry.MovieId,
                    Score = entry.Score
                };
                allCommunityLikesMemory.Add(newEntry);
            }

            foreach (var likedMovie in allCommunityLikesMemory)
            {
                Movie newMovie = _repository.GetMovieByMovieId(likedMovie.MovieId);
                allCommunityBasedRecommendation.Add(newMovie);
            }
            return View(allCommunityBasedRecommendation);
        }
    }
}
