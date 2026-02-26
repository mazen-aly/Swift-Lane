using System;
using System.Collections.Generic;

namespace Entities
{
    public class UserSubscription : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid PackageId { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public SubscriptionPackage Package { get; set; } = null!;
    }
}
