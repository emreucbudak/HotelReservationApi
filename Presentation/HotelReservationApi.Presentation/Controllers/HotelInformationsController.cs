using HotelReservationApi.Application.Features.CQRS.HotelInformation.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelInformation.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.HotelInformation.Queries.GetByHotelId;
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
    [Route("api/[controller]")]
    [ApiController]
    public class HotelInformationsController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelInformationsController(IMediator context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelInformation>> GetHotelInformation(int id)
        {
            return Ok(await _context.Send(new GetHotelInformationByIdQueriesRequest(id)));
        }
        [Authorize(Roles ="HotelManager",Policy = "Verified")]
        [HttpPost]
        public async Task<ActionResult<HotelInformation>> PostHotelInformation(CreateHotelInformationCommandRequest hotelInformation)
        {
            await _context.Send(hotelInformation);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager",Policy = "Verified")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelInformation(int id)
        {
            await _context.Send(new DeleteHotelInformationCommandRequest(id));

            return NoContent();
        }


    }
}
