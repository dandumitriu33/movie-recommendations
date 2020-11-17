using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRecommendationsAPI.Controllers;
using MovieRecommendationsAPI.Models;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRecommendations.Tests.Controller
{
    public class APIPartiesControllerShould
    {
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PartiesController _sut;

        public APIPartiesControllerShould()
        {
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new PartiesController(_mockRepository.Object, _mockMapper.Object);
        }


        [Fact]
        public void ReturnDefaultGetValues()
        {
            string result = _sut.Get();

            Assert.Equal("Value 1", result);
        }

        [Fact]
        public async Task ReturnBadRequestOnPostInvalid()
        {
            _sut.ModelState.AddModelError("x", "Test error.");
            IActionResult result = await _sut.Post(new MovieRecommendationsAPI.Models.PartyDTO());
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Bad request.", badRequestResult.Value);
        }

        //[Fact]
        //public async Task ReturnOkResponseOnPostValid()
        //{
        //    PartyMember returnedPartyMember = new PartyMember() 
        //    {
        //        Id = 1,
        //        PartyId = 1,
        //        Email = "john@email.com"
        //    };

        //    PartyDTO partyDTO = new PartyDTO()
        //    {
        //        Name = "Jim's party",
        //        CreatorEmail = "Jim@email.com"
        //    };
        //    PartyDTO tempPartyDTO = null;
        //    Party party = new Party
        //    {
        //        Name = "Jim's party",
        //        CreatorEmail = "Jim@email.com"
        //    };
        //    Party tempParty = null;
        //    _mockRepository.Setup(x => x.AddMemberToParty(It.IsAny<PartyMember>()))
        //        .Returns(Task.CompletedTask)
        //        .Callback<PartyDTO>(y => tempPartyDTO = y);
        //    _mockMapper.Setup(x => x.Map<Party>(It.IsAny<PartyDTO>()))
        //        .Returns(party)
        //        .Callback<Party>(x => tempParty = x);


        //    IActionResult result = await _sut.Post(partyDTO);
        //    var okRequestResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal("Party \"\" was created successfully.", okRequestResult.Value);
        //}


        [Fact]
        public async Task ReturnBadRequestOnAddMemberToParty()
        {
            // for the temp unused API
            _sut.ModelState.AddModelError("x", "Test error.");
            IActionResult result = await _sut.AddMemberToParty(new PartyMemberDTO());
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Bad request.", badRequestResult.Value);
        }
    }
}
