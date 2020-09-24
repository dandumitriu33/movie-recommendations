using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        [Column(TypeName =("varchar(150)"))]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Length in minutes")]
        public int LengthInMinutes { get; set; }
        [Required]
        [Display(Name = "Release year")]
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        [Required]
        [Display(Name = "Main Genre")]
        [MaxLength(20)]
        public string MainGenre { get; set; }
        [Required]
        [Display(Name = "Sub Genre 1")]
        [MaxLength(20)]
        public string SubGenre1 { get; set; }
        [Required]
        [Display(Name = "Sub Genre 2")]
        [MaxLength(20)]
        public string SubGenre2 { get; set; }
    }
}
