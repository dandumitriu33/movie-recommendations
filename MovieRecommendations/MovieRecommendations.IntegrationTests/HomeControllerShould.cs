using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Xunit.Abstractions;

namespace MovieRecommendations.IntegrationTests
{
    public class HomeControllerShould
    {
        private const string AntiForgeryFieldName = "__AFTField";
        private const string AntiForgeryCookieName = "AFTCookie";
        private readonly ITestOutputHelper _output;

        public HomeControllerShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task RenderTheAddMoviePage()
        {
            // CONFIGURATION FOR LOCAL DB - switch in Startup to DEVELOPMENT

            //// use configuration builder and copy over appsettings.json w/ always copy prop
            //// from MovieRecommendations web project
            //// this is because the Configuration setup in startup here can't reach the configuration file there and get the DB connection string
            //var configurationBuilder = new ConfigurationBuilder()
            //                    .AddJsonFile("appsettings.json");
            //var builder = new WebHostBuilder()
            //                    .UseContentRoot(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations")
            //                    .UseEnvironment("Development")
            //                    .UseConfiguration(configurationBuilder.Build())
            //                    .UseStartup<MovieRecommendations.Startup>();

            // CONFIGURATION FOR IN MEMORY DB - switch in Startup to TESTING
            
            var builder = new WebHostBuilder()
                                .UseContentRoot(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations")
                                .UseEnvironment("Development")
                                .UseStartup<MovieRecommendations.Startup>();

            var server = new TestServer(builder);
            var client = server.CreateClient();

            var response = await client.GetAsync("Home/AddMovie");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Add a movie to the database", responseString);
        }

        [Fact]
        public async Task NotAcceptANewMovieWithoutTitle()
        {
            
            var builder = new WebHostBuilder()
                                .UseContentRoot(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations")
                                .UseEnvironment("Development")
                                .UseStartup<MovieRecommendations.Startup>()
                                .ConfigureServices( x =>
                                {
                                    x.AddAntiforgery(t =>
                                    {
                                        t.Cookie.Name = AntiForgeryCookieName;
                                        t.FormFieldName = AntiForgeryFieldName;
                                    });
                                });

            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Get initial response that contains the anti forgery tokens
            HttpResponseMessage initialResponse = await client.GetAsync("Home/AddMovie");

            string antiForgeryCookieValue = ExtractAntiForgeryCookieValueFrom(initialResponse);

            string antiForgeryToken = ExtractAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "Home/AddMovie");
            postRequest.Headers.Add("Cookie",
                new CookieHeaderValue(AntiForgeryCookieName,
                                      antiForgeryCookieValue).ToString());

            var formData = new Dictionary<string, string>
            {
                {AntiForgeryFieldName, antiForgeryToken },
                // title missing
                { "LengthInMinutes", "121" },
                { "Rating", "3.4" },
                { "ReleaseYear", "2003" },
                { "MainGenre", "Comedy" },
                { "SubGenre1", "Adventure" },
                { "SubGenre2", "Crime" }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await client.SendAsync(postRequest);

            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();

            // the expectation is for validation to kick in and generate this warning
            Assert.Contains("The movie title is required.", responseString);
        }

        private static string ExtractAntiForgeryCookieValueFrom(HttpResponseMessage response)
        {
            string antiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(x => x.Contains(AntiForgeryCookieName));

            if (antiForgeryCookie is null)
            {
                throw new ArgumentException(
                    $"Cookie '{AntiForgeryCookieName}' not found in HTTP response",
                    nameof(response));
            }

            string antiForgeryCookieValue = SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();

            return antiForgeryCookieValue;
        }

        private string ExtractAntiForgeryToken(string htmlBody)
        {

            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");
            _output.WriteLine(htmlBody);
            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' not found in HTML", nameof(htmlBody));
        }
    }
}
