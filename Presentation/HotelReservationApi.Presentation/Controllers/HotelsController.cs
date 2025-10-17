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
using HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetById;
using HotelReservationApi.Application.Features.CQRS.Hotels.Command.Update;
using HotelReservationApi.Application.Features.CQRS.Hotels.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Hotels.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelsController(IMediator context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels([FromBody]int? pageSize, int? pageNumber)
        {
            return Ok(await _context.Send(new GetAllHotelsQueriesRequest(pageNumber,pageSize)));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotels>> GetHotels(int id)
        {
            return Ok(await _context.Send(new GetHotelByIdQueriesRequest(id)));
        }


        [HttpPut]
        public async Task<IActionResult> PutHotels(UpdateHotelsCommandRequest req)
        {
            await _context.Send(req);


            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotels>> PostHotels(CreateHotelsCommandRequest hotels)
        {
            await _context.Send(hotels);
            return NoContent();
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotels(int id)
        {
            await _context.Send(new DeleteHotelsCommandRequest(id));


            return NoContent();
        }


    }
}
