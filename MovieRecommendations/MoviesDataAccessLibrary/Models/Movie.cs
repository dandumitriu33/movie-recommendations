using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public int ReleaseYear { get; set; }
    }
}
