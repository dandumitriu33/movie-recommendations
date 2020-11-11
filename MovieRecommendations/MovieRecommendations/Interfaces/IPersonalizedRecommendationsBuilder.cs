using MoviesDataAccessLibrary.Entities;
using System.Collections.Generic;

namespace MovieRecommendations.Interfaces
{
    public interface IPersonalizedRecommendationsBuilder
    {
        List<Movie> Build(List<Movie> rabbitHoleSuggestions, List<Movie> communityBasedSuggestions, List<Movie> historyBasedSuggestions);
    }
}