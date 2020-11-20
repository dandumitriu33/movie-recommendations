using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MovieRecommendations.Controllers;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Entities;
using MovieRecommendations.ViewModels;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace MovieRecommendations.Tests.Controller
{
    public class HomeControllerShould
    {
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ITestOutputHelper _output;
        // sut = System Under Test
        private readonly HomeController _sut;

        public HomeControllerShould(ITestOutputHelper output)
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();
            _output = output;
            _sut = new HomeController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void ReturnViewForPrivacy()
        {
            IActionResult result = _sut.Privacy();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAllMovies()
        {
            IActionResult result = _sut.AllMovies(1, 20);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAllCommunityLikes()
        {
            IActionResult result = _sut.AllCommunityLikes();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAddMovie()
        {
            var result = _sut.AddMovie();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForDetails()
        {
            IActionResult result = _sut.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void RunGetMovieByMovieIdOnceForDetails()
        {
            IActionResult result = _sut.Details(1);

            Assert.IsType<ViewResult>(result);
            _mockRepository.Verify(x => x.GetMovieByMovieId(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void RunMapperForDetails()
        {
            IActionResult result = _sut.Details(1);

            Assert.IsType<ViewResult>(result);
            _mockMapper.Verify(x => x.Map<Movie, MovieViewModel>(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public void ReturnsCorrectViewForDetails()
        {
            ViewResult result = (ViewResult) _sut.Details(1);
            
            Assert.Equal("Details", result.ViewName);
        }

        [Fact]
        public void ReturnViewForHandleError()
        {
            IActionResult result = _sut.HandleError(404);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ReturnViewForAddMoviePost()
        {
            IActionResult result = await _sut.AddMovie(new MovieViewModel 
            { 
                Id = 1,
                Title = "Test Title",
                LengthInMinutes = 121,
                Rating = 3.4,
                ReleaseYear = 2003,
                MainGenre = "Comedy",
                SubGenre1 = "Adventure",
                SubGenre2 = "Crime"
            });

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task RunAddOnceForValidModel()
        {
            MovieViewModel newEntry = new MovieViewModel
            {
                Id = 1,
                Title = "Test Title",
                LengthInMinutes = 121,
                Rating = 3.4,
                ReleaseYear = 2003,
                MainGenre = "Comedy",
                SubGenre1 = "Adventure",
                SubGenre2 = "Crime"
            };
            await _sut.AddMovie(newEntry);

            _mockRepository.Verify(x => x.Add(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task NotRunAddForInvalidModel()
        {
            _sut.ModelState.AddModelError("x", "Test error.");
            MovieViewModel newEntry = new MovieViewModel
            {
                Id = 1,
                Title = "Test Title",
                LengthInMinutes = 121,
                Rating = 3.4,
                ReleaseYear = 2003,
                MainGenre = "Comedy",
                SubGenre1 = "Adventure",
                SubGenre2 = "Crime"
            };
            await _sut.AddMovie(newEntry);

            _mockRepository.Verify(x => x.Add(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task ReturnViewForAddMoviePostInvalidModelTitleDisplay()
        {
            _sut.ModelState.AddModelError("x", "Test error.");
            MovieViewModel newEntry = new MovieViewModel
            {
                Id = 1,
                Title = "Test Title",
                LengthInMinutes = 121,
                Rating = 3.4,
                ReleaseYear = 2003,
                MainGenre = "Comedy",
                SubGenre1 = "Adventure",
                SubGenre2 = "Crime"
            };
            IActionResult result = await _sut.AddMovie(newEntry);

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<MovieViewModel>(viewResult.Model);
            Assert.Equal(newEntry.Title, model.Title);
            _output.WriteLine($"Movie title in model: {model.Title}");
            _mockRepository.Verify(x => x.Add(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public void ReturnRedirectToActionForProcessWatchedMovie()
        {
            IActionResult result = _sut.ProcessWatchedMovie("john@email.com", 1);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
