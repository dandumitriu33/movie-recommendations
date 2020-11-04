using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesDataAccessLibrary.Entities
{
    public class PartyMember
    {
        public int Id { get; set; }
        [Required]
        public int PartyId { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }
    }
}
