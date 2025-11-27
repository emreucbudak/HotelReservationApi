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
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels([FromBody]int? pageSize, int? pageNumber)
        {
            return Ok(await _context.Send(new GetAllHotelsQueriesRequest(pageNumber,pageSize)));
        }
        [Authorize(Roles ="Member,Admin,Reception,HotelManager",Policy = "Verified2FA")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotels>> GetHotels(int id)
        {
            return Ok(await _context.Send(new GetHotelByIdQueriesRequest(id)));
        }
        [Authorize(Roles ="Admin",Policy = "Verified2FA")]
        [HttpPut]
        public async Task<IActionResult> PutHotels(UpdateHotelsCommandRequest req)
        {
            await _context.Send(req);


            return NoContent();
        }
        [Authorize(Roles ="Admin",Policy = "Verified2FA")]
        [HttpPost]
        public async Task<ActionResult<Hotels>> PostHotels(CreateHotelsCommandRequest hotels)
        {
            await _context.Send(hotels);
            return NoContent();
        }
        [Authorize(Roles = "Admin",Policy = "Verified2FA")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotels(int id)
        {
            await _context.Send(new DeleteHotelsCommandRequest(id));


            return NoContent();
        }


    }
}
