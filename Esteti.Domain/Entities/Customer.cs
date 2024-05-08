using Esteti.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Domain.Entities
{
    public class Customer : DomainEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
