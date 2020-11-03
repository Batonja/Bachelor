using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class AirlineFlightLuggage
    {

        public AirlineFlightLuggage() { }

        public AirlineFlightLuggage(AirlineFlightLuggage afl)
        {
            FlightLuggage = afl.FlightLuggage;
            FlightLuggageId = afl.FlightLuggageId;
            Airline = afl.Airline;
            AirlineId = afl.AirlineId;
        }

        public FlightLuggage FlightLuggage { get; set; }
        [Key, Column(Order = 2)]
        public int FlightLuggageId { get; set; }

        public Airline Airline { get; set; }
        [Key, Column(Order = 1)]
        public int AirlineId { get; set; }
    }
}
