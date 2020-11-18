using Microsoft.EntityFrameworkCore;
using MoviesDataAccessLibrary.Contexts;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRecommendations.Tests.Repository
{
    public class RepositoryShould
    {
        public RepositoryShould()
        {
            //addMovies(options);
            //addHistories(options);
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
                .UseInMemoryDatabase(databaseName: "MRGetFullHistory")
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
