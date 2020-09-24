using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Length in minutes")]
        public int LengthInMinutes { get; set; }
        [Required]
        [Display(Name = "Release year")]
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
    }
}
