using System;
using System.Collections.Generic;

namespace Entities
{
    public class Shipment : BaseEntity
    {
        public DateTime ShippingDate { get; set; }

        public Guid UserSenderId { get; set; }

        public Guid UserReceiverId { get; set; }

        public Guid ShippingTypeId { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public decimal PackageValue { get; set; }

        public decimal ShippingRate { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public Guid? UserSubscriptionId { get; set; }

        public double? TrackingNumber { get; set; }

        public Guid? ReferenceId { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public UserReceiver UserReceiver { get; set; } = null!;

        public UserSender UserSender { get; set; } = null!;

        public ShippingType ShippingType { get; set; } = null!;

        public ICollection<ShipmentStatus> ShipmentStatuses { get; set; } = new List<ShipmentStatus>();
    }
}
