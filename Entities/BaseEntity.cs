using System;
using System.Collections.Generic;

namespace Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int CurrentState { get; set; }
    }
}
