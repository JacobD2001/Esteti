using Esteti.Application.Interfaces;
using Esteti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Infrastructure.Persistence
{
    public class MainDbContext : DbContext, IApplicationDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
      
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this is for configurations if we want to add configurations in configuration folder but for now we are using it.
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
            modelBuilder.Entity<Staff>()
                .Property(s => s.Role)
                .HasConversion<string>();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(10, 2);

            base.ConfigureConventions(configurationBuilder);
        }

    }
}
