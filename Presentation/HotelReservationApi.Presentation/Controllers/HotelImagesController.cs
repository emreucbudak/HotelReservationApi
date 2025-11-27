using Microsoft.AspNetCore.Mvc;
using HotelReservationApi.Domain.Entities;
using MediatR;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelImagesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelImagesController(IMediator context)
        {
            _context = context;
        }



        // GET: api/HotelImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HotelImages>>> GetHotelImages(int id)
        {
            await _context.Send(new GetAllHotelImagesByIdQueriesRequest(id));
            return NoContent();
        }
        [Authorize(Roles ="HotelManager")]
        [HttpPost]
        public async Task<ActionResult<HotelImages>> PostHotelImages(CreateHotelImagesCommandRequest hotelImages)
        {
            await _context.Send(hotelImages);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImages(int id)
        {
            await _context.Send(new DeleteHotelImagesCommandRequest(id));
            return NoContent();
        }

    }
}
