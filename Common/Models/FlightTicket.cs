using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class FlightTicket
    {
        public int FlightTicketId { get; set; }
        
        public FlightTicketType Type { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        [Required]
        public decimal Price { get; set; }
        

    }
}
