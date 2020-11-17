using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieRecommendations.IntegrationTests
{
    public class APIPartiesControllerShould : IClassFixture<APITestServerFixture>
    {
        private readonly APITestServerFixture _fixture;

        public APIPartiesControllerShould(APITestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetValidValue()
        {
            var response = await _fixture.Client.GetAsync("/api/parties/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Value 1", responseString);
        }
    }
}
