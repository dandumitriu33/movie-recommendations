using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesDataAccessLibrary.Models
{
    public interface IRepository
    {
        Task<Movie> Add(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        public Movie GetMovieByMovieId(int movieId);
        public List<History> GetFullHistory(string email);
        public void AddToHistory(string email, int movieId);
        IEnumerable<Movie> GetDistanceRecommendation(string mainGenre, double rating);
    }
}