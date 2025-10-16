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
    public class HotelServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelServices>>> GetHotelServices()
        {
            return await _context.HotelServices.ToListAsync();
        }

        // GET: api/HotelServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelServices>> GetHotelServices(int id)
        {
            var hotelServices = await _context.HotelServices.FindAsync(id);

            if (hotelServices == null)
            {
                return NotFound();
            }

            return hotelServices;
        }

        // PUT: api/HotelServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelServices(int id, HotelServices hotelServices)
        {
            if (id != hotelServices.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelServices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelServicesExists(id))
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

        // POST: api/HotelServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelServices>> PostHotelServices(HotelServices hotelServices)
        {
            _context.HotelServices.Add(hotelServices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelServices", new { id = hotelServices.Id }, hotelServices);
        }

        // DELETE: api/HotelServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelServices(int id)
        {
            var hotelServices = await _context.HotelServices.FindAsync(id);
            if (hotelServices == null)
            {
                return NotFound();
            }

            _context.HotelServices.Remove(hotelServices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelServicesExists(int id)
        {
            return _context.HotelServices.Any(e => e.Id == id);
        }
    }
}
