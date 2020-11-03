using System.ComponentModel;


namespace Common.Models
{
    public class FlightOrder
    {
        public int FlightOrderId { get; set; }
        
        public Flight Flight { get; set; }
       
        public FlightTicket FlightTicket { get; set; }

        public Seat Seat { get; set; }
        
        public FlightLuggage FlightLuggage { get; set; }

        public User User { get; set; }
        [DefaultValue(true)]
        public bool Confirmed { get; set; }

    }
}
