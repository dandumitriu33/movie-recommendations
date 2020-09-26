using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class MoviesContext : IdentityDbContext<IdentityUser>
    {
        public MoviesContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<UserLikedMovie> CommunityLikes { get; set; }
    }
}
