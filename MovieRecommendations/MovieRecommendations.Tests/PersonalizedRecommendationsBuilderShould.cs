using MovieRecommendations.Models;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace MovieRecommendations.Tests
{
    public class PersonalizedRecommendationsBuilderShould
    {
        private readonly ITestOutputHelper _output;

        public PersonalizedRecommendationsBuilderShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OutputCorrectList()
        {
            // Arrange
            List<Movie> rabbitHoleSuggestions = new List<Movie>();
            Movie rabbitHoleEntry = new Movie
            {
                Id = 101,
                Title = "Test Rabbit Hole Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            rabbitHoleSuggestions.Add(rabbitHoleEntry);
            List<Movie> communityBasedSuggestions = new List<Movie>();
            Movie communityEntry = new Movie
            {
                Id = 101,
                Title = "Test Community Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            communityBasedSuggestions.Add(communityEntry);
            List<Movie> contentBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 8; i++)
            {
                Movie contentEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test Content Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                contentBasedSuggestions.Add(contentEntry);
            }
            List<Movie> expected = new List<Movie>();
            expected.Add(rabbitHoleSuggestions[0]);
            expected.Add(communityBasedSuggestions[0]);
            foreach (var entry in contentBasedSuggestions)
            {
                expected.Add(entry);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, contentBasedSuggestions);

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Rabbit Hole Entry", actual[0].Title);
            Assert.Equal("Test Community Entry", actual[1].Title);
            Assert.Equal("Test Content Entry 1", actual[3].Title);
            Assert.Equal("Test Content Entry 7", actual[9].Title);
            Assert.Equal(10, actual.Count);
        }

        [Fact]
        public void OutputCorrectListNoRabbitHole()
        {
            // Arrange
            List<Movie> rabbitHoleSuggestions = new List<Movie>();
            List<Movie> communityBasedSuggestions = new List<Movie>();
            Movie communityEntry = new Movie
            {
                Id = 101,
                Title = "Test Community Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            communityBasedSuggestions.Add(communityEntry);
            List<Movie> contentBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 9; i++)
            {
                Movie contentEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test Content Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                contentBasedSuggestions.Add(contentEntry);
            }
            List<Movie> expected = new List<Movie>();            
            expected.Add(communityBasedSuggestions[0]);
            foreach (var entry in contentBasedSuggestions)
            {
                expected.Add(entry);
            }
            foreach (var mv in expected)
            {
                _output.WriteLine(mv.Title);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, contentBasedSuggestions);
            foreach (var mv in actual)
            {
                _output.WriteLine(mv.Title);
            }

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Community Entry", actual[0].Title);
            Assert.Equal("Test Content Entry 1", actual[2].Title);
            Assert.Equal("Test Content Entry 7", actual[8].Title);
            Assert.Equal(10, actual.Count);
        }

        [Fact]
        public void OutputCorrectListNoCommunity()
        {
            // Arrange
            List<Movie> rabbitHoleSuggestions = new List<Movie>();
            Movie rabbitHoleEntry = new Movie
            {
                Id = 101,
                Title = "Test Rabbit Hole Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            rabbitHoleSuggestions.Add(rabbitHoleEntry);
            List<Movie> communityBasedSuggestions = new List<Movie>();
            List<Movie> contentBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 9; i++)
            {
                Movie contentEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test Content Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                contentBasedSuggestions.Add(contentEntry);
            }
            List<Movie> expected = new List<Movie>();
            expected.Add(rabbitHoleSuggestions[0]);
            foreach (var entry in contentBasedSuggestions)
            {
                expected.Add(entry);
            }
            foreach (var mv in expected)
            {
                _output.WriteLine(mv.Title);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, contentBasedSuggestions);
            foreach (var mv in actual)
            {
                _output.WriteLine(mv.Title);
            }

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Rabbit Hole Entry", actual[0].Title);
            Assert.Equal("Test Content Entry 1", actual[2].Title);
            Assert.Equal("Test Content Entry 7", actual[8].Title);
            Assert.Equal(10, actual.Count);
        }

        [Fact]
        public void OutputCorrectListNoHistory()
        {
            // Arrange
            List<Movie> rabbitHoleSuggestions = new List<Movie>();
            Movie rabbitHoleEntry = new Movie
            {
                Id = 101,
                Title = "Test Rabbit Hole Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            rabbitHoleSuggestions.Add(rabbitHoleEntry);
            List<Movie> communityBasedSuggestions = new List<Movie>();
            Movie communityEntry = new Movie
            {
                Id = 101,
                Title = "Test Community Entry",
                LengthInMinutes = 142,
                Rating = 5.6,
                ReleaseYear = 2019,
                MainGenre = "Action",
                SubGenre1 = "Crime",
                SubGenre2 = "Adventure"
            };
            communityBasedSuggestions.Add(communityEntry);
            List<Movie> contentBasedSuggestions = new List<Movie>();
            List<Movie> expected = new List<Movie>();
            expected.Add(rabbitHoleSuggestions[0]);
            expected.Add(communityBasedSuggestions[0]);

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, contentBasedSuggestions);

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Rabbit Hole Entry", actual[0].Title);
            Assert.Equal("Test Community Entry", actual[1].Title);
            Assert.Equal(2, actual.Count);
        }
    }
}
