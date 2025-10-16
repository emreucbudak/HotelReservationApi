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
    public class PriceListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PriceListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PriceLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceList>>> GetpriceList()
        {
            return await _context.priceList.ToListAsync();
        }

        // GET: api/PriceLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceList>> GetPriceList(int id)
        {
            var priceList = await _context.priceList.FindAsync(id);

            if (priceList == null)
            {
                return NotFound();
            }

            return priceList;
        }

        // PUT: api/PriceLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceList(int id, PriceList priceList)
        {
            if (id != priceList.Id)
            {
                return BadRequest();
            }

            _context.Entry(priceList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceListExists(id))
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

        // POST: api/PriceLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PriceList>> PostPriceList(PriceList priceList)
        {
            _context.priceList.Add(priceList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriceList", new { id = priceList.Id }, priceList);
        }

        // DELETE: api/PriceLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceList(int id)
        {
            var priceList = await _context.priceList.FindAsync(id);
            if (priceList == null)
            {
                return NotFound();
            }

            _context.priceList.Remove(priceList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceListExists(int id)
        {
            return _context.priceList.Any(e => e.Id == id);
        }
    }
}
