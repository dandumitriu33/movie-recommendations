using System.Collections.Generic;

namespace MoviesDataAccessLibrary.Models
{
    public interface IRepository
    {
        Movie Add(Movie movie);
        IEnumerable<Movie> GetAllMovies();
    }
}