using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.DataHelpers;

namespace MovieCharactersAPI.Model
{
    public class MovieCharacterDbContext : DbContext
    {
        public MovieCharacterDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(SeederHelper.SeedCharacterHelper());
            modelBuilder.Entity<Movie>().HasData(SeederHelper.SeedMovieHelper());
            modelBuilder.Entity<Franchise>().HasData(SeederHelper.SeedFranchiseHelper());
        }
    }
}
