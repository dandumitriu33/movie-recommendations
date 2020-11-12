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

namespace MovieRecommendations.Tests.Controller
{
    public class HomeControllerShould
    {
        [Fact]
        public void ReturnViewForPrivacy()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.Privacy();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAllMovies()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AllMovies();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAllCommunityLikes()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AllCommunityLikes();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAddMovie()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AddMovie();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForDetails()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForHandleError()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.HandleError(404);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAddMoviePost()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AddMovie(new MovieViewModel 
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
        public void ReturnViewForAddMoviePostInvalidModel()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AddMovie(new MovieViewModel
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
        public void ReturnRedirectToActionForProcessWatchedMovie()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new HomeController(mockLogger.Object, mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.ProcessWatchedMovie("john@email.com", 1);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
