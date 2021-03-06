﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class Party
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = ("varchar(50)"))]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string CreatorEmail { get; set; }
    }
}
