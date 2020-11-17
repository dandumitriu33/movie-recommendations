using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace MovieRecommendations.IntegrationTests
{
    public class APITestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public static readonly string AntiForgeryFieldName = "__AFTField";
        public static readonly string AntiForgeryCookieName = "AFTCookie";

        public APITestServerFixture()
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
                                .UseContentRoot(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendationsAPI")
                                .UseEnvironment("Development")
                                .UseStartup<MovieRecommendationsAPI.Startup>();
                                //.ConfigureServices(x =>
                                //{
                                //    x.AddAntiforgery(t =>
                                //    {
                                //        t.Cookie.Name = AntiForgeryCookieName;
                                //        t.FormFieldName = AntiForgeryFieldName;
                                //    });
                                //});
            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public async Task<(string fieldValue, string cookieValue)> ExtractAntiForgeryValues(HttpResponseMessage response)
        {
            return (ExtractAntiForgeryToken(await response.Content.ReadAsStringAsync()),
                                            ExtractAntiForgeryCookieValueFrom(response));
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
            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' not found in HTML", nameof(htmlBody));
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
