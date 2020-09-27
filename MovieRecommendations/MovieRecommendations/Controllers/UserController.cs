using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Models;

namespace MovieRecommendations.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("user/userhistory/{email}")]
        public IActionResult UserHistory(string email)
        {
            List<History> fullHistory = _repository.GetFullHistory(email);

            List<MovieViewModel> moviesHistory = new List<MovieViewModel>();

            foreach (var entry in fullHistory)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(entry.MovieId);
                MovieViewModel newMovie = new MovieViewModel
                {
                    Id = tempMovie.Id,
                    Title = tempMovie.Title,
                    LengthInMinutes = tempMovie.LengthInMinutes,
                    ReleaseYear = tempMovie.ReleaseYear,
                    Rating = tempMovie.Rating,
                    MainGenre = tempMovie.MainGenre,
                    SubGenre1 = tempMovie.SubGenre1,
                    SubGenre2 = tempMovie.SubGenre2,
                    DateAddedToHistory = entry.DateAdded
                };
                moviesHistory.Add(newMovie);
            }
            List<MovieViewModel> orderedMovieHistory = moviesHistory.OrderByDescending(m => m.DateAddedToHistory).ToList();
            return View(orderedMovieHistory);
        }

        [HttpGet]
        public IActionResult Personalized(string userEmail)
        {
            // cookie management - reading offsets for db query
            int contentOffset = Convert.ToInt32(HttpContext.Request.Cookies["contentOffset"]);
            string newContentOffset = (contentOffset + 1).ToString();
            int communityOffset = Convert.ToInt32(HttpContext.Request.Cookies["communityOffset"]);
            string newCommunityOffset = (communityOffset + 1).ToString();
            
            List<Movie> result = new List<Movie>();
            ViewBag.Text = userEmail;

            // get the last movie from history
            History latestWatchedHistory = _repository.GetLatestFromHistory(userEmail);
            Movie latestWatchedMovie = _repository.GetMovieByMovieId(latestWatchedHistory.MovieId);

            // get 9 newest, similar rating movies
            List<Movie> historyBasedSuggestions = new List<Movie>();
            int limit = 9;
            var initialRecommendation = _repository.GetDistanceRecommendation(latestWatchedMovie.MainGenre, latestWatchedMovie.Rating, limit, contentOffset*limit);
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
                historyBasedSuggestions.Add(movie);
            }

            // get the community top pick
            List<Movie> communityBasedSuggestions = new List<Movie>();
            int communityLimit = 1;
            IEnumerable<UserLikedMovie> communityTopPicks = _repository.GetCommunityTop(communityLimit);
            // lazy loading doesn't close DB connection ? so transfer to memory
            List<UserLikedMovie> communityTopPicksMemory = new List<UserLikedMovie>();
            foreach (var pick in communityTopPicks)
            {
                communityTopPicksMemory.Add(pick);
            }
            // convert to movie
            foreach (var pick in communityTopPicksMemory)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(pick.MovieId);
                communityBasedSuggestions.Add(tempMovie);
            }

            // arranging the history and community suggestions in result 4-1-5
            for (int i = 0; i < 4; i++)
            {
                result.Add(historyBasedSuggestions[i]);
            }
            result.Add(communityBasedSuggestions[0]);
            for (int i = 4; i < 9; i++)
            {
                result.Add(historyBasedSuggestions[i]);
            }


            // cookie management - increasing offsets for next refresh
            HttpContext.Response.Cookies.Append("contentOffset", newContentOffset);
            HttpContext.Response.Cookies.Append("communityOffset", newCommunityOffset);

            return View(result);
        }
    }
}
