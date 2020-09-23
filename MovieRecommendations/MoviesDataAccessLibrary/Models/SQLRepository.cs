using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class SQLRepository : IRepository
    {
        private readonly MoviesContext _context;

        public SQLRepository(MoviesContext context)
        {
            _context = context;
        }

        public Movie Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies;
        }
    }
}
