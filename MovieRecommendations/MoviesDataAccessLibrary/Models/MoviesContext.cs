using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesDataAccessLibrary.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
