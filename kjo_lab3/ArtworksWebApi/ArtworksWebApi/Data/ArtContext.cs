using ArtworksWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworksWebApi.Data
{
    public class ArtContext : DbContext
    {
        public ArtContext(DbContextOptions<ArtContext> options)
            : base(options)
        {

        }

        public DbSet<ArtType> ArtTypes { get; set; }
        public DbSet<Artwork> Artworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ART");

            //Add a unique index to combination of Name and art type
            modelBuilder.Entity<Artwork>()
            .HasIndex(p => new { p.Name, p.ArtTypeID })
            .IsUnique();

            modelBuilder.Entity<ArtType>()
                .HasMany(p => p.Artworks)
                .WithOne(d => d.ArtType)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique Name
            modelBuilder.Entity<Artwork>()
                .HasIndex(a => a.Name)
                .IsUnique();

            //// Lab 3-1 Unique - combination of Name and ArtTypeID
            //modelBuilder.Entity<Artwork>()
            //    .HasIndex(a => new { a.Name, a.ArtTypeID })
            //    .IsUnique();

        }
    }
}
