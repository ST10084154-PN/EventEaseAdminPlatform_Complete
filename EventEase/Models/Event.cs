using System;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(200)]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        // Foreign key to Venue
        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        // Foreign key to EventType (new)
        [Required]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
