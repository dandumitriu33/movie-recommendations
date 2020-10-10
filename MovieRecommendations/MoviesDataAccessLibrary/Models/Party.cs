using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class Party
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = ("varchar(50)"))]
        public string Name { get; set; }
    }
}
