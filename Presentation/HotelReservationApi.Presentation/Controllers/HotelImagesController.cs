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
    public class HotelImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelImages>>> GetHotelImages()
        {
            return await _context.HotelImages.ToListAsync();
        }

        // GET: api/HotelImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelImages>> GetHotelImages(int id)
        {
            var hotelImages = await _context.HotelImages.FindAsync(id);

            if (hotelImages == null)
            {
                return NotFound();
            }

            return hotelImages;
        }

        // PUT: api/HotelImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelImages(int id, HotelImages hotelImages)
        {
            if (id != hotelImages.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelImages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelImagesExists(id))
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

        // POST: api/HotelImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelImages>> PostHotelImages(HotelImages hotelImages)
        {
            _context.HotelImages.Add(hotelImages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelImages", new { id = hotelImages.Id }, hotelImages);
        }

        // DELETE: api/HotelImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImages(int id)
        {
            var hotelImages = await _context.HotelImages.FindAsync(id);
            if (hotelImages == null)
            {
                return NotFound();
            }

            _context.HotelImages.Remove(hotelImages);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelImagesExists(int id)
        {
            return _context.HotelImages.Any(e => e.Id == id);
        }
    }
}
