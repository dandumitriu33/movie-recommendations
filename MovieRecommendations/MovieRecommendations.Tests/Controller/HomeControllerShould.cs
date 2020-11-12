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
    }
}
