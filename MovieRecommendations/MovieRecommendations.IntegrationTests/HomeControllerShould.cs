using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MovieRecommendations.IntegrationTests
{
    public class HomeControllerShould : IClassFixture<TestServerFixture>
    {        
        private readonly TestServerFixture _fixture;
        private readonly ITestOutputHelper _output;

        public HomeControllerShould(TestServerFixture fixture,
                                    ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task RenderTheAddMoviePage()
        {
            var response = await _fixture.Client.GetAsync("Home/AddMovie");
            
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Add a movie to the database", responseString);
        }

        [Fact]
        public async Task NotAcceptANewMovieWithoutTitle()
        {
            string expectedMatch = "The movie was not added to the database. Please check the details and submit when ready.";

            // Get initial response that contains the anti forgery tokens
            HttpResponseMessage initialResponse = await _fixture.Client.GetAsync("Home/AddMovie");

            var antiForgeryValues = await _fixture.ExtractAntiForgeryValues(initialResponse);

            // create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "Home/AddMovie");
            postRequest.Headers.Add("Cookie",
                                new CookieHeaderValue(TestServerFixture.AntiForgeryCookieName,
                                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    {TestServerFixture.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                    //{ "Title", "Mem test movie"},
                    { "LengthInMinutes", "121" },
                    { "Rating", "3.4" },
                    { "ReleaseYear", "2003" },
                    { "MainGenre", "Comedy" },
                    { "SubGenre1", "Adventure" },
                    { "SubGenre2", "Crime" }
                };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _fixture.Client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains(expectedMatch, responseString);
        }

        [Fact]
        public async Task NotAcceptANewMovieWithoutLengthInMinutes()
        {
            // Required validation on the server https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0
            string expectedMatch = "The movie was not added to the database. Please check the details and submit when ready.";

            // Get initial response that contains the anti forgery tokens
            HttpResponseMessage initialResponse = await _fixture.Client.GetAsync("Home/AddMovie");

            var antiForgeryValues = await _fixture.ExtractAntiForgeryValues(initialResponse);

            // create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "Home/AddMovie");
            postRequest.Headers.Add("Cookie",
                                new CookieHeaderValue(TestServerFixture.AntiForgeryCookieName,
                                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    {TestServerFixture.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                    { "Title", "Mem test movie"},
                    //{ "LengthInMinutes", "121" },
                    { "Rating", "3.4" },
                    { "ReleaseYear", "2003" },
                    { "MainGenre", "Comedy" },
                    { "SubGenre1", "Adventure" },
                    { "SubGenre2", "Crime" }
                };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _fixture.Client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains(expectedMatch, responseString);
        }

        [Fact]
        public async Task NotAcceptANewMovieWithoutRating()
        {
            // Required validation on the server https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0
            string expectedMatch = "The movie was not added to the database. Please check the details and submit when ready.";

            // Get initial response that contains the anti forgery tokens
            HttpResponseMessage initialResponse = await _fixture.Client.GetAsync("Home/AddMovie");

            var antiForgeryValues = await _fixture.ExtractAntiForgeryValues(initialResponse);

            // create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "Home/AddMovie");
            postRequest.Headers.Add("Cookie",
                                new CookieHeaderValue(TestServerFixture.AntiForgeryCookieName,
                                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    {TestServerFixture.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                    { "Title", "Mem test movie"},
                    { "LengthInMinutes", "121" },
                    //{ "Rating", "3.4" },
                    { "ReleaseYear", "2003" },
                    { "MainGenre", "Comedy" },
                    { "SubGenre1", "Adventure" },
                    { "SubGenre2", "Crime" }
                };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _fixture.Client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains(expectedMatch, responseString);
        }

        [Fact]
        public async Task NotAcceptANewMovieWithoutReleaseYear()
        {
            // Required validation on the server https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0
            string expectedMatch = "The movie was not added to the database. Please check the details and submit when ready.";

            // Get initial response that contains the anti forgery tokens
            HttpResponseMessage initialResponse = await _fixture.Client.GetAsync("Home/AddMovie");

            var antiForgeryValues = await _fixture.ExtractAntiForgeryValues(initialResponse);

            // create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "Home/AddMovie");
            postRequest.Headers.Add("Cookie",
                                new CookieHeaderValue(TestServerFixture.AntiForgeryCookieName,
                                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    {TestServerFixture.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                    { "Title", "Mem test movie"},
                    { "LengthInMinutes", "121" },
                    { "Rating", "3.4" },
                    //{ "ReleaseYear", "2003" },
                    { "MainGenre", "Comedy" },
                    { "SubGenre1", "Adventure" },
                    { "SubGenre2", "Crime" }
                };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _fixture.Client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains(expectedMatch, responseString);
        }

    }
}
