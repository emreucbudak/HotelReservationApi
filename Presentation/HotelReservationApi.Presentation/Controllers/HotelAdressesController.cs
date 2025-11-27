using Microsoft.AspNetCore.Mvc;
using HotelReservationApi.Domain.Entities;
using MediatR;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Queries.GetById;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAdressesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelAdressesController(IMediator context)
        {
            _context = context;
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelAdress>> GetHotelAdress(int id)
        {
            return Ok(await _context.Send(new GetHotelAdressByIdQueriesRequest(id)));

        }
        [Authorize(Roles ="HotelManager",Policy = "Verified2FA")]
        [HttpPost]
        public async Task<ActionResult<HotelAdress>> PostHotelAdress(CreateHotelAdressCommandRequest hotelAdress)
        {
            await _context.Send(hotelAdress);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager",Policy = "Verified2FA")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelAdress(int id)
        {
            await _context.Send(new DeleteHotelAdressCommandRequest(id));

            return NoContent();
        }
    }
}
