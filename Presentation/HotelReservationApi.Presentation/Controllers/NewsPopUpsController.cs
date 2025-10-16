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
    public class NewsPopUpsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsPopUpsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsPopUps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsPopUp>>> GetnewsPopUps()
        {
            return await _context.newsPopUps.ToListAsync();
        }

        // GET: api/NewsPopUps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsPopUp>> GetNewsPopUp(int id)
        {
            var newsPopUp = await _context.newsPopUps.FindAsync(id);

            if (newsPopUp == null)
            {
                return NotFound();
            }

            return newsPopUp;
        }

        // PUT: api/NewsPopUps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsPopUp(int id, NewsPopUp newsPopUp)
        {
            if (id != newsPopUp.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsPopUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsPopUpExists(id))
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

        // POST: api/NewsPopUps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsPopUp>> PostNewsPopUp(NewsPopUp newsPopUp)
        {
            _context.newsPopUps.Add(newsPopUp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsPopUp", new { id = newsPopUp.Id }, newsPopUp);
        }

        // DELETE: api/NewsPopUps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsPopUp(int id)
        {
            var newsPopUp = await _context.newsPopUps.FindAsync(id);
            if (newsPopUp == null)
            {
                return NotFound();
            }

            _context.newsPopUps.Remove(newsPopUp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsPopUpExists(int id)
        {
            return _context.newsPopUps.Any(e => e.Id == id);
        }
    }
}
