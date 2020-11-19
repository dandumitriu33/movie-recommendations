using Microsoft.EntityFrameworkCore;
using MoviesDataAccessLibrary.Contexts;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MovieRecommendations.Tests.Repository
{
    public class RepositoryShould
    {
        private readonly ITestOutputHelper _output;

        public RepositoryShould(ITestOutputHelper output)
        {
            _output = output;
        }

        

        [Fact]
        public void GetAllMoviesWithPagination()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAllMovies")
                .Options;
            addMovies(options);
            // using a context instance to run the repository method
            using ( var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                var allMovies = controller.GetAllMovies(1, 20);

                Assert.Equal(20, allMovies.Count);
                Assert.Equal("In Memory DB Movie 3", allMovies[2].Title);
            }
        }

        [Fact]
        public void GetInventoryTotal()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetInventory")
                .Options;
            addMovies(options);
            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                int inventoryCount = controller.GetInventoryTotal();

                Assert.Equal(22, inventoryCount);
            }
        }

        [Fact]
        public async Task AddAMovieToTheDatabase()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRADdMovie")
                .Options;
            addMovies(options);
            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                Movie movieToAdd = new Movie
                {
                    Title = "Movie to add to Mem DB 23",
                    LengthInMinutes = 134,
                    Rating = 7.9,
                    ReleaseYear = 2003,
                    MainGenre = "Action",
                    SubGenre1 = "Comedy",
                    SubGenre2 = "Adventure"
                };
                await controller.Add(movieToAdd);
                int inventoryCount = controller.GetInventoryTotal();

                Assert.Equal(23, inventoryCount);
            }
        }

        [Fact]
        public void GetTop20YearRatingMovies()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetTop20")
                .Options;
            addMovies(options);
            
            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                var top20Movies = controller.GetTop20YearRating();

                // movies over 6.5
                Assert.Equal(10, top20Movies.Count);
                Assert.Equal("In Memory DB Movie 6", top20Movies[2].Title);
            }
        }

        [Fact]
        public void GetMovieByMovieId()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetMovieByMovieId")
                .Options;
            addMovies(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                Movie movieFromDb = controller.GetMovieByMovieId(5);
                
                Assert.Equal("In Memory DB Movie 5", movieFromDb.Title);
            }
        }

        [Fact]
        public void GetFullHistory()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetFullHistory")
                .Options;
            addHistories(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                
                List<History> history = controller.GetFullHistory("jimsmith@email.com");

                Assert.Equal(3, history.Count);
            }
        }

        [Fact]
        public async Task AddToHistory()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAddToHistory")
                .Options;
            addHistories(options);
            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                string email = "jimsmith@email.com";
                int movieId = 20;

                await controller.AddToHistory(email, movieId);
                List<History> fullHistory = controller.GetFullHistory("jimsmith@email.com");

                Assert.Equal(4, fullHistory.Count);
            }
        }

        [Fact]
        public void GetLatestFromHistory()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetLatestHistory")
                .Options;
            addHistories(options);
            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                string email = "jimsmith@email.com";
                
                History latestHistoryItem = controller.GetLatestFromHistory(email);

                Assert.Equal(12, latestHistoryItem.MovieId);
            }
        }

        [Fact]
        public void GetDistanceRecommendation()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetDistanceRec")
                .Options;
            addMovies(options);

            using( var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                string mainGenre = "Comedy";
                double rating = 6;
                int limit = 20;
                int offset = 0;

                List<Movie> distanceContent = controller.GetDistanceRecommendation(mainGenre, rating, limit, offset);

                Assert.Equal(16, distanceContent.Count);
                Assert.True(distanceContent[0].Rating > 4);
            }
        }

        [Fact]
        public void GetCommunityLikedMovieById()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRCommLikedById")
                .Options;
            addCommunityLikes(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                UserLikedMovie movieScore = controller.GetCommunityLikedMovieById(3);

                Assert.Equal(4, movieScore.Score);
            }
        }

        [Fact]
        public async Task IncrementScoreOnLikedMovie()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRIncrementScore")
                .Options;
            addCommunityLikes(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                await controller.IncrementCommunityLikedMovieScore(3);
                UserLikedMovie movieScore = controller.GetCommunityLikedMovieById(3);

                Assert.Equal(5, movieScore.Score);
            }
        }

        [Fact]
        public async Task AddToCommunityLikes()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAddToLikes")
                .Options;
            addCommunityLikes(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                
                await controller.AddToCommunityLikes(8);
                UserLikedMovie movieScore = controller.GetCommunityLikedMovieById(8);

                Assert.Equal(1, movieScore.Score);
            }
        }

        [Fact]
        public void GetCommunityLikesTop()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetCommLikesTop")
                .Options;
            addCommunityLikes(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<UserLikedMovie> movieScores = controller.GetCommunityTop(2, 0);

                Assert.Equal(2, movieScores.Count);
                Assert.True(movieScores[0].Score >= movieScores[1].Score);
            }
        }

        [Fact]
        public void GetAllCommunityLikes()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetCommLikesAll")
                .Options;
            addCommunityLikes(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<UserLikedMovie> movieScores = controller.GetAllCommunityLikes();

                Assert.Equal(3, movieScores.Count);
                Assert.True(movieScores[0].Score >= movieScores[1].Score);
            }
        }

        [Fact]
        public void GetNextMoviesForMovieById()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRNextMoviesForMovie")
                .Options;
            addNextMovies(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<NextMovie> nextMovies = controller.GetNextMoviesForMovieById(1);

                Assert.Equal(2, nextMovies.Count);
                Assert.True(nextMovies[0].Score >= nextMovies[1].Score);
            }
        }

        [Fact]
        public void GetNextMoviesForMovieByIdForSuggestions()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRNextMoviesForMovieForSuggestions")
                .Options;
            addNextMovies(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<NextMovie> nextMovies = controller.GetNextMoviesForMovieByIdForSuggestions(1, 2, 0);

                Assert.Equal(2, nextMovies.Count);
                Assert.True(nextMovies[0].Score >= nextMovies[1].Score);
            }
        }

        [Fact]
        public async Task AddNextMovie()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAddNextMovie")
                .Options;
            addNextMovies(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                await controller.AddNextMovie(1, 20, 1);
                List<NextMovie> nextMovies = controller.GetNextMoviesForMovieById(1);

                Assert.Equal(3, nextMovies.Count);
                Assert.True(nextMovies[0].Score >= nextMovies[1].Score);
            }
        }

        [Fact]
        public async Task UpdateNextMovie()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRUpdateNextMovie")
                .Options;
            addNextMovies(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                await controller.UpdateNextMovieScore(1, 2, 6);
                List<NextMovie> nextMovies = controller.GetNextMoviesForMovieById(1);

                Assert.Equal(2, nextMovies.Count);
                Assert.True(nextMovies[0].Score == 6);
            }
        }

        [Fact]
        public void GetPartyById()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetPartyById")
                .Options;
            addParties(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                Party result = controller.GetPartyById(1);

                Assert.Equal(1, result.Id);
                Assert.Equal("First", result.Name);
            }
        }

        [Fact]
        public void GetUserParties()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetUserParties")
                .Options;
            addParties(options);
            addPartyMembers(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<Party> result = controller.GetUserParties("janesmith@email.com");

                Assert.Equal(2, result.Count);
                Assert.Equal("First", result[0].Name);
            }
        }

        [Fact]
        public async Task AddParty()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAddParty")
                .Options;
            addParties(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                Party partyToAdd = new Party
                {
                    Id = 4,
                    Name = "Fourth",
                    CreatorEmail = "jimsmith@email.com"
                };
                await controller.AddParty(partyToAdd);
                Party actual = controller.GetPartyById(4);

                Assert.Equal("Fourth", actual.Name);
            }
        }

        [Fact]
        public async Task AddMemberToParty()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRAddMemberToParty")
                .Options;
            addParties(options);
            addPartyMembers(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                PartyMember partyMemberToAdd = new PartyMember
                {
                    Id = 4,
                    PartyId = 2,
                    Email = "jimsmith@email.com"
                };
                await controller.AddMemberToParty(partyMemberToAdd);
                List<Party> actual = controller.GetUserParties("jimsmith@email.com");

                Assert.Equal("Second", actual[1].Name);
            }
        }

        [Fact]
        public void GetPartyMember()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetPartyMember")
                .Options;
            addPartyMembers(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                
                PartyMember actual = controller.GetPartyMember(1, "jimsmith@email.com");

                Assert.Equal("jimsmith@email.com", actual.Email);
            }
        }

        [Fact]
        public void RemoveMemberFromParty()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRRemoveMemberFromParty")
                .Options;
            addParties(options);
            addPartyMembers(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);
                PartyMember memberToRemove = new PartyMember
                {
                    Id = 1,
                    PartyId = 1,
                    Email = "jimsmith@email.com"
                };
                controller.RemoveMemberFromParty(memberToRemove);
                List<Party> actual = controller.GetUserParties("jimsmith@email.com");

                Assert.Empty(actual);
            }
        }

        [Fact]
        public void GetAllChoicesForParty()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetAllChoices")
                .Options;
            addPartyChoices(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<PartyChoice> actual = controller.GetAllPartyChoicesForParty(1);

                Assert.Equal(3, actual.Count);
            }
        }

        [Fact]
        public void ResetChoicesForParty()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRResetChoices")
                .Options;
            addPartyChoices(options);

            using (var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                List<PartyChoice> initial = controller.GetAllPartyChoicesForParty(1);
                _output.WriteLine($"The length of the choice list before reset: {initial.Count}");
                controller.ResetChoicesForParty(1);
                List<PartyChoice> actual = controller.GetAllPartyChoicesForParty(1);
                _output.WriteLine($"The length of the choice list after reset: {actual.Count}");
                Assert.Empty(actual);
            }
        }

        [Fact]
        public void GetBatch()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MRGetBatch")
                .Options;
            addMovies(options);

            using ( var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                int newestId = 21;
                int oldestId = 11;
                int limit = 5;
                List<Movie> actual = controller.GetBatch(newestId, oldestId, limit);
                // expecting movies 22, 10, 9, 8 and 7
                Assert.Equal(5, actual.Count);
                Assert.Equal(22, actual[0].Id);
                Assert.Equal(10, actual[1].Id);
                Assert.Equal(9, actual[2].Id);
                Assert.Equal(8, actual[3].Id);
                Assert.Equal(7, actual[4].Id);
            }
        }



        private void addPartyChoices(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.PartyChoices.Add(new PartyChoice
                {
                    Id = 1,
                    PartyId = 1,
                    MovieId = 1,
                    Score = 1
                });
                context.PartyChoices.Add(new PartyChoice
                {
                    Id = 2,
                    PartyId = 1,
                    MovieId = 2,
                    Score = 2
                });
                context.PartyChoices.Add(new PartyChoice
                {
                    Id = 3,
                    PartyId = 1,
                    MovieId = 3,
                    Score = 2
                });
                context.SaveChanges();
            }
        }

        private void addPartyMembers(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.PartyMembers.Add(new PartyMember
                {
                    Id = 1,
                    PartyId = 1,
                    Email = "jimsmith@email.com"
                });
                context.PartyMembers.Add(new PartyMember
                {
                    Id = 2,
                    PartyId = 1,
                    Email = "janesmith@email.com"
                });
                context.PartyMembers.Add(new PartyMember
                {
                    Id = 3,
                    PartyId = 2,
                    Email = "janesmith@email.com"
                });
                context.SaveChanges();
            }
        }

        private void addParties(DbContextOptions<MoviesContext> options)
        {
            using( var context = new MoviesContext(options))
            {
                context.Parties.Add(new Party
                {
                    Id = 1,
                    Name = "First",
                    CreatorEmail = "jimsmith@email.com"
                });
                context.Parties.Add(new Party
                {
                    Id = 2,
                    Name = "Second",
                    CreatorEmail = "jimsmith@email.com"
                });
                context.Parties.Add(new Party
                {
                    Id = 3,
                    Name = "Third",
                    CreatorEmail = "jimsmith@email.com"
                });
                context.SaveChanges();
            }
        }

        private void addNextMovies(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.NextMovies.Add(new NextMovie
                {
                    Id = 1,
                    CurrentMovieId = 1,
                    NextMovieId = 2,
                    Score = 5
                });
                context.NextMovies.Add(new NextMovie
                {
                    Id = 2,
                    CurrentMovieId = 2,
                    NextMovieId = 3,
                    Score = 5
                });
                context.NextMovies.Add(new NextMovie
                {
                    Id = 3,
                    CurrentMovieId = 3,
                    NextMovieId = 4,
                    Score = 5
                });
                context.NextMovies.Add(new NextMovie
                {
                    Id = 4,
                    CurrentMovieId = 1,
                    NextMovieId = 3,
                    Score = 4
                });
                context.SaveChanges();
            }
        }

        private void addCommunityLikes(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.CommunityLikes.Add(new UserLikedMovie
                {
                    Id = 1,
                    MovieId = 1,
                    Score = 5
                });
                context.CommunityLikes.Add(new UserLikedMovie
                {
                    Id = 2,
                    MovieId = 3,
                    Score = 4
                });
                context.CommunityLikes.Add(new UserLikedMovie
                {
                    Id = 3,
                    MovieId = 5,
                    Score = 1
                });

                context.SaveChanges();
            }
        }

        private void addHistories(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.Histories.Add(new History
                {
                    Id = 1,
                    Email = "jimsmith@email.com",
                    MovieId = 1,
                    DateAdded = new DateTime(2020, 11, 11, 12, 39, 41)
                });
                context.Histories.Add(new History
                {
                    Id = 2,
                    Email = "jimsmith@email.com",
                    MovieId = 5,
                    DateAdded = new DateTime(2020, 11, 12, 12, 39, 41)
                });
                context.Histories.Add(new History
                {
                    Id = 3,
                    Email = "jimsmith@email.com",
                    MovieId = 12,
                    DateAdded = new DateTime(2020, 11, 14, 12, 39, 41)
                });
                context.SaveChanges();
            }
        }

        private void addMovies(DbContextOptions<MoviesContext> options)
        {
            using (var context = new MoviesContext(options))
            {
                context.Movies.Add(new Movie
                {
                    Id = 1, Title = "In Memory DB Movie 1", LengthInMinutes = 121, Rating = 8.2, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 2, Title = "In Memory DB Movie 2", LengthInMinutes = 121, Rating = 6.2, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 3, Title = "In Memory DB Movie 3", LengthInMinutes = 121, Rating = 5.9, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 4, Title = "In Memory DB Movie 4", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 5, Title = "In Memory DB Movie 5", LengthInMinutes = 121, Rating = 8.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 6, Title = "In Memory DB Movie 6", LengthInMinutes = 121, Rating = 7.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 7, Title = "In Memory DB Movie 7", LengthInMinutes = 121, Rating = 4.5, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 8, Title = "In Memory DB Movie 8", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 9, Title = "In Memory DB Movie 9", LengthInMinutes = 121, Rating = 8.5, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 10, Title = "In Memory DB Movie 10", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 11, Title = "In Memory DB Movie 11", LengthInMinutes = 121, Rating = 8.3, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 12, Title = "In Memory DB Movie 12", LengthInMinutes = 121, Rating = 4.5, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 13, Title = "In Memory DB Movie 13", LengthInMinutes = 121, Rating = 9.1, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 14, Title = "In Memory DB Movie 14", LengthInMinutes = 121, Rating = 3.4, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 15, Title = "In Memory DB Movie 15", LengthInMinutes = 121, Rating = 5.1, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 16, Title = "In Memory DB Movie 16", LengthInMinutes = 121, Rating = 1.9, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 17, Title = "In Memory DB Movie 17", LengthInMinutes = 121, Rating = 8.2, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 18, Title = "In Memory DB Movie 18", LengthInMinutes = 121, Rating = 5.1, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 19, Title = "In Memory DB Movie 19", LengthInMinutes = 121, Rating = 7.1, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 20, Title = "In Memory DB Movie 20", LengthInMinutes = 121, Rating = 8.7, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 21, Title = "In Memory DB Movie 21", LengthInMinutes = 121, Rating = 1.1, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.Movies.Add(new Movie
                {
                    Id = 22, Title = "In Memory DB Movie 22", LengthInMinutes = 121, Rating = 8.1, ReleaseYear = 2013,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                context.SaveChanges();
            }
        }

    }
}
