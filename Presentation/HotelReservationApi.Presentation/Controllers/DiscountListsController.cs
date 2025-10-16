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
    public class DiscountListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiscountListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DiscountLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountList>>> GetdiscountLists()
        {
            return await _context.discountLists.ToListAsync();
        }

        // GET: api/DiscountLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountList>> GetDiscountList(int id)
        {
            var discountList = await _context.discountLists.FindAsync(id);

            if (discountList == null)
            {
                return NotFound();
            }

            return discountList;
        }

        // PUT: api/DiscountLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscountList(int id, DiscountList discountList)
        {
            if (id != discountList.Id)
            {
                return BadRequest();
            }

            _context.Entry(discountList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountListExists(id))
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

        // POST: api/DiscountLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiscountList>> PostDiscountList(DiscountList discountList)
        {
            _context.discountLists.Add(discountList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscountList", new { id = discountList.Id }, discountList);
        }

        // DELETE: api/DiscountLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountList(int id)
        {
            var discountList = await _context.discountLists.FindAsync(id);
            if (discountList == null)
            {
                return NotFound();
            }

            _context.discountLists.Remove(discountList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscountListExists(int id)
        {
            return _context.discountLists.Any(e => e.Id == id);
        }
    }
}
