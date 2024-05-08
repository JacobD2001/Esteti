using Esteti.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Esteti.Domain.Entities
{
    public class Booking : DomainEntity
    {
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public required virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public required virtual Service Service { get; set; }

        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public required virtual Staff Staff { get; set; }

        public required DateOnly BookingDate { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
