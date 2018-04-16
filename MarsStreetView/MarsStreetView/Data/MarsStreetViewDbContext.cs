using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsStreetView.Models;
using Microsoft.EntityFrameworkCore;

namespace MarsStreetView.Data
{
    public class MarsStreetViewDbContext : DbContext
    {
        public MarsStreetViewDbContext(DbContextOptions<MarsStreetViewDbContext> options) : base(options)
        {
        }

        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<FavoriteList> FavoriteList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.List)
                .WithMany(l => l.Favorites)
                .IsRequired();

            modelBuilder.Entity<FavoriteList>()
                .HasMany(l => l.Favorites)
                .WithOne(f => f.List)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
