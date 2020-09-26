﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDataAccessLibrary.Models
{
    public interface IRepository
    {
        Task<Movie> Add(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        IEnumerable<Movie> GetAllMoviesTop20();
        public Movie GetMovieByMovieId(int movieId);
        public List<History> GetFullHistory(string email);
        public void AddToHistory(string email, int movieId);
        IEnumerable<Movie> GetDistanceRecommendation(string mainGenre, double rating);
        UserLikedMovie GetCommunityLikedMovieById(int movieId);
        void IncrementCommunityLikedMovieScore(int movieId);
        void AddToCommunityLikes(int movieId);

        IEnumerable<UserLikedMovie> GetCommunityTop();
    }
}