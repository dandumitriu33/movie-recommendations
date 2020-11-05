using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationsAPI.Models
{
    public class GenreCountDTO
    {
        public string GenreName { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
    }
}
