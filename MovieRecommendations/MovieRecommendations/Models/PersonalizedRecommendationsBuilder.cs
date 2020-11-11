﻿using MovieRecommendations.Interfaces;
using MoviesDataAccessLibrary.Entities;
using System.Collections.Generic;

namespace MovieRecommendations.Models
{
    public class PersonalizedRecommendationsBuilder : IPersonalizedRecommendationsBuilder
    {
        private const int personalizedRecommendationsLength = 10;
        public List<Movie> Build(List<Movie> rabbitHoleSuggestions, List<Movie> communityBasedSuggestions, List<Movie> historyBasedSuggestions)
        {
            int personalizedRecommendationsLength = 10;
            // arranging the history, community and rabbitHole suggestions in result 1RH - 1Com - 8History
            List<Movie> output = new List<Movie>();
            if (rabbitHoleSuggestions.Count > 0)
            {
                output.Add(rabbitHoleSuggestions[0]);
                personalizedRecommendationsLength--;
            }
            if (communityBasedSuggestions.Count > 0)
            {
                output.Add(communityBasedSuggestions[0]);
                personalizedRecommendationsLength--;
            }
            if (historyBasedSuggestions.Count > 0)
            {
                for (int i = 0; i < personalizedRecommendationsLength; i++)
                {
                    output.Add(historyBasedSuggestions[i]);
                }
            }
            return output;
        }
    }
}
