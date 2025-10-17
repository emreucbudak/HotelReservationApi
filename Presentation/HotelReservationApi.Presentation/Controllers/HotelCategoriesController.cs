using HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.HotelCategory.Queries.GetAll;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelCategoriesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelCategoriesController(IMediator context)
        {
            _context = context;
        }

        // GET: api/HotelCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelCategory>>> GetHotelCategories()
        {
            return Ok(await _context.Send(new GetAllHotelCategoryQueriesRequest()));
        }
        // POST: api/HotelCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelCategory>> PostHotelCategory(CreateHotelCategoryCommandRequest hotelCategory)
        {
            await _context.Send(hotelCategory);
            return NoContent();
        }

        // DELETE: api/HotelCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelCategory(int id)
        {
            await _context.Send(new DeleteHotelCategoryCommandRequest(id));

            return NoContent();
        }

    
    }
}
