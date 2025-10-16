using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAdressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelAdressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelAdresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelAdress>>> GetHotelAdresses()
        {
            return await _context.HotelAdresses.ToListAsync();
        }

        // GET: api/HotelAdresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelAdress>> GetHotelAdress(int id)
        {
            var hotelAdress = await _context.HotelAdresses.FindAsync(id);

            if (hotelAdress == null)
            {
                return NotFound();
            }

            return hotelAdress;
        }

        // PUT: api/HotelAdresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelAdress(int id, HotelAdress hotelAdress)
        {
            if (id != hotelAdress.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelAdress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelAdressExists(id))
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

        // POST: api/HotelAdresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelAdress>> PostHotelAdress(HotelAdress hotelAdress)
        {
            _context.HotelAdresses.Add(hotelAdress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelAdress", new { id = hotelAdress.Id }, hotelAdress);
        }

        // DELETE: api/HotelAdresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelAdress(int id)
        {
            var hotelAdress = await _context.HotelAdresses.FindAsync(id);
            if (hotelAdress == null)
            {
                return NotFound();
            }

            _context.HotelAdresses.Remove(hotelAdress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelAdressExists(int id)
        {
            return _context.HotelAdresses.Any(e => e.Id == id);
        }
    }
}
