using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Bookings
      /*  [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.TodoItems.ToListAsync();
        }*/

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(string id)
        {
            var booking = await _context.TodoItems.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(string id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.TodoItems.Add(booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            var booking = await _context.TodoItems.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(string id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
