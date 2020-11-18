using MoviesDataAccessLibrary.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public List<Movie> GetAllMovies(int page, int cards)
        {
            return _context.Movies.Where(m => m.Rating > 0)
                .OrderByDescending(m => m.ReleaseYear)
                .ThenByDescending(m => m.Rating)
                .Skip((page-1)*cards)
                .Take(cards)
                .ToList();
        }

        public int GetInventoryTotal()
        {
            return _context.Movies.Count();
        }

        public List<Movie> GetTop20YearRating()
        {
            double minimumQualityRating = 6.5;
            return _context.Movies.Where(m => m.Rating > minimumQualityRating).OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating).Take(20).ToList();
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

        public async Task AddToHistory(string email, int movieId)
        {
            History newHistoryItem = new History
            {
                Email = email,
                MovieId = movieId,
                DateAdded = DateTime.UtcNow
            };
            await _context.Histories.AddAsync(newHistoryItem);
            await _context.SaveChangesAsync();
        }

        public History GetLatestFromHistory(string userEmail)
        {
            return _context.Histories.Where(h => h.Email == userEmail).OrderByDescending(h => h.DateAdded).FirstOrDefault();
        }

        public List<Movie> GetDistanceRecommendation(string mainGenre, double rating, int limit, int offset)
        {
            // considering the last movie watched rating, 
            // we can drop 2 points and still argue that the results will be watchable for the user
            int ratingDrop = 2;
            var recommendedMovies = _context.Movies.Where(m => m.MainGenre == mainGenre && m.Rating > rating - ratingDrop)
                                                   .OrderByDescending(m => m.ReleaseYear)
                                                   .ThenByDescending(m => m.Rating)
                                                   .Skip(offset)
                                                   .Take(limit)
                                                   .ToList();
            return recommendedMovies;
        }

        public UserLikedMovie GetCommunityLikedMovieById(int movieId)
        {
            UserLikedMovie movieFromDb = _context.CommunityLikes.Where(m => m.MovieId == movieId).FirstOrDefault();
            return movieFromDb;
        }

        public async Task IncrementCommunityLikedMovieScore(int movieId)
        {
            var result = await _context.CommunityLikes.Where(m => m.MovieId == movieId).FirstOrDefaultAsync();

            if (result != null)
            {
                result.Score = result.Score + 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddToCommunityLikes(int movieId)
        {
            UserLikedMovie newUserLikedMovie = new UserLikedMovie
            {
                MovieId = movieId,
                Score = 1
            };
            await _context.CommunityLikes.AddAsync(newUserLikedMovie);
            await _context.SaveChangesAsync();
        }

        public List<UserLikedMovie> GetCommunityTop(int limit, int offset)
        {
            return _context.CommunityLikes.OrderByDescending(m => m.Score).Skip(offset).Take(limit).ToList(); ;
        }

        public List<UserLikedMovie> GetAllCommunityLikes()
        {
            var allCommunityLikes = _context.CommunityLikes.OrderByDescending(m => m.Score).ToList();
            return allCommunityLikes;
        }

        public List<NextMovie> GetNextMoviesForMovieById(int currentMovie)
        {
            return _context.NextMovies.Where(m => m.CurrentMovieId == currentMovie).OrderByDescending(m => m.Score).ToList();
        }

        public List<NextMovie> GetNextMoviesForMovieByIdForSuggestions(int currentMovieId, int limit, int offset)
        {
            var rabbitHoleResults = _context.NextMovies.Where(m => m.CurrentMovieId == currentMovieId).OrderByDescending(m => m.Score).Skip(offset).Take(limit).ToList();
            return rabbitHoleResults;
        }

        public async Task AddNextMovie(int currentMovieId, int nextMovieId, int score)
        {
            NextMovie newEntry = new NextMovie
            {
                CurrentMovieId = currentMovieId,
                NextMovieId = nextMovieId,
                Score = score
            };
            await _context.AddAsync(newEntry);
            await _context.SaveChangesAsync();
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

        public async Task<Party> AddParty(Party party)
        {
            await _context.Parties.AddAsync(party);
            await _context.SaveChangesAsync();
            return party;
        }

        public async Task AddMemberToParty(PartyMember newPartyMember)
        {
            await _context.PartyMembers.AddAsync(newPartyMember);
            await _context.SaveChangesAsync();
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
