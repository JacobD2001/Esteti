using Esteti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Domain.Entities
{
    public class User : DomainEntity
    {
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();

        //to do - user should have role but than we need to adjust the staff entity??
    }
}
