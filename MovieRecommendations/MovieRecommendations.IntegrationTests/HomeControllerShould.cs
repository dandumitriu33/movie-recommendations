using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MovieRecommendations.IntegrationTests
{
    public class HomeControllerShould
    {
        [Fact]
        public async Task RenderTheAddMoviePage()
        {
            // use configuration builder and copy over appsettings.json w/ always copy prop
            // from MovieRecommendations web project
            // this is because the Configuration setup in startup here can't reach the configuration file there and get the DB connection string
            var configurationBuilder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json");
            var builder = new WebHostBuilder()
                                .UseContentRoot(@"C:\Users\Dan\Projects\movie-recommendations\MovieRecommendations\MovieRecommendations")
                                .UseEnvironment("Development")
                                .UseConfiguration(configurationBuilder.Build())
                                .UseStartup<MovieRecommendations.Startup>();

            var server = new TestServer(builder);
            var client = server.CreateClient();

            var response = await client.GetAsync("Home/AddMovie");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Add a movie to the database", responseString);
        }
    }
}
