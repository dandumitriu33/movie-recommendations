using AutoMapper;
using MovieRecommendationsAPI.Models;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationsAPI.Profiles
{
    public class MovieRecommendationsAPIProfiles : Profile
    {
        public MovieRecommendationsAPIProfiles()
        {
            CreateMap<Movie, MovieDTO>()
                .ReverseMap();
            CreateMap<Party, PartyDTO>()
                .ReverseMap();
            CreateMap<PartyMember, PartyMemberDTO>()
                .ReverseMap();
        }
    }
}
