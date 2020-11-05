using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRecommendations.ViewModels;

namespace MovieRecommendations.Models
{
    public class AcceptableWatchHistoryMovieModel : MovieViewModel
    {
        public DateTime DateAddedToHistory { get; set; }
    }
}
