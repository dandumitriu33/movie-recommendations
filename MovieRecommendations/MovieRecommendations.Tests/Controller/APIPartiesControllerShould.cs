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
using Xunit.Abstractions;

namespace MovieRecommendations.Tests.Controller
{
    public class APIPartiesControllerShould
    {
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PartiesController _sut;
        private readonly ITestOutputHelper _output;

        public APIPartiesControllerShould(ITestOutputHelper output)
        {
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new PartiesController(_mockRepository.Object, _mockMapper.Object);
            _output = output;
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
    }
}
