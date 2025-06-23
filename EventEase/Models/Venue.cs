using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Venue name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? ImageUrl { get; set; } // For Azure Blob Storage

        // Navigation property
        public ICollection<Event>? Events { get; set; }
    }
}
