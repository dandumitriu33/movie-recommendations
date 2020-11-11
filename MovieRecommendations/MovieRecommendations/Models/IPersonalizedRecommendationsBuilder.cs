using MoviesDataAccessLibrary.Entities;
using System.Collections.Generic;

namespace MovieRecommendations.Models
{
    public interface IPersonalizedRecommendationsBuilder
    {
        List<Movie> Build(List<Movie> historyBasedSuggestions, List<Movie> communityBasedSuggestions, List<Movie> rabbitHoleSuggestions);
    }
}