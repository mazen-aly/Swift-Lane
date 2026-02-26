using System;
using System.Collections.Generic;

namespace Entities
{
    public class ShipmentStatus : BaseEntity
    {
        public Guid? ShipmentId { get; set; }

        public string? Notes { get; set; }

        public Guid CarrierId { get; set; }

        public ShipmentCarrier Carrier { get; set; } = null!;

        public Shipment? Shipment { get; set; }
    }

}

