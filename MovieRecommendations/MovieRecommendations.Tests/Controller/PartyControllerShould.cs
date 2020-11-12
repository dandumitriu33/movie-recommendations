using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;
using MovieRecommendations.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MovieRecommendations.Tests.Controller
{
    public class PartyControllerShould
    {
        [Fact]
        public void ReturnViewForAllParties()
        {
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new PartyController(mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.AllParties("john@email.com");

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnRedirectToActionForCreatePartyPost()
        {
            var mockRepository = new Mock<IRepository>();
            var mockMapper = new Mock<IMapper>();

            // sut = System Under Test
            var sut = new PartyController(mockRepository.Object, mockMapper.Object);

            IActionResult result = sut.CreateParty("john@email.com");

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
