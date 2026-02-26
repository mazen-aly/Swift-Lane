using System;
using System.Collections.Generic;

namespace Entities
{
    public class PaymentMethod : BaseEntity
    {
        public string? MethodName { get; set; }

        public double? Commission { get; set; }

        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }

}
