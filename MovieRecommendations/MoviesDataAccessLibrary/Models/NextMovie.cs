using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class NextMovie
    {
        public int Id { get; set; }
        public int CurrentMovieId { get; set; }
        public int NextMovieId { get; set; }
        public int Score { get; set; }
    }
}
