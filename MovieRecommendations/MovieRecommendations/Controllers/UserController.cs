﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.Models;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;

namespace MovieRecommendations.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPersonalizedRecommendationsBuilder _recommendationsBuilder;

        public UserController(IRepository repository,
                              IMapper mapper,
                              IPersonalizedRecommendationsBuilder recommendationsBuilder)
        {
            _repository = repository;
            _mapper = mapper;
            _recommendationsBuilder = recommendationsBuilder;
        }
        [HttpGet]
        [Route("user/userhistory/{email}")]
        public IActionResult UserHistory(string email)
        {
            List<History> fullHistory = _repository.GetFullHistory(email);

            List<AcceptableWatchHistoryMovieModel> moviesHistory = new List<AcceptableWatchHistoryMovieModel>();

            foreach (var entry in fullHistory)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(entry.MovieId);
                AcceptableWatchHistoryMovieModel newMovie = new AcceptableWatchHistoryMovieModel
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
            List<AcceptableWatchHistoryMovieModel> orderedMovieHistory = moviesHistory.OrderByDescending(m => m.DateAddedToHistory).ToList();
            return View(orderedMovieHistory);
        }

        [HttpGet]
        public IActionResult Personalized(string userEmail)
        {            
            ViewBag.Text = userEmail;

            // get the last movie from history
            History latestWatchedHistory = _repository.GetLatestFromHistory(userEmail);
            Movie latestWatchedMovie = _repository.GetMovieByMovieId(latestWatchedHistory.MovieId);

            // STEP 1 - get the rabbitHole list - order matters as step 3 depends on 1's result
            List<Movie> rabbitHoleSuggestions = GetRabbitHoleList(latestWatchedHistory);

            // Step 2 - get the community top pick - order matters as step 3 depends on 2's result as well
            List<Movie> communityBasedSuggestions = GetCommunityList();

            // STEP 3 - get 8 or 9 newest, similar rating movies, if rabbit hole has entries or not
            int contentLimit = 10;
            if (rabbitHoleSuggestions.Count() > 0)
            {
                contentLimit = 9;
            }
            if (communityBasedSuggestions.Count() > 0)
            {
                contentLimit = 8;
            }
            List<Movie> historyBasedSuggestions = GetContentList(latestWatchedMovie, contentLimit);

            // arranging the rabbitHole, community and history(content) suggestions in result 1-1-8
            List<Movie> result = _recommendationsBuilder.Build(rabbitHoleSuggestions, communityBasedSuggestions, historyBasedSuggestions);

            // map result to MovieViewModel
            List<MovieViewModel> resultMovieViewModel = _mapper.Map<List<Movie>, List<MovieViewModel>>(result);

            return View(resultMovieViewModel);
        }

        /// <summary>
        /// Gets content based recommendations from the database based on the newest movie in the user's history.
        /// </summary>
        /// <param name="latestWatchedMovie"></param>
        /// <param name="contentLimit"></param>
        /// <returns></returns>
        private List<Movie> GetContentList(Movie latestWatchedMovie, int contentLimit)
        {
            List<Movie> output = new List<Movie>();
            int contentOffset = Convert.ToInt32(HttpContext.Request.Cookies["contentOffset"]);
            string newContentOffset = (contentOffset + 1).ToString();
            List<Movie> initialRecommendation = _repository.GetDistanceRecommendation(latestWatchedMovie.MainGenre, latestWatchedMovie.Rating, contentLimit, contentOffset * contentLimit).ToList();
            if (initialRecommendation.Count() < contentLimit)
            {
                contentOffset = 0;
                newContentOffset = "1";
                initialRecommendation = _repository.GetDistanceRecommendation(latestWatchedMovie.MainGenre, latestWatchedMovie.Rating, contentLimit, contentOffset * contentLimit).ToList();
            }
            foreach (Movie movie in initialRecommendation)
            {
                output.Add(movie);
            }
            HttpContext.Response.Cookies.Append("contentOffset", newContentOffset);
            return output;
        }

        /// <summary>
        /// Gets community based recommendations from the database based on the entire community views.
        /// </summary>
        /// <returns></returns>
        private List<Movie> GetCommunityList()
        {
            int communityOffset = Convert.ToInt32(HttpContext.Request.Cookies["communityOffset"]);
            string newCommunityOffset = (communityOffset + 1).ToString();
            List<Movie> output = new List<Movie>();
            int communityLimit = 1;
            List<UserLikedMovie> communityTopPicks = _repository.GetCommunityTop(communityLimit, communityOffset * communityLimit);
            if (communityTopPicks.Count() == 0)
            {
                communityOffset = 0;
                newCommunityOffset = "1";
                communityTopPicks = _repository.GetCommunityTop(communityLimit, communityOffset * communityLimit);
            }
            // lazy loading doesn't close DB connection so transfer to memory
            List<UserLikedMovie> communityTopPicksMemory = communityTopPicks;
            // convert to movie
            foreach (var pick in communityTopPicksMemory)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(pick.MovieId);
                output.Add(tempMovie);
            }
            HttpContext.Response.Cookies.Append("communityOffset", newCommunityOffset);
            return output;
        }

        /// <summary>
        /// Gets the list of movies that are NextMovies for that newest user history entry. These are the next steps in their rabbit holes.
        /// </summary>
        /// <param name="latestWatchedHistory"></param>
        /// <returns></returns>
        private List<Movie> GetRabbitHoleList(History latestWatchedHistory)
        {
            int rabbitHoleOffset = Convert.ToInt32(HttpContext.Request.Cookies["rabbitHoleOffset"]);
            string newRabbitHoleOffset = (rabbitHoleOffset + 1).ToString();
            List<Movie> output = new List<Movie>();
            int rabbitHoleLimit = 1;
            var rabbitHoleEntries = _repository.GetNextMoviesForMovieByIdForSuggestions(latestWatchedHistory.MovieId, rabbitHoleLimit, rabbitHoleOffset * rabbitHoleLimit);
            // lazy loading doesn't close DB connection ? so transfer to memory
            List<NextMovie> rabbitHoleEntriesMemory = new List<NextMovie>();
            if (rabbitHoleEntries.Count() == 0)
            {
                rabbitHoleOffset = 0;
                newRabbitHoleOffset = "1";
                rabbitHoleEntries = _repository.GetNextMoviesForMovieByIdForSuggestions(latestWatchedHistory.MovieId, rabbitHoleLimit, rabbitHoleOffset * rabbitHoleLimit);
            }
            if (rabbitHoleEntries.Count() > 0)
            {
                foreach (var entry in rabbitHoleEntries)
                {
                    rabbitHoleEntriesMemory.Add(entry);
                }
                // convert to movie
                foreach (var entry in rabbitHoleEntriesMemory)
                {
                    Movie tempMovie = _repository.GetMovieByMovieId(entry.NextMovieId);
                    output.Add(tempMovie);
                }
            }
            HttpContext.Response.Cookies.Append("rabbitHoleOffset", newRabbitHoleOffset);
            return output;
        }
    }
}
