using AutoMapper;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Profiles
{
    public class MovieRecommendationsProfiles : Profile
    {
        public MovieRecommendationsProfiles()
        {
            CreateMap<Movie, MovieViewModel>()
                .ForMember(vm => vm.Title, o => o.MapFrom(s => s.Title))
                .ReverseMap();
        }
    }
}
