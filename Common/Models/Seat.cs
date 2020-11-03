using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Seat
    {
        public int SeatId { get; set; }

        [Required]
        public int SeatNumber { get; set; }
        
        public SeatState SeatState{ get;set;}

        
        public Flight Flight { get; set; }
    }
}
