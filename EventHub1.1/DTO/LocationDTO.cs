using System;
using System.Collections.Generic;
using System.Linq;
using EventHub1._1.Models;

namespace EventHub1._1.DTO
{
    public class LocationDTO
    {
        public LocationDTO(Location location)
        {
            LocationId = location.LocationId;
            Name = location.Name;
            Address = location.Address;
            Active = location.Active;
        }

        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
    }
}