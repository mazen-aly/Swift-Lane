using System.Collections.Generic;

namespace Entities
{
    public class SubscriptionPackage : BaseEntity
    {
        public string PackageName { get; set; } = null!;

        public int ShippimentCount { get; set; }

        public double NumberOfKiloMeters { get; set; }

        public double TotalWeight { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    }

}

