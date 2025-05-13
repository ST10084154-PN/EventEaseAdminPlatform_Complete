
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseAdminPlatform.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public DateTime BookingDate { get; set; }

        public Venue Venue { get; set; }
        public Event Event { get; set; }
    }
}
