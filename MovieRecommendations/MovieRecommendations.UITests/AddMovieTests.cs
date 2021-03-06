using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

        [Fact]
        public void ShouldValidateAddMovieDetailsTitleMissing()
        {
            _driver.Navigate().GoToUrl("http://localhost:51264/home/addmovie");

            // Title left out

            IWebElement lengthInMinutes = _driver.FindElement(By.Name("LengthInMinutes"));
            lengthInMinutes.SendKeys("121");

            DelayForDemo();

            _driver.FindElement(By.Name("ReleaseYear")).SendKeys("2016");

            DelayForDemo();

            IWebElement rating = _driver.FindElement(By.Name("Rating"));
            rating.SendKeys("6.5");

            DelayForDemo();

            // including select elements https://stackoverflow.com/questions/5278281/how-to-select-an-option-from-drop-down-using-selenium-webdriver-c
            IWebElement mainGenre = _driver.FindElement(By.Name("MainGenre"));
            var mainGenreElement = new SelectElement(mainGenre);
            mainGenreElement.SelectByText("Comedy");

            DelayForDemo();

            IWebElement subGenre1 = _driver.FindElement(By.Name("SubGenre1"));
            var subGenre1Element = new SelectElement(subGenre1);
            subGenre1Element.SelectByText("Adventure");

            DelayForDemo();

            IWebElement subGenre2 = _driver.FindElement(By.Name("SubGenre2"));
            var subGenre2Element = new SelectElement(subGenre2);
            subGenre2Element.SelectByText("Mystery");

            DelayForDemo();

            _driver.FindElement(By.Name("SubmitNewMovie")).Click();

            DelayForDemo();

            Assert.Equal("Add movie - MovieRecommendations", _driver.Title);

            IWebElement firstErrorMessage = _driver.FindElement(By.CssSelector(".validation-summary-errors ul > li"));

            Assert.Equal("The movie title is required.", firstErrorMessage.Text);
        }

        [Fact]
        public void ShouldValidateAddMovieDetailsLengthInMinutesMissing()
        {
            _driver.Navigate().GoToUrl("http://localhost:51264/home/addmovie");

            IWebElement title = _driver.FindElement(By.Name("Title"));
            title.SendKeys("Fun Test Movie");

            DelayForDemo();

            // LengthInMinutes left out

            _driver.FindElement(By.Name("ReleaseYear")).SendKeys("2016");

            DelayForDemo();

            IWebElement rating = _driver.FindElement(By.Name("Rating"));
            rating.SendKeys("6.5");

            DelayForDemo();

            // including select elements https://stackoverflow.com/questions/5278281/how-to-select-an-option-from-drop-down-using-selenium-webdriver-c
            IWebElement mainGenre = _driver.FindElement(By.Name("MainGenre"));
            var mainGenreElement = new SelectElement(mainGenre);
            mainGenreElement.SelectByText("Comedy");

            DelayForDemo();

            IWebElement subGenre1 = _driver.FindElement(By.Name("SubGenre1"));
            var subGenre1Element = new SelectElement(subGenre1);
            subGenre1Element.SelectByText("Adventure");

            DelayForDemo();

            IWebElement subGenre2 = _driver.FindElement(By.Name("SubGenre2"));
            var subGenre2Element = new SelectElement(subGenre2);
            subGenre2Element.SelectByText("Mystery");

            DelayForDemo();

            _driver.FindElement(By.Name("SubmitNewMovie")).Click();

            DelayForDemo();

            Assert.Equal("Add movie - MovieRecommendations", _driver.Title);

            IWebElement firstErrorMessage = _driver.FindElement(By.CssSelector(".validation-summary-errors ul > li"));

            Assert.Equal("The movie runtime (minutes) is required.", firstErrorMessage.Text);
        }

        [Fact]
        public void ShouldValidateAddMovieDetailsReleaseYearMissing()
        {
            _driver.Navigate().GoToUrl("http://localhost:51264/home/addmovie");

            IWebElement title = _driver.FindElement(By.Name("Title"));
            title.SendKeys("Fun Test Movie");

            IWebElement lengthInMinutes = _driver.FindElement(By.Name("LengthInMinutes"));
            lengthInMinutes.SendKeys("121");

            //_driver.FindElement(By.Name("ReleaseYear")).SendKeys("2016");

            IWebElement rating = _driver.FindElement(By.Name("Rating"));
            rating.SendKeys("6.5");

            // including select elements https://stackoverflow.com/questions/5278281/how-to-select-an-option-from-drop-down-using-selenium-webdriver-c
            IWebElement mainGenre = _driver.FindElement(By.Name("MainGenre"));
            var mainGenreElement = new SelectElement(mainGenre);
            mainGenreElement.SelectByText("Comedy");

            IWebElement subGenre1 = _driver.FindElement(By.Name("SubGenre1"));
            var subGenre1Element = new SelectElement(subGenre1);
            subGenre1Element.SelectByText("Adventure");

            IWebElement subGenre2 = _driver.FindElement(By.Name("SubGenre2"));
            var subGenre2Element = new SelectElement(subGenre2);
            subGenre2Element.SelectByText("Mystery");

            _driver.FindElement(By.Name("SubmitNewMovie")).Click();

            Assert.Equal("Add movie - MovieRecommendations", _driver.Title);

            IWebElement firstErrorMessage = _driver.FindElement(By.CssSelector(".validation-summary-errors ul > li"));

            Assert.Equal("The movie release year is required.", firstErrorMessage.Text);
        }

        [Fact]
        public void ShouldValidateAddMovieDetailsRatingMissing()
        {
            _driver.Navigate().GoToUrl("http://localhost:51264/home/addmovie");

            IWebElement title = _driver.FindElement(By.Name("Title"));
            title.SendKeys("Fun Test Movie");

            IWebElement lengthInMinutes = _driver.FindElement(By.Name("LengthInMinutes"));
            lengthInMinutes.SendKeys("121");

            _driver.FindElement(By.Name("ReleaseYear")).SendKeys("2016");

            //IWebElement rating = _driver.FindElement(By.Name("Rating"));
            //rating.SendKeys("6.5");

            // including select elements https://stackoverflow.com/questions/5278281/how-to-select-an-option-from-drop-down-using-selenium-webdriver-c
            IWebElement mainGenre = _driver.FindElement(By.Name("MainGenre"));
            var mainGenreElement = new SelectElement(mainGenre);
            mainGenreElement.SelectByText("Comedy");

            IWebElement subGenre1 = _driver.FindElement(By.Name("SubGenre1"));
            var subGenre1Element = new SelectElement(subGenre1);
            subGenre1Element.SelectByText("Adventure");

            IWebElement subGenre2 = _driver.FindElement(By.Name("SubGenre2"));
            var subGenre2Element = new SelectElement(subGenre2);
            subGenre2Element.SelectByText("Mystery");

            _driver.FindElement(By.Name("SubmitNewMovie")).Click();

            Assert.Equal("Add movie - MovieRecommendations", _driver.Title);

            IWebElement firstErrorMessage = _driver.FindElement(By.CssSelector(".validation-summary-errors ul > li"));

            Assert.Equal("The movie rating is required.", firstErrorMessage.Text);
        }

        [Fact]
        public void ShouldValidateEmailOnAddPartyMember()
        {
            // must be logged in with jimsmith@email.com
            _driver.Navigate().GoToUrl("https://localhost:44318/Account/Login");
            _driver.FindElement(By.Name("Email")).SendKeys("jimsmith@email.com");
            DelayForDemo();
            _driver.FindElement(By.Name("Password")).SendKeys("12345");
            DelayForDemo();
            DelayForDemo();
            _driver.FindElement(By.Name("SubmitLoginDetails")).Click();

            DelayForDemo();
            
            _driver.Navigate().GoToUrl("https://localhost:44318/party/details/33");

            IWebElement emailAddress = _driver.FindElement(By.Name("EmailAddress"));
            emailAddress.SendKeys("johnATemail.com");
            DelayForDemo();
            

            _driver.FindElement(By.Id("addMemberButton")).Click();

            DelayForDemo();

            Assert.Equal("Party details - MovieRecommendations", _driver.Title);

            IWebElement errorMessage = _driver.FindElement(By.Id("inlineFormInputEmail-error"));

            DelayForDemo();
            DelayForDemo();
            DelayForDemo();

            Assert.Equal("The email address field is not a valid email address.", errorMessage.Text);

        }

        /// <summary>
        /// Introduces a 1 second delay for demo purposes. The action happens to fast to observe otherwise.
        /// </summary>
        private static void DelayForDemo()
        {
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
