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
    public class HotelsPoliticiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelsPoliticiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelsPoliticies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelsPoliticy>>> GetHotelsPoliticies()
        {
            return await _context.HotelsPoliticies.ToListAsync();
        }

        // GET: api/HotelsPoliticies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsPoliticy>> GetHotelsPoliticy(int id)
        {
            var hotelsPoliticy = await _context.HotelsPoliticies.FindAsync(id);

            if (hotelsPoliticy == null)
            {
                return NotFound();
            }

            return hotelsPoliticy;
        }

        // PUT: api/HotelsPoliticies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelsPoliticy(int id, HotelsPoliticy hotelsPoliticy)
        {
            if (id != hotelsPoliticy.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelsPoliticy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelsPoliticyExists(id))
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

        // POST: api/HotelsPoliticies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelsPoliticy>> PostHotelsPoliticy(HotelsPoliticy hotelsPoliticy)
        {
            _context.HotelsPoliticies.Add(hotelsPoliticy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelsPoliticy", new { id = hotelsPoliticy.Id }, hotelsPoliticy);
        }

        // DELETE: api/HotelsPoliticies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelsPoliticy(int id)
        {
            var hotelsPoliticy = await _context.HotelsPoliticies.FindAsync(id);
            if (hotelsPoliticy == null)
            {
                return NotFound();
            }

            _context.HotelsPoliticies.Remove(hotelsPoliticy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelsPoliticyExists(int id)
        {
            return _context.HotelsPoliticies.Any(e => e.Id == id);
        }
    }
}
