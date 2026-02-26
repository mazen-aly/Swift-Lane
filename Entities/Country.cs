using System;
using System.Collections.Generic;

namespace Entities
{
    public class Country : BaseEntity
    {
        public string? CountryName { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }

}