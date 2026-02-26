using System;
using System.Collections.Generic;

namespace Entities
{
    public class UserReceiver : BaseEntity
    {
        public Guid UserId { get; set; }

        public string ReceiverName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public Guid CityId { get; set; }

        public string Address { get; set; } = null!;

        public City City { get; set; } = null!;

        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }

}

