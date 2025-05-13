
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseAdminPlatform.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        public Venue Venue { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
