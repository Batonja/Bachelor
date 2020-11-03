using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Airline
    {
        
        public int AirlineId { get; set; }
        
        public User Administrator { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<AirlineFlightLuggage> AvailableFlightLuggage { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<AirlineDestination> AirlineDestinations { get; set; }

    }
}
