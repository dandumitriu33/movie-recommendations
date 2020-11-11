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
            List<Movie> historyBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 8; i++)
            {
                Movie historyEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test History Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                historyBasedSuggestions.Add(historyEntry);
            }
            List<Movie> expected = new List<Movie>();
            expected.Add(rabbitHoleSuggestions[0]);
            expected.Add(communityBasedSuggestions[0]);
            foreach (var entry in historyBasedSuggestions)
            {
                expected.Add(entry);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, historyBasedSuggestions);

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Rabbit Hole Entry", actual[0].Title);
            Assert.Equal("Test Community Entry", actual[1].Title);
            Assert.Equal("Test History Entry 1", actual[3].Title);
            Assert.Equal("Test History Entry 7", actual[9].Title);
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
            List<Movie> historyBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 9; i++)
            {
                Movie historyEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test History Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                historyBasedSuggestions.Add(historyEntry);
            }
            List<Movie> expected = new List<Movie>();            
            expected.Add(communityBasedSuggestions[0]);
            foreach (var entry in historyBasedSuggestions)
            {
                expected.Add(entry);
            }
            foreach (var mv in expected)
            {
                _output.WriteLine(mv.Title);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, historyBasedSuggestions);
            foreach (var mv in actual)
            {
                _output.WriteLine(mv.Title);
            }

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Community Entry", actual[0].Title);
            Assert.Equal("Test History Entry 1", actual[2].Title);
            Assert.Equal("Test History Entry 7", actual[8].Title);
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
            List<Movie> historyBasedSuggestions = new List<Movie>();
            for (int i = 0; i < 9; i++)
            {
                Movie historyEntry = new Movie
                {
                    Id = 101,
                    Title = $"Test History Entry {i}",
                    LengthInMinutes = 142,
                    Rating = 5.6,
                    ReleaseYear = 2019,
                    MainGenre = "Action",
                    SubGenre1 = "Crime",
                    SubGenre2 = "Adventure"
                };
                historyBasedSuggestions.Add(historyEntry);
            }
            List<Movie> expected = new List<Movie>();
            expected.Add(rabbitHoleSuggestions[0]);
            foreach (var entry in historyBasedSuggestions)
            {
                expected.Add(entry);
            }
            foreach (var mv in expected)
            {
                _output.WriteLine(mv.Title);
            }

            // Act
            PersonalizedRecommendationsBuilder builder = new PersonalizedRecommendationsBuilder();
            List<Movie> actual = builder.Build(rabbitHoleSuggestions, communityBasedSuggestions, historyBasedSuggestions);
            foreach (var mv in actual)
            {
                _output.WriteLine(mv.Title);
            }

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal("Test Rabbit Hole Entry", actual[0].Title);
            Assert.Equal("Test History Entry 1", actual[2].Title);
            Assert.Equal("Test History Entry 7", actual[8].Title);
            Assert.Equal(10, actual.Count);
        }
    }
}
