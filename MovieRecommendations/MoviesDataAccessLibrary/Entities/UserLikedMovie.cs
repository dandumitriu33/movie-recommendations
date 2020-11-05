using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class UserLikedMovie
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        public int Score { get; set; }
    }
}
