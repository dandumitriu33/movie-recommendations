using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace MovieRecommendations.UITests
{
    public class AddMovieTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public AddMovieTests()
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void ShouldLoadAddMovieFormPage_SmokeTest()
        {
            _driver.Navigate().GoToUrl("http://localhost:51264/home/addmovie");

            Assert.Equal("Add movie - MovieRecommendations", _driver.Title);
        }


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
