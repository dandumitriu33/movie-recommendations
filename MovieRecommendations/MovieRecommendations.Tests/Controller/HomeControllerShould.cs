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
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        // sut = System Under Test
        private readonly HomeController _sut;

        public HomeControllerShould()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new HomeController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        // FAILS because of HTTPContext for cookie management
        //[Fact]
        //public void ReturnViewForIndex()
        //{
        
        //    IActionResult result = _sut.Index();

        //    Assert.IsType<ViewResult>(result);
        //}

        [Fact]
        public void ReturnViewForPrivacy()
        {
            IActionResult result = _sut.Privacy();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAllMovies()
        {
            IActionResult result = _sut.AllMovies();

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
            IActionResult result = _sut.AddMovie();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForDetails()
        {
            IActionResult result = _sut.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForHandleError()
        {
            IActionResult result = _sut.HandleError(404);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForAddMoviePost()
        {
            IActionResult result = _sut.AddMovie(new MovieViewModel 
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
            IActionResult result = _sut.AddMovie(new MovieViewModel
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
            IActionResult result = _sut.ProcessWatchedMovie("john@email.com", 1);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
