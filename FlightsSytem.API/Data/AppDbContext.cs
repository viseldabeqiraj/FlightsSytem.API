using FlightSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole,string> //IdentityDbContext<ApplicationUser>//
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Flight> Flights { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            base.OnModelCreating(modelBuilder);

        }

    }
}
