using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThemePark.Models
{
    public class Ride
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int ThrillFactor { get; set; }
        public int VomitFactor { get; set; }
    }
}