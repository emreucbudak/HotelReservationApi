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
using HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Rooms.Command.Update;
using HotelReservationApi.Application.Features.CQRS.Rooms.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Rooms.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _context;

        public RoomsController( IMediator context)
        {
            _context = context;
        }

 

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Rooms>>> GetRooms(int id, [FromBody] int? pageCount, int? pageSize)
        {
            return Ok(await _context.Send(new GetAllRoomsQueriesRequest(id,pageCount,pageSize)));
        }

        
        [HttpPut]
        public async Task<IActionResult> PutRooms([FromQuery]UpdateRoomsCommandRequest req )
        {
          await _context.Send(req);

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rooms>> PostRooms(CreateRoomsCommandRequest rooms)
        {
           await _context.Send(rooms);
            return NoContent();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRooms(int id)
        {
            await _context.Send(new DeleteRoomsCommandRequest(id));

            return NoContent();
        }


    }
}
