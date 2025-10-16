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
    public class TypesFeaturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TypesFeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TypesFeatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypesFeatures>>> GetTypesFeatures()
        {
            return await _context.TypesFeatures.ToListAsync();
        }

        // GET: api/TypesFeatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypesFeatures>> GetTypesFeatures(int id)
        {
            var typesFeatures = await _context.TypesFeatures.FindAsync(id);

            if (typesFeatures == null)
            {
                return NotFound();
            }

            return typesFeatures;
        }

        // PUT: api/TypesFeatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypesFeatures(int id, TypesFeatures typesFeatures)
        {
            if (id != typesFeatures.Id)
            {
                return BadRequest();
            }

            _context.Entry(typesFeatures).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypesFeaturesExists(id))
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

        // POST: api/TypesFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypesFeatures>> PostTypesFeatures(TypesFeatures typesFeatures)
        {
            _context.TypesFeatures.Add(typesFeatures);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypesFeatures", new { id = typesFeatures.Id }, typesFeatures);
        }

        // DELETE: api/TypesFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypesFeatures(int id)
        {
            var typesFeatures = await _context.TypesFeatures.FindAsync(id);
            if (typesFeatures == null)
            {
                return NotFound();
            }

            _context.TypesFeatures.Remove(typesFeatures);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypesFeaturesExists(int id)
        {
            return _context.TypesFeatures.Any(e => e.Id == id);
        }
    }
}
