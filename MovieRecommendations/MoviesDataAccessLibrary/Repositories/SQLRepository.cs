using MoviesDataAccessLibrary.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;

namespace MoviesDataAccessLibrary.Repositories
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

        //public IEnumerable<Movie> GetAllMoviesTop20()
        //{
        //    return _context.Movies.Where(m => m.Rating > 6.5).OrderByDescending(m => m.ReleaseYear).ThenBy(m => m.Rating).Take(20);
        //}

        public List<Movie> GetTop20YearRating()
        {
            return _context.Movies.Where(m => m.Rating > 6.5).OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).Take(20).ToList();
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
            var recommendedMovies = _context.Movies.Where(m => m.MainGenre == mainGenre && m.Rating > rating - 2).OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).Skip(offset).Take(limit);

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

        public List<NextMovie> GetNextMoviesForMovieById(int currentMovie)
        {
            return _context.NextMovies.Where(m => m.CurrentMovieId == currentMovie).OrderByDescending(m => m.Score).ToList();
        }

        public IEnumerable<NextMovie> GetNextMoviesForMovieByIdForSuggestions(int currentMovieId, int limit, int offset)
        {
            var rabbitHoleResults = _context.NextMovies.Where(m => m.CurrentMovieId == currentMovieId).OrderByDescending(m => m.Score).Skip(offset).Take(limit);
            return rabbitHoleResults;
        }

        public void AddNextMovie(int currentMovieId, int nextMovieId, int score)
        {
            NextMovie newEntry = new NextMovie
            {
                CurrentMovieId = currentMovieId,
                NextMovieId = nextMovieId,
                Score = score
            };
            _context.Add(newEntry);
            _context.SaveChanges();
        }

        public void UpdateNextMovieScore(int currentMovieId, int nextMovieId, int score)
        {
            // can be simplified with query by row Id, probably better if indexing is introduced
            NextMovie updateEntry = _context.NextMovies.Where(m => m.CurrentMovieId == currentMovieId && m.NextMovieId == nextMovieId).FirstOrDefault();

            if (updateEntry != null)
            {
                updateEntry.Score = score;
                _context.SaveChanges();
            }
        }

        public List<Party> GetUserParties(string userEmail)
        {
            List<PartyMember> userMemberships = _context.PartyMembers.Where(p => p.Email == userEmail).ToList();
            List<Party> userParties = new List<Party>();
            
            foreach (var entry in userMemberships)
            {
                Party partyFromDb = _context.Parties.Where(p => p.Id == entry.PartyId).First();
                userParties.Add(partyFromDb);
            }
            
            return userParties;
        }

        public Party GetPartyById(int partyId)
        {
            return _context.Parties.Where(p => p.Id == partyId).FirstOrDefault();
        }

        public void AddParty(Party party)
        {
            _context.Parties.Add(party);
            _context.SaveChanges();
        }

        public void AddMemberToParty(PartyMember newPartyMember)
        {
            _context.PartyMembers.Add(newPartyMember);
            _context.SaveChanges();
        }

        public PartyMember GetPartyMember(int partyId, string userEmail)
        {
            return _context.PartyMembers.Where(p => p.PartyId == partyId && p.Email == userEmail).FirstOrDefault();
        }

        public void RemoveMemberFromParty(PartyMember partyMember)
        {
            _context.PartyMembers.Remove(partyMember);
            _context.SaveChanges();
        }

        public void ResetChoicesForParty(int partyId)
        {
            _context.PartyChoices.RemoveRange(_context.PartyChoices.Where(c => c.PartyId == partyId));
            _context.SaveChanges();
        }

        public List<Movie> GetBatch( int newestId, int oldestId, int limit)
        {
            // the newest movie added to the DB will have the highest ID - incrementing
            // the oldest movie represents the last fetched movie chronologically
            List<Movie> batch = _context.Movies.Where(m => m.Id > newestId || m.Id < oldestId).OrderByDescending(m => m.Id).Take(limit).ToList();
            return batch;
        }

        public void AddChoice(PartyChoice choice)
        {
            PartyChoice choiceFromDb = _context.PartyChoices.Where(c => c.PartyId == choice.PartyId && c.MovieId == choice.MovieId).FirstOrDefault();
            if (choiceFromDb != null)
            {
                choiceFromDb.Score += choice.Score;
                _context.SaveChanges();
            }
            else
            {
                _context.PartyChoices.Add(choice);
                _context.SaveChanges();
            }
        }

        public int GetPartyCount(int partyId)
        {
            return _context.PartyMembers.Where(p => p.PartyId == partyId).Count();
        }

        public List<PartyChoice> GetMovieIdsForParty(int partyId, int count)
        {
            return _context.PartyChoices.Where(c => c.PartyId == partyId && c.Score == count).ToList();
        }

        public List<PartyMember> GetPartyMembersForParty(int partyId)
        {
            return _context.PartyMembers.Where(p => p.PartyId == partyId).ToList();
        }

        public List<GenreCount> GetGenreCount()
        {
            return _context.Movies.GroupBy(
                m => m.MainGenre,
                m => m.Title,
                (key, g) => new GenreCount { 
                                                GenreName = key, 
                                                Count = g.Count(), 
                                                Color = "Default" }
                ).ToList();
        }

        public List<CommunityGenreScore> GetCommunityGenresScore()
        {
            // not the greatest Linq query - improve later
            var result = from communityLike in _context.CommunityLikes
                         join movie in _context.Movies on communityLike.MovieId equals movie.Id
                         select new CommunityGenreScore { GenreName = movie.MainGenre, Score = communityLike.Score, Color = "Default" };
            return result.ToList();
        }

        public List<Movie> GetLatestHorrorMovies(int chartSize)
        {
            return _context.Movies.Where(m => m.MainGenre == "Horror").OrderByDescending(m => m.ReleaseYear).Take(chartSize).ToList();
        }
    }
}
