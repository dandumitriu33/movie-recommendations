using Microsoft.EntityFrameworkCore;
using MoviesDataAccessLibrary.Contexts;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieRecommendations.Tests.Repository
{
    public class RepositoryShould
    {
        [Fact]
        public void GetAllMoviesWithPagination()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<MoviesContext>()
            .UseInMemoryDatabase(databaseName: "MovieRecommendationsDataBase")
            .Options;

            // Create mocked Context by seeding Data as per Schema///

            using (var context = new MoviesContext(options))
            {
                context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie",
                    LengthInMinutes = 121,
                    Rating = 2.1,
                    ReleaseYear = 2003,
                    MainGenre = "Comedy",
                    SubGenre1 = "Action",
                    SubGenre2 = "Adventure"
                });

                context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 2",
                    LengthInMinutes = 121,
                    Rating = 2.1,
                    ReleaseYear = 2003,
                    MainGenre = "Comedy",
                    SubGenre1 = "Action",
                    SubGenre2 = "Adventure"
                });

                context.Movies.Add(new Movie
                {
                    Title = "In Memory DB Movie 3",
                    LengthInMinutes = 121,
                    Rating = 2.1,
                    ReleaseYear = 2003,
                    MainGenre = "Comedy",
                    SubGenre1 = "Action",
                    SubGenre2 = "Adventure"
                });
                context.SaveChanges();

            }

            // using a context instance to run the repository method
            using( var context = new MoviesContext(options))
            {
                SQLRepository controller = new SQLRepository(context);

                var allMovies = controller.GetAllMovies(1, 20);

                Assert.Equal(3, allMovies.Count);
                Assert.Equal("In Memory DB Movie 3", allMovies[2].Title);
            }
        }
    }
}
