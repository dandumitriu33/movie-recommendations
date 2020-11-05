using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class GenreCount
    {
        [Required]
        [Column(TypeName = ("varchar(20)"))]
        public string GenreName { get; set; }
        public int Count { get; set; }
        [Required]
        [Column(TypeName = ("varchar(20)"))]
        public string Color { get; set; }
    }
}
