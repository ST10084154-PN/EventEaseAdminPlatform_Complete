using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Event type is required")]
        [StringLength(50)]
        public string EventType { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [FutureDate(ErrorMessage = "Event date must be in the future")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        public string? ImageUrl { get; set; } // For Azure Blob Storage

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }
    }

    // Custom validation attribute
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}
