using System.Collections.Generic;
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
        IEnumerable<Movie> GetDistanceRecommendation(string mainGenre, double rating, int limit, int offset);
        UserLikedMovie GetCommunityLikedMovieById(int movieId);
        void IncrementCommunityLikedMovieScore(int movieId);
        void AddToCommunityLikes(int movieId);
        IEnumerable<UserLikedMovie> GetCommunityTop(int limit, int offset);
        IEnumerable<UserLikedMovie> GetAllCommunityLikes();
        History GetLatestFromHistory(string userEmail);
        public List<NextMovie> GetNextMoviesForMovieById(int currentMovie);
        public void AddNextMovie(int previousMovieId, int nextMovieId, int score);
        public void UpdateNextMovieScore(int currentMovieId, int nextMovieId, int score);
    }
}