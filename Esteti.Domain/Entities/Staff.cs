using Esteti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Esteti.Domain.Enums.RolesEnum;

namespace Esteti.Domain.Entities
{
    public class Staff : DomainEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required StaffRoles Role { get; set; }
    }
}
