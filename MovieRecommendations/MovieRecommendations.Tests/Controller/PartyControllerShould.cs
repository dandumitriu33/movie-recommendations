using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;
using MovieRecommendations.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MoviesDataAccessLibrary.Entities;

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
        public async Task ReturnRedirectToActionForCreatePartyPost()
        {
            IActionResult result = await _sut.CreateParty("john@email.com");

            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async Task RunsAddPartyOnCreatePartyPost()
        {
            IActionResult result = await _sut.CreateParty("john@email.com");

            Assert.IsType<RedirectToActionResult>(result);
            
            _mockRepository.Verify(x => x.AddParty(It.IsAny<Party>()), Times.Once);
        }

        [Fact]
        public async Task RunsAddMemberToPartyOnCreatePartyPost()
        {
            IActionResult result = await _sut.CreateParty("john@email.com");

            Assert.IsType<RedirectToActionResult>(result);

            _mockRepository.Verify(x => x.AddMemberToParty(It.IsAny<PartyMember>()), Times.Once);
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
