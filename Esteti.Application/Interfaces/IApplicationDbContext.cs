using Esteti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Staff> Staff { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountUser> AccountUsers { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
