using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationsAPI.Models
{
    public class PartyMemberDTO
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
