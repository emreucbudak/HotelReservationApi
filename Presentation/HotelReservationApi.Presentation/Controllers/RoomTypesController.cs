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
    public class RoomTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RoomTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypes>>> GetRoomTypes()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        // GET: api/RoomTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypes>> GetRoomTypes(int id)
        {
            var roomTypes = await _context.RoomTypes.FindAsync(id);

            if (roomTypes == null)
            {
                return NotFound();
            }

            return roomTypes;
        }

        // PUT: api/RoomTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomTypes(int id, RoomTypes roomTypes)
        {
            if (id != roomTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTypesExists(id))
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

        // POST: api/RoomTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomTypes>> PostRoomTypes(RoomTypes roomTypes)
        {
            _context.RoomTypes.Add(roomTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomTypes", new { id = roomTypes.Id }, roomTypes);
        }

        // DELETE: api/RoomTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomTypes(int id)
        {
            var roomTypes = await _context.RoomTypes.FindAsync(id);
            if (roomTypes == null)
            {
                return NotFound();
            }

            _context.RoomTypes.Remove(roomTypes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomTypesExists(int id)
        {
            return _context.RoomTypes.Any(e => e.Id == id);
        }
    }
}
