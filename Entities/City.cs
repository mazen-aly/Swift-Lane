using System;
using System.Collections.Generic;

namespace Entities
{
    public class City : BaseEntity
    {
        public string? CityName { get; set; }

        public Guid CountryId { get; set; }

        public Country Country { get; set; } = null!;

        public ICollection<UserReceiver> UserReceivers { get; set; } = new List<UserReceiver>();

        public ICollection<UserSender> UserSenders { get; set; } = new List<UserSender>();
    }

}
