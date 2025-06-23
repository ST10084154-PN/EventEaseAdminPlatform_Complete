using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events with filtering
        public async Task<IActionResult> Index(int? eventTypeId, DateTime? startDate, DateTime? endDate, bool? venueAvailable)
        {
            var eventsQuery = _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .AsQueryable();

            if (eventTypeId.HasValue && eventTypeId.Value != 0)
                eventsQuery = eventsQuery.Where(e => e.EventTypeId == eventTypeId.Value);

            if (startDate.HasValue)
                eventsQuery = eventsQuery.Where(e => e.EventDate >= startDate.Value);

            if (endDate.HasValue)
                eventsQuery = eventsQuery.Where(e => e.EventDate <= endDate.Value);

            if (venueAvailable.HasValue)
                eventsQuery = eventsQuery.Where(e => e.Venue.Availability == venueAvailable.Value);

            ViewData["EventTypes"] = await _context.EventTypes.ToListAsync();

            return View(await eventsQuery.ToListAsync());
        }

        // Other CRUD methods remain unchanged
    }
}
