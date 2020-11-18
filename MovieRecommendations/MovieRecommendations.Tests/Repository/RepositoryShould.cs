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
        private DbContextOptions<MoviesContext> _options;
        private MoviesContext _context;

        public RepositoryShould()
        {
            _options = new DbContextOptionsBuilder<MoviesContext>()
                .UseInMemoryDatabase(databaseName: "MovieRecommendationsDataBase")
                .Options;

            _context = new MoviesContext(_options);
            add22Movies();
            
        }

        [Fact]
        public void GetAllMoviesWithPagination()
        {
            // using a context instance to run the repository method
            using( var context = new MoviesContext(_options))
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
            using (var context = new MoviesContext(_options))
            {
                SQLRepository controller = new SQLRepository(context);
                int inventoryCount = controller.GetInventoryTotal();

                Assert.Equal(22, inventoryCount);
            }
        }

        [Fact]
        public async Task AddAMovieToTheDatabase()
        {
            using (var context = new MoviesContext(_options))
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

        private void add22Movies()
        {
            // Create mocked Context by seeding Data as per Schema///

            using (_context)
            {
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 1", LengthInMinutes = 121, Rating = 8.2, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 2", LengthInMinutes = 121, Rating = 6.2, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 3", LengthInMinutes = 121, Rating = 5.9, ReleaseYear = 2020,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                 _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 4", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 5", LengthInMinutes = 121, Rating = 8.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 6", LengthInMinutes = 121, Rating = 7.1, ReleaseYear = 2019,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                 _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 7", LengthInMinutes = 121, Rating = 4.5, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 8", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 9", LengthInMinutes = 121, Rating = 8.5, ReleaseYear = 2018,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 10", LengthInMinutes = 121, Rating = 2.1, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 11", LengthInMinutes = 121, Rating = 8.3, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 12", LengthInMinutes = 121, Rating = 4.5, ReleaseYear = 2017,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 13", LengthInMinutes = 121, Rating = 9.1, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 14", LengthInMinutes = 121, Rating = 3.4, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 15", LengthInMinutes = 121, Rating = 5.1, ReleaseYear = 2016,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 16", LengthInMinutes = 121, Rating = 1.9, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 17", LengthInMinutes = 121, Rating = 8.2, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 18", LengthInMinutes = 121, Rating = 5.1, ReleaseYear = 2015,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 19", LengthInMinutes = 121, Rating = 7.1, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 20", LengthInMinutes = 121, Rating = 8.7, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 21", LengthInMinutes = 121, Rating = 1.1, ReleaseYear = 2014,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 22", LengthInMinutes = 121, Rating = 8.1, ReleaseYear = 2013,
                    MainGenre = "Comedy", SubGenre1 = "Action", SubGenre2 = "Adventure"
                });
                _context.SaveChanges();
            }
        }
    }
}
