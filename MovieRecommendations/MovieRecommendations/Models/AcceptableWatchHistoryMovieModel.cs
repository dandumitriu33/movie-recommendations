using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.Models
{
    public class AcceptableWatchHistoryMovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string MainGenre { get; set; }
        public string SubGenre1 { get; set; }
        public string SubGenre2 { get; set; }
        public DateTime DateAddedToHistory { get; set; }
    }
}
