using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Entities;

namespace MoviesDataAccessLibrary.Repositories
{
    public interface IRepository
    {
        Task<Movie> Add(Movie movie);
        List<Movie> GetAllMovies(int page, int cards);
        int GetInventoryTotal();
        List<Movie> GetTop20YearRating();
        public Movie GetMovieByMovieId(int movieId);
        public List<History> GetFullHistory(string email);
        public Task AddToHistory(string email, int movieId);
        public History GetLatestFromHistory(string userEmail);
        public List<Movie> GetDistanceRecommendation(string mainGenre, double rating, int limit, int offset);
        public UserLikedMovie GetCommunityLikedMovieById(int movieId);
        public Task IncrementCommunityLikedMovieScore(int movieId);
        public Task AddToCommunityLikes(int movieId);
        public List<UserLikedMovie> GetCommunityTop(int limit, int offset);
        public List<UserLikedMovie> GetAllCommunityLikes();
        public List<NextMovie> GetNextMoviesForMovieById(int currentMovie);
        public List<NextMovie> GetNextMoviesForMovieByIdForSuggestions(int currentMovieId, int limit, int offset);
        public Task AddNextMovie(int previousMovieId, int nextMovieId, int score);
        public Task UpdateNextMovieScore(int currentMovieId, int nextMovieId, int score);
        public Party GetPartyById(int partyId);
        public List<Party> GetUserParties(string userEmail);
        public Task<Party> AddParty(Party party);
        public Task AddMemberToParty(PartyMember newPartyMember);
        public PartyMember GetPartyMember(int partyId, string userEmail);
        public void RemoveMemberFromParty(PartyMember partyMember);
        public void ResetChoicesForParty(int partyId);
        public List<Movie> GetBatch(int newestId, int oldestId, int limit);
        public void AddChoice(PartyChoice choice);
        public int GetPartyCount(int partyId);
        public List<PartyChoice> GetMovieIdsForParty(int partyId, int count);
        public List<PartyMember> GetPartyMembersForParty(int partyId);
        public List<GenreCount> GetGenreCount();
        public List<CommunityGenreScore> GetCommunityGenresScore();
        public List<Movie> GetLatestHorrorMovies(int chartSize);
    }
}