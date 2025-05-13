
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseAdminPlatform.Data;
using EventEaseAdminPlatform.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EventEaseAdminPlatform.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var bookings = _context.Bookings.Include(b => b.Event).Include(b => b.Venue);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookings = bookings.Where(b =>
                    b.Event.EventName.Contains(searchTerm) ||
                    b.Venue.VenueName.Contains(searchTerm));
            }

            return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Events"] = _context.Events.ToList();
            ViewData["Venues"] = _context.Venues.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            bool isDuplicate = _context.Bookings.Any(b =>
                b.VenueId == booking.VenueId &&
                b.BookingDate == booking.BookingDate);

            if (isDuplicate)
            {
                ModelState.AddModelError("", "This venue is already booked on the selected date.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Events"] = _context.Events.ToList();
            ViewData["Venues"] = _context.Venues.ToList();
            return View(booking);
        }
    }
}
