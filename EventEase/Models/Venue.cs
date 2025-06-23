using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Venue
    {
        public int VenueId { get; set; }

        [Required]
        [StringLength(200)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(300)]
        public string Location { get; set; }

        public int Capacity { get; set; }

        public string ImageUrl { get; set; }

        // New Availability property
        public bool Availability { get; set; }
    }
}
