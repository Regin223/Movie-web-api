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
        /// <summary>
        /// The class <c>MovieCharacterDBContext</c> is used for setting up a database. Overriding the OnModelCreating to seed the database data.
        /// </summary>
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

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Movies)
                .WithMany(m => m.Characters).UsingEntity<Dictionary<string, object>>("CharacterMovie",
                l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                lr => 
                {
                    lr.HasKey("CharacterId", "MovieId");
                    lr.HasData(
                            new {CharacterId = 1, MovieId = 1},
                            new {CharacterId = 2, MovieId = 1},
                            new {CharacterId = 3, MovieId = 1},
                            new {CharacterId = 4, MovieId = 2}
                        );
                });

        }
    }
}
