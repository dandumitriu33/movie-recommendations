using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MovieRecommendations.Components
{
    public class AllCommunityLikes : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AllCommunityLikes(IRepository repository,
                                 IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            List<MovieViewModel> allCommunityBasedRecommendation = new List<MovieViewModel>();

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
                Movie tempMovie = _repository.GetMovieByMovieId(likedMovie.MovieId);
                MovieViewModel tempMovieViewModel = _mapper.Map<Movie, MovieViewModel>(tempMovie);
                allCommunityBasedRecommendation.Add(tempMovieViewModel);
            }
            return View(allCommunityBasedRecommendation);
        }
    }
}
