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
    public class AdsBannersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdsBannersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdsBanners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdsBanner>>> GetAdsBanner()
        {
            return await _context.AdsBanner.ToListAsync();
        }

        // GET: api/AdsBanners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdsBanner>> GetAdsBanner(int id)
        {
            var adsBanner = await _context.AdsBanner.FindAsync(id);

            if (adsBanner == null)
            {
                return NotFound();
            }

            return adsBanner;
        }

        // PUT: api/AdsBanners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdsBanner(int id, AdsBanner adsBanner)
        {
            if (id != adsBanner.Id)
            {
                return BadRequest();
            }

            _context.Entry(adsBanner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdsBannerExists(id))
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

        // POST: api/AdsBanners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdsBanner>> PostAdsBanner(AdsBanner adsBanner)
        {
            _context.AdsBanner.Add(adsBanner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdsBanner", new { id = adsBanner.Id }, adsBanner);
        }

        // DELETE: api/AdsBanners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdsBanner(int id)
        {
            var adsBanner = await _context.AdsBanner.FindAsync(id);
            if (adsBanner == null)
            {
                return NotFound();
            }

            _context.AdsBanner.Remove(adsBanner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdsBannerExists(int id)
        {
            return _context.AdsBanner.Any(e => e.Id == id);
        }
    }
}
