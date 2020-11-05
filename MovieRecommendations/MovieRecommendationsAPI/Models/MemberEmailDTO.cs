using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationsAPI.Models
{
    public class MemberEmailDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
