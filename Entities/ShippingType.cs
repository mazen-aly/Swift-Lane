using System;
using System.Collections.Generic;

namespace Entities
{
    public class ShippingType : BaseEntity
    {
        public string ShippingTypeName { get; set; }

        public double ShippingFactor { get; set; }

        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }

}

