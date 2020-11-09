using MovieRecommendations.ViewModels;
using System;
using Xunit;

namespace MovieRecommendations.Tests
{
    public class MovieViewModelShould
    {
        [Fact]
        public void BeNotNullOnCreation()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();

            // Act
            // included in the Arrange stage

            // Assert
            Assert.True(movieVM != null);
        }

        [Fact]
        public void HaveNameStrtingWithThe()
        {
            // Arrange
            MovieViewModel movieVM = new MovieViewModel();
            
            // Act
            movieVM.Title = "The Best Movie";

            // Assert
            Assert.StartsWith("The", movieVM.Title);
        }
    }
}
