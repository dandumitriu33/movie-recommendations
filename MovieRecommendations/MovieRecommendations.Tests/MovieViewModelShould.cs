using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using Xunit;
using Xunit.Abstractions;

namespace MovieRecommendations.Tests
{
    [Trait("Category", "MovieViewModel")]
    public class MovieViewModelShould
    {
        private readonly ITestOutputHelper _output;
        private readonly MovieViewModel _movieVM;

        // Some of these tests are extremey simple. This is an educational
        // project and this is the XUnit part.
        // an instane of this class is created for every test, some info can be DRY
        // context classes can be shared between tests or across (with other) classes with IClassFixture and ICollectionFixture

        public MovieViewModelShould(ITestOutputHelper output)
        {
            _output = output;
            _movieVM = new MovieViewModel();
        }

        [Fact]
        public void Dispose()
        {
            _output.WriteLine($"Disposing movieVM: {_movieVM.Title}.");
            //_movieVM.Dispose(); if we had a Dispose method in MovieViewModel to dispose of the object IDisposable
        }

        [Fact]
        public void BeNotNullOnCreation()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();  // in ctor
            _output.WriteLine("Not instantiating movieVM as it was instantiated in the constructor.");
            _output.WriteLine($"Current title: {_movieVM.Title}");

            // Act
            // included in the Arrange stage
            _output.WriteLine("Nothing to Act. Null verification.");

            // Assert
            Assert.True(_movieVM != null);
            Assert.NotNull(_movieVM);
            // additional info after a step, the point is to use Output with more info
            _output.WriteLine("Asserts complete.");
            _movieVM.Title ??= _movieVM.Title = "New Movie";
            _output.WriteLine($"The title updated to: {_movieVM.Title}");
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
            // MovieViewModel movieVM = new MovieViewModel();  // in ctor

            // Act
            // included in the Arrange stage

            // Assert
            Assert.IsType<MovieViewModel>(_movieVM);
        }

        [Fact]
        public void NotBeOfTypeMovie()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();  // in ctor

            // Act
            // included in the Arrange stage

            // Assert
            Assert.IsNotType<Movie>(_movieVM);
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
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.Equal("The Best Movie", _movieVM.Title);
        }

        [Fact]
        public void HaveCorrectTitlePropertyCaseInsensitive()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The BEST Movie";

            // Assert
            Assert.Equal("The Best Movie", _movieVM.Title, ignoreCase: true);
        }

        [Fact]
        public void HaveTitleStartingWithThe()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();
            
            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.StartsWith("The", _movieVM.Title);
        }

        [Fact]
        public void HaveTitleEndingWithMovie()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.EndsWith("Movie", _movieVM.Title);
        }

        [Fact]
        public void HaveTitleContainingBest()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.Contains("Best", _movieVM.Title);
        }

        [Fact]
        public void HaveTitleWithTitleCase()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _movieVM.Title);
        }

        [Fact]
        public void HaveTitleNullOnCreation()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            // Id should be nulll

            // Assert
            Assert.Null(_movieVM.Title);
        }

        [Fact]
        public void HaveTitleWithContent()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Title = "The Best Movie";

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(_movieVM.Title));
        }

        [Fact]
        public void HaveLengthInMinutes()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.LengthInMinutes = 124;

            // Assert
            Assert.Equal(124, _movieVM.LengthInMinutes);
        }

        [Fact]
        //[Fact(Skip ="Covered in the next text with the positive range.")]
        public void HaveLengthInMinutesOverZero()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.LengthInMinutes = 124;

            // Assert
            Assert.NotEqual(0, _movieVM.LengthInMinutes);
            Assert.True(_movieVM.LengthInMinutes > 0);
        }

        [Fact]
        public void HaveLengthInMinutesInRange()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.LengthInMinutes = 124;

            // Assert
            Assert.InRange(_movieVM.LengthInMinutes, 1, 5000);
        }

        [Fact]
        public void HaveRatingWithDecimal()
        {
            // Arrange
            //MovieViewModel movieVM = new MovieViewModel();

            // Act
            _movieVM.Rating = 8.0;

            // Assert
            Assert.Equal(8.0, _movieVM.Rating, 2);
        }
    }
}
