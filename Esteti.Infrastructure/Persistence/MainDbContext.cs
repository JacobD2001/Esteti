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

        //this is for configurations if we want to add configurations in configuration folder but for now we are using it.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
            modelBuilder.Entity<Staff>()
                .Property(s => s.Role)
                .HasConversion<string>();
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<decimal>().HavePrecision(18, 4);

        //    base.ConfigureConventions(configurationBuilder);
        //}

    }
}
