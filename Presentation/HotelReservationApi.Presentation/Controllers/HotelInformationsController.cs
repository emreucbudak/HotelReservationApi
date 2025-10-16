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
    public class HotelInformationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelInformation>>> GetHotelInformations()
        {
            return await _context.HotelInformations.ToListAsync();
        }

        // GET: api/HotelInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelInformation>> GetHotelInformation(int id)
        {
            var hotelInformation = await _context.HotelInformations.FindAsync(id);

            if (hotelInformation == null)
            {
                return NotFound();
            }

            return hotelInformation;
        }

        // PUT: api/HotelInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelInformation(int id, HotelInformation hotelInformation)
        {
            if (id != hotelInformation.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelInformationExists(id))
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

        // POST: api/HotelInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelInformation>> PostHotelInformation(HotelInformation hotelInformation)
        {
            _context.HotelInformations.Add(hotelInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelInformation", new { id = hotelInformation.Id }, hotelInformation);
        }

        // DELETE: api/HotelInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelInformation(int id)
        {
            var hotelInformation = await _context.HotelInformations.FindAsync(id);
            if (hotelInformation == null)
            {
                return NotFound();
            }

            _context.HotelInformations.Remove(hotelInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelInformationExists(int id)
        {
            return _context.HotelInformations.Any(e => e.Id == id);
        }
    }
}
