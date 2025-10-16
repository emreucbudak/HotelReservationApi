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
    public class HowFarSpecialPlacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HowFarSpecialPlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HowFarSpecialPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HowFarSpecialPlace>>> GetHowFarSpecialPlaces()
        {
            return await _context.HowFarSpecialPlaces.ToListAsync();
        }

        // GET: api/HowFarSpecialPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HowFarSpecialPlace>> GetHowFarSpecialPlace(int id)
        {
            var howFarSpecialPlace = await _context.HowFarSpecialPlaces.FindAsync(id);

            if (howFarSpecialPlace == null)
            {
                return NotFound();
            }

            return howFarSpecialPlace;
        }

        // PUT: api/HowFarSpecialPlaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHowFarSpecialPlace(int id, HowFarSpecialPlace howFarSpecialPlace)
        {
            if (id != howFarSpecialPlace.Id)
            {
                return BadRequest();
            }

            _context.Entry(howFarSpecialPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HowFarSpecialPlaceExists(id))
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

        // POST: api/HowFarSpecialPlaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HowFarSpecialPlace>> PostHowFarSpecialPlace(HowFarSpecialPlace howFarSpecialPlace)
        {
            _context.HowFarSpecialPlaces.Add(howFarSpecialPlace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHowFarSpecialPlace", new { id = howFarSpecialPlace.Id }, howFarSpecialPlace);
        }

        // DELETE: api/HowFarSpecialPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHowFarSpecialPlace(int id)
        {
            var howFarSpecialPlace = await _context.HowFarSpecialPlaces.FindAsync(id);
            if (howFarSpecialPlace == null)
            {
                return NotFound();
            }

            _context.HowFarSpecialPlaces.Remove(howFarSpecialPlace);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HowFarSpecialPlaceExists(int id)
        {
            return _context.HowFarSpecialPlaces.Any(e => e.Id == id);
        }
    }
}
