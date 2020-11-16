using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieRecommendations.Tests.Controller
{
    public class StatisticsControllerShould
    {
        [Fact]
        public void ReturnViewForIndex()
        {
            
            // sut = System Under Test
            var sut = new StatisticsController();

            IActionResult result = sut.Index();

            Assert.IsType<ViewResult>(result);
        }

    }
}
