using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Destination
    {
        

        public int DestinationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        
        public decimal Longitude { get; set; }
        
        public decimal Latitude { get; set; }

    }
}
