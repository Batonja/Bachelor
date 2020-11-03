using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Required]
        public Destination ToDestination { get; set; }
        
        public Destination FromDestination { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Column(TypeName ="decimal(5,2)")]
        public decimal TripLength { get; set; }
        
        public int NumOfChangeovers { get; set; }

        
        public Airline Airline { get; set; }

        public ICollection<FlightTicket> Tickets { get; set; }
        public ICollection<FlightOrder> FlightOrders { get; set; }
        public ICollection<Seat> Seats { get; set; }

    }
}
