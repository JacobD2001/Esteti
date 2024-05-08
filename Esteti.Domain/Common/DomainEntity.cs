using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Domain.Common
{
    public abstract class DomainEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
