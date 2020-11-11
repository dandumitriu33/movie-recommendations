using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Models
{
    public class PersonalizedRecommendationsBuilder : IPersonalizedRecommendationsBuilder
    {
        private const int personalizedRecommendationsLength = 10;
        public List<Movie> Build(List<Movie> historyBasedSuggestions, List<Movie> communityBasedSuggestions, List<Movie> rabbitHoleSuggestions)
        {
            // arranging the history, community and rabbitHole suggestions in result 1RH - 1Com - 8History
            List<Movie> output = new List<Movie>();
            output.Add(rabbitHoleSuggestions[0]);
            output.Add(communityBasedSuggestions[0]);
            for (int i = 0; i < personalizedRecommendationsLength - 2; i++)
            {
                output.Add(historyBasedSuggestions[i]);
            }
            return output;
        }
    }
}
