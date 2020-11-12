using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;
using MovieRecommendations.Interfaces;
using MovieRecommendations.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MovieRecommendations.Tests.Controller
{
    public class UserControllerShould
    {
        [Fact]
        public void ReturnViewForUserHistory()
        {
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockBuilder = new Mock<IPersonalizedRecommendationsBuilder>();

            // sut = System Under Test
            var sut = new UserController(mockRepository.Object, mockMapper.Object, mockBuilder.Object);

            IActionResult result = sut.UserHistory("john@email.com");

            Assert.IsType<ViewResult>(result);
        }

    }
}
