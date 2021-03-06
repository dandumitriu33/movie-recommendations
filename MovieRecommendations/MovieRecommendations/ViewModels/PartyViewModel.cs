﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendations.ViewModels
{
    public class PartyViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string CreatorEmail { get; set; }
    }
}
