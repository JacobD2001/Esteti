using Esteti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Domain.Entities
{
    public class Account : DomainEntity
    {
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();
    }
}
