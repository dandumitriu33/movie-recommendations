using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class UserLikedMovie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Email { get; set; }
        [Required]
        public int MovieId { get; set; }
        public int Score { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
