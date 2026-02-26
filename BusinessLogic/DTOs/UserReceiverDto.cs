using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs
{
    public class UserReceiverDto : BaseDto
    {
        public Guid UserId { get; set; }

        public string ReceiverName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public Guid CityId { get; set; }

        public string Address { get; set; } = null!;
    }
}
