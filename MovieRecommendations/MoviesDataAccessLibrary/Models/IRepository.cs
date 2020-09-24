using System.Collections.Generic;

namespace MoviesDataAccessLibrary.Models
{
    public interface IRepository
    {
        Movie Add(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        public Movie GetMovieByMovieId(int movieId);
        public List<History> GetFullHistory(string email);
        public void AddToHistory(string email, int movieId);
    }
}