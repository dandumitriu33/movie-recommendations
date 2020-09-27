using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDataAccessLibrary.Models
{
    public class SQLRepository : IRepository
    {
        private readonly MoviesContext _context;

        public SQLRepository(MoviesContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.Where(m => m.Rating > 0).OrderByDescending(m => m.ReleaseYear).ThenBy(m => m.Rating);
        }

        public IEnumerable<Movie> GetAllMoviesTop20()
        {
            return _context.Movies.Where(m => m.Rating > 6.5).OrderByDescending(m => m.ReleaseYear).ThenBy(m => m.Rating).Take(20);
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

        public IEnumerable<Movie> GetDistanceRecommendation(string mainGenre, double rating, int limit, int offset)
        {
            var recommendedMovies = _context.Movies.Where(m => m.MainGenre == mainGenre && m.Rating > rating - 2).OrderByDescending(m => m.ReleaseYear).ThenBy(m => m.Rating).Skip(offset).Take(limit);

            return recommendedMovies;
        }

        public UserLikedMovie GetCommunityLikedMovieById(int movieId)
        {
            UserLikedMovie movieFromDb = _context.CommunityLikes.Where(m => m.MovieId == movieId).FirstOrDefault();
            return movieFromDb;
        }

        public void IncrementCommunityLikedMovieScore(int movieId)
        {
            var result = _context.CommunityLikes.Where(m => m.MovieId == movieId).FirstOrDefault();

            if (result != null)
            {
                result.Score = result.Score + 1;
                _context.SaveChanges();
            }
        }

        public void AddToCommunityLikes(int movieId)
        {
            UserLikedMovie newUserLikedMovie = new UserLikedMovie
            {
                MovieId = movieId,
                Score = 1
            };
            _context.CommunityLikes.Add(newUserLikedMovie);
            _context.SaveChanges();
        }

        public IEnumerable<UserLikedMovie> GetCommunityTop(int limit, int offset)
        {
            var communityTop = _context.CommunityLikes.OrderByDescending(m => m.Score).Skip(offset).Take(limit);
            return communityTop;
        }

        public IEnumerable<UserLikedMovie> GetAllCommunityLikes()
        {
            var allCommunityLikes = _context.CommunityLikes.OrderByDescending(m => m.Score);
            return allCommunityLikes;
        }

        public History GetLatestFromHistory(string userEmail)
        {
            return _context.Histories.Where(h => h.Email == userEmail).OrderByDescending(h => h.DateAdded).FirstOrDefault();
        }
    }
}
