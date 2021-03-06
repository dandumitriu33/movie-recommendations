﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class History
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }
        [Required]
        public int MovieId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
