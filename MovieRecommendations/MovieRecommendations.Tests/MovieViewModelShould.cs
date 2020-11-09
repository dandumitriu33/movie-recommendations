using MovieRecommendations.ViewModels;
using System;
using Xunit;

namespace MovieRecommendations.Tests
{
    public class MovieViewModelShould
    {
        [Fact]
        public void BeNotNullOnCreation()
        {
            MovieViewModel movieVM = new MovieViewModel();

        }
    }
}
