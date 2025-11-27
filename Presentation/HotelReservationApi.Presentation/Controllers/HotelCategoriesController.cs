using HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.HotelCategory.Queries.GetAll;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<HotelCategory>> PostHotelCategory(CreateHotelCategoryCommandRequest hotelCategory)
        {
            await _context.Send(hotelCategory);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelCategory(int id)
        {
            await _context.Send(new DeleteHotelCategoryCommandRequest(id));

            return NoContent();
        }

    
    }
}
