using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class FlightLuggage
    {
        public int FlightLuggageId { get; set; }
        
        public FlightLuggageType FlightLuggageType { get; set; }
        [Required]
        [Column(TypeName ="decimal(6,2)")]
        public decimal Price { get; set; }

        

    }
}
