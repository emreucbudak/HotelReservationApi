using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;
using MediatR;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Queries.GetById;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Delete;

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
        // POST: api/HotelAdresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelAdress>> PostHotelAdress(CreateHotelAdressCommandRequest hotelAdress)
        {
            await _context.Send(hotelAdress);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelAdress(int id)
        {
            await _context.Send(new DeleteHotelAdressCommandRequest(id));

            return NoContent();
        }
    }
}
