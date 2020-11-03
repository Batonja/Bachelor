using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    [NotMapped]
    public class SearchObject
    {
        public List<decimal> PriceRange { get; set; }
        public Destination Destination { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
