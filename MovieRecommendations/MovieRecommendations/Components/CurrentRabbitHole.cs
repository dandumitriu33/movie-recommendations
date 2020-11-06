using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MovieRecommendations.Components
{
    public class CurrentRabbitHole : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CurrentRabbitHole(IRepository repository,
                                 IHttpContextAccessor httpContextAccessor,
                                 IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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
            MovieViewModel latestWatchedMovieViewModel = _mapper.Map<Movie, MovieViewModel>(latestWatchedMovie);
            currentRabbitHole.Add(latestWatchedMovieViewModel);

            // while there are next movies, always get the strongest nextMovie for each and add it to the Rabbit Hole
            List<NextMovie> nextMovies = _repository.GetNextMoviesForMovieById(latestWatchedMovie.Id);

            // based on the latest movie, get the list of nextMovies
            int counter = 0;
            while (nextMovies.Count() > 0 && counter < 9)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(nextMovies[0].NextMovieId);
                MovieViewModel tempMovieViewModel = _mapper.Map<Movie, MovieViewModel>(tempMovie);
                currentRabbitHole.Add(tempMovieViewModel);
                nextMovies.Clear();
                nextMovies = _repository.GetNextMoviesForMovieById(tempMovie.Id);
                counter++;
            }

            return View(currentRabbitHole);
        }
    }
}
