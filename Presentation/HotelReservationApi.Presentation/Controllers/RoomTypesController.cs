using HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Create;
using HotelReservationApi.Application.Features.CQRS.RoomTypes.Command.Delete;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationApi.Presentation.Controllers
{
    [Authorize(Roles ="HotelManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IMediator _context;

        public RoomTypesController(IMediator context)
        {
            _context = context;
        }


        // POST: api/RoomTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomTypes>> PostRoomTypes(CreateRoomTypesCommandRequest roomTypes)
        {
            await _context.Send(roomTypes);
            return NoContent();
        }

        // DELETE: api/RoomTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomTypes(int id)
        {
            await _context.Send(new DeleteRoomTypesCommandRequest(id));

            return NoContent();
        }

 
    }
}
