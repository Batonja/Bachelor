using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models.Airline
{
    public class AirlineDestination
    {
        [Key,Column(Order = 1)]
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        [Key, Column(Order = 2)]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
    }
}
