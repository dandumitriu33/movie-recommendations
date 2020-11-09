using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using Xunit;

namespace MovieRecommendations.Tests
{
    public class MovieViewModelShould
    {
        // Some of these tests are extremey simple. This is an educational
        // project and this is the XUnit part.
        [Fact]
        public void BeNotNullOnCreation()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            // included in the Arrange stage

            // Assert
            Assert.True(movieVM != null);
            Assert.NotNull(movieVM);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            // Arrange
            MovieViewModel movieVM1 = new MovieViewModel();
            MovieViewModel movieVM2 = new MovieViewModel();

            // Act
            // included in the Arrange stage

            // Assert
            Assert.NotSame(movieVM1, movieVM2);
        }

        [Fact]
        public void BeOfTypeMovieViewModel()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            // included in the Arrange stage

            // Assert
            Assert.IsType<MovieViewModel>(movieVM);
        }

        [Fact]
        public void NotBeOfTypeMovie()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            // included in the Arrange stage

            // Assert
            Assert.IsNotType<Movie>(movieVM);
        }

        [Fact]
        public void BeOfTypeMovieViewModelWithTitle()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.IsType<MovieViewModel>(movieVM);

            // Additional asserts on type object
            Assert.Equal("The Best Movie", movieVM.Title);
        }

        [Fact]
        public void HaveCorrectTitleProperty()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.Equal("The Best Movie", movieVM.Title);
        }

        [Fact]
        public void HaveCorrectTitlePropertyCaseInsensitive()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The BEST Movie";

            // Assert
            Assert.Equal("The Best Movie", movieVM.Title, ignoreCase: true);
        }

        [Fact]
        public void HaveTitleStartingWithThe()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();
            
            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.StartsWith("The", movieVM.Title);
        }

        [Fact]
        public void HaveTitleEndingWithMovie()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.EndsWith("Movie", movieVM.Title);
        }

        [Fact]
        public void HaveTitleContainingBest()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.Contains("Best", movieVM.Title);
        }

        [Fact]
        public void HaveTitleWithTitleCase()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", movieVM.Title);
        }

        [Fact]
        public void HaveTitleNullOnCreation()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            // Id should be nulll

            // Assert
            Assert.Null(movieVM.Title);
        }

        [Fact]
        public void HaveTitleWithContent()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(movieVM.Title));
        }

        [Fact]
        public void HaveLengthInMinutes()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.LengthInMinutes = 124;

            // Assert
            Assert.Equal(124, movieVM.LengthInMinutes);
        }

        [Fact]
        public void HaveLengthInMinutesOverZero()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.LengthInMinutes = 124;

            // Assert
            Assert.NotEqual(0, movieVM.LengthInMinutes);
            Assert.True(movieVM.LengthInMinutes > 0);
        }

        [Fact]
        public void HaveLengthInMinutesInRange()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.LengthInMinutes = 124;

            // Assert
            Assert.InRange(movieVM.LengthInMinutes, 1, 5000);
        }

        [Fact]
        public void HaveRatingWithDecimal()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            movieVM.Rating = 8.0;

            // Assert
            Assert.Equal(8.0, movieVM.Rating, 2);
        }
    }
}
