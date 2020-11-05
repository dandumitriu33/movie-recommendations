using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName =("varchar(150)"))]
        public string Title { get; set; }
        [Required]
        public int LengthInMinutes { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        [Required]
        [Column(TypeName = ("varchar(20)"))]
        public string MainGenre { get; set; }
        [Column(TypeName = ("varchar(20)"))]
        public string SubGenre1 { get; set; }
        [Column(TypeName = ("varchar(20)"))]
        public string SubGenre2 { get; set; }
    }
}
