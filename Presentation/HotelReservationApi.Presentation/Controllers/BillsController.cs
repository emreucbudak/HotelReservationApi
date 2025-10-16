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
    public class BillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bills>>> Getbills()
        {
            return await _context.bills.ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bills>> GetBills(int id)
        {
            var bills = await _context.bills.FindAsync(id);

            if (bills == null)
            {
                return NotFound();
            }

            return bills;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBills(int id, Bills bills)
        {
            if (id != bills.Id)
            {
                return BadRequest();
            }

            _context.Entry(bills).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillsExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBills(Bills bills)
        {
            _context.bills.Add(bills);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBills", new { id = bills.Id }, bills);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBills(int id)
        {
            var bills = await _context.bills.FindAsync(id);
            if (bills == null)
            {
                return NotFound();
            }

            _context.bills.Remove(bills);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillsExists(int id)
        {
            return _context.bills.Any(e => e.Id == id);
        }
    }
}
