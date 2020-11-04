using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Repositories;
using MovieRecommendations.ViewModels;

namespace MovieRecommendations.Components
{
    public class CurrentRabbitHole : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentRabbitHole(IRepository repository,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            List<MovieViewModel> currentRabbitHole = new List<MovieViewModel>();

            // get the currently connected user email
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value.ToString();

            // based on email, get the latest movie the user watched
            History latestWatchedHistoryItem = _repository.GetLatestFromHistory(userEmail);
            if (latestWatchedHistoryItem == null)
            {
                return View(currentRabbitHole);
            }
            Movie latestWatchedMovie = _repository.GetMovieByMovieId(latestWatchedHistoryItem.MovieId);

            // map and add the Rabbit Hole entry point - the last movie in the History of the user
            MovieViewModel latestWatchedMovieViewModel = new MovieViewModel
            {
                Id = latestWatchedMovie.Id,
                Title = latestWatchedMovie.Title,
                LengthInMinutes = latestWatchedMovie.LengthInMinutes,
                ReleaseYear = latestWatchedMovie.ReleaseYear,
                Rating = latestWatchedMovie.Rating,
                MainGenre = latestWatchedMovie.MainGenre,
                SubGenre1 = latestWatchedMovie.SubGenre1,
                SubGenre2 = latestWatchedMovie.SubGenre2
            };
            currentRabbitHole.Add(latestWatchedMovieViewModel);

            // while there are next movies, always get the strongest nextMovie for each and add it to the Rabbit Hole
            List<NextMovie> nextMovies = new List<NextMovie>();

            // based on the latest movie, get the list of nextMovies
            nextMovies = _repository.GetNextMoviesForMovieById(latestWatchedMovie.Id);
            int counter = 0;
            while (nextMovies.Count() > 0 && counter < 9)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(nextMovies[0].NextMovieId);
                MovieViewModel tempMovieViewModel = new MovieViewModel
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
                currentRabbitHole.Add(tempMovieViewModel);
                nextMovies.Clear();
                nextMovies = _repository.GetNextMoviesForMovieById(tempMovie.Id);
                counter++;
            }

            return View(currentRabbitHole);
        }
    }
}
