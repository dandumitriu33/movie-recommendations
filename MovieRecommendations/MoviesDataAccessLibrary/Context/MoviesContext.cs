using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesDataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Context
{
    public class MoviesContext : IdentityDbContext<IdentityUser>
    {
        public MoviesContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<UserLikedMovie> CommunityLikes { get; set; }
        public DbSet<NextMovie> NextMovies { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<PartyMember> PartyMembers { get; set; }
        public DbSet<PartyChoice> PartyChoices { get; set; }
    }
}
