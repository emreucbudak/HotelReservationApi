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
    public class HotelCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HotelCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelCategory>>> GetHotelCategories()
        {
            return await _context.HotelCategories.ToListAsync();
        }

        // GET: api/HotelCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelCategory>> GetHotelCategory(int id)
        {
            var hotelCategory = await _context.HotelCategories.FindAsync(id);

            if (hotelCategory == null)
            {
                return NotFound();
            }

            return hotelCategory;
        }

        // PUT: api/HotelCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelCategory(int id, HotelCategory hotelCategory)
        {
            if (id != hotelCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelCategoryExists(id))
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

        // POST: api/HotelCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelCategory>> PostHotelCategory(HotelCategory hotelCategory)
        {
            _context.HotelCategories.Add(hotelCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelCategory", new { id = hotelCategory.Id }, hotelCategory);
        }

        // DELETE: api/HotelCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelCategory(int id)
        {
            var hotelCategory = await _context.HotelCategories.FindAsync(id);
            if (hotelCategory == null)
            {
                return NotFound();
            }

            _context.HotelCategories.Remove(hotelCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelCategoryExists(int id)
        {
            return _context.HotelCategories.Any(e => e.Id == id);
        }
    }
}
