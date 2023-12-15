using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EisntFlix.Models;

namespace EisntFlix.Data.Access.DbContext
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId,
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

            modelBuilder.Entity<Actor_Serie>().HasKey(ase => new
            {
                ase.ActorId,
                ase.SerieId,
            });

            modelBuilder.Entity<Actor_Serie>().HasOne(s => s.Serie).WithMany(ase => ase.Actors_Series).HasForeignKey(s => s.SerieId);
            modelBuilder.Entity<Actor_Serie>().HasOne(s => s.Actor).WithMany(ase => ase.Actors_Series).HasForeignKey(s => s.ActorId);


			base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Actor_Serie> Actors_Series  { get; set; }

		public DbSet<Streaming> Streamings { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Serie> Series { get; set; }

        //Orders related tables

        public DbSet<Order> Orders { get; set; }
        public DbSet <OrderItem> OrderItems { get; set; }

    }
}