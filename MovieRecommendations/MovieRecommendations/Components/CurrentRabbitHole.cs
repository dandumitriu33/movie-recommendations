using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            List<Movie> currentRabbitHole = new List<Movie>();
            // get the currently connected user email
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value.ToString();

            // based on email, get the latest movie the user watched
            History latestWatchedHistoryItem = _repository.GetLatestFromHistory(userEmail);
            Movie latestWatchedMovie = _repository.GetMovieByMovieId(latestWatchedHistoryItem.MovieId);

            // add the Rabbit Hole entry point - the last movie in the History of the user
            currentRabbitHole.Add(latestWatchedMovie);

            // while there are next movies, always get the strongest nextMovie for each and add it to the Rabbit Hole
            List<NextMovie> nextMovies = new List<NextMovie>();
            // based on the latest movie, get the list of nextMovies
            nextMovies = _repository.GetNextMoviesForMovieById(latestWatchedMovie.Id);
            int counter = 0;
            while (nextMovies.Count() > 0 && counter < 9)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(nextMovies[0].NextMovieId);
                currentRabbitHole.Add(tempMovie);
                nextMovies.Clear();
                nextMovies = _repository.GetNextMoviesForMovieById(tempMovie.Id);
                counter++;
            }

            return View(currentRabbitHole);
        }
    }
}
