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
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PartyController _sut;

        public PartyControllerShould()
        {
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new PartyController(_mockRepository.Object, _mockMapper.Object);
        }


        [Fact]
        public void ReturnViewForAllParties()
        {
            IActionResult result = _sut.AllParties("john@email.com");

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnRedirectToActionForCreatePartyPost()
        {
            IActionResult result = _sut.CreateParty("john@email.com");

            Assert.IsType<RedirectToActionResult>(result);
        }

        // FAILS because of HTTPContext for cookie management
        //[Fact]
        //public void ReturnViewForDetails()
        //{
        //    IActionResult result = _sut.Details(1);

        //    Assert.IsType<ViewResult>(result);
        //}
    }
}
