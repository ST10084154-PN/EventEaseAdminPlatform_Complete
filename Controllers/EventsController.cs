
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseAdminPlatform.Data;
using EventEaseAdminPlatform.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EventEaseAdminPlatform.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = _context.Events.Include(e => e.Venue);
            return View(await events.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Venues"] = _context.Venues.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event evnt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evnt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Venues"] = _context.Venues.ToList();
            return View(evnt);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var evnt = await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);
            if (evnt == null) return NotFound();

            bool hasBookings = _context.Bookings.Any(b => b.EventId == id);
            if (hasBookings)
            {
                ViewBag.ErrorMessage = "Cannot delete event with active bookings.";
                return View("Error");
            }

            return View(evnt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evnt = await _context.Events.FindAsync(id);
            _context.Events.Remove(evnt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
