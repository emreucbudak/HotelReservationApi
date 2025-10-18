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
using HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsPoliticiesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelsPoliticiesController(IMediator context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsPoliticy>> GetHotelsPoliticy(int HotelId)
        {
            return Ok(await _context.Send(new GetAllHotelsPoliticyQueriesRequest(HotelId)));
        }
        [Authorize(Roles ="HotelManager,Reception")]
        [HttpPost]
        public async Task<ActionResult<HotelsPoliticy>> PostHotelsPoliticy(CreateHotelsPoliticyCommandRequest hotelsPoliticy)
        {
            await _context.Send(hotelsPoliticy);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager,Reception")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelsPoliticy(int id)
        {
            await _context.Send(new DeleteHotelsPoliticyCommandRequest(id));

            return NoContent();
        }


    }
}
