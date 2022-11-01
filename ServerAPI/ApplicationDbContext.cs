using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI
{
    // public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieClient>()
                .HasKey(x => new { x.MovieId, x.ClientId});

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<MovieClient> MovieClients { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
