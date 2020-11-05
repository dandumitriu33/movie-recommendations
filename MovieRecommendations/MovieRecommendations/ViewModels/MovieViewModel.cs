using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage ="The movie title must be less than 150 characters long.")]
        public string Title { get; set; }
        [Required(ErrorMessage ="The movie length is required.")]
        [Display(Name = "Length in minutes")]
        public int LengthInMinutes { get; set; }
        [Required]
        [Display(Name = "Release year")]
        public int ReleaseYear { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        [Display(Name = "Main genre")]
        [MaxLength(20, ErrorMessage = "The genre name must be less than 20 characters long.")]
        public string MainGenre { get; set; }
        [Display(Name = "Sub genre 1")]
        [MaxLength(20, ErrorMessage = "The genre name must be less than 20 characters long.")]
        public string SubGenre1 { get; set; }
        [Display(Name = "Sub genre 2")]
        [MaxLength(20, ErrorMessage = "The genre name must be less than 20 characters long.")]
        public string SubGenre2 { get; set; }
    }
}
