using System;
using System.Collections.Generic;
using System.Linq;
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

        public Movie GetMovieByMovieId(int movieId)
        {
            Movie movie = _context.Movies.Where(m => m.Id == movieId).FirstOrDefault();

            return movie;
        }

        public List<History> GetFullHistory(string email)
        {
            var fullHistory = _context.Histories.Where(h => h.Email == email).OrderByDescending(h => h.DateAdded).ToList();
            return fullHistory;
        }

        public void AddToHistory(string email, int movieId)
        {
            History newHistoryItem = new History
            {
                Email = email,
                MovieId = movieId,
                DateAdded = DateTime.UtcNow
            };
            _context.Histories.Add(newHistoryItem);
            _context.SaveChanges();
        }
    }
}
