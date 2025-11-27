using HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Queries.GetAllByHotelId;
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
    public class HowFarSpecialPlacesController : ControllerBase
    {
        private readonly IMediator _context;

        public HowFarSpecialPlacesController(IMediator context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HowFarSpecialPlace>> GetHowFarSpecialPlace(int id)
        {
            return Ok(new GetAllHowFarSpecialPlaceQueriesRequest(id));
        }
        [Authorize(Roles = "Reception,HotelMember", Policy = "Verified2FA")]
        [HttpPost]
        public async Task<ActionResult<HowFarSpecialPlace>> PostHowFarSpecialPlace(CreateHowFarSpecialPlaceCommandRequest howFarSpecialPlace)
        {
            await _context.Send(howFarSpecialPlace);
            return Ok();
        }
        [Authorize(Roles = "Reception,HotelMember", Policy = "Verified2FA")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHowFarSpecialPlace(int id)
        {
            await _context.Send(new DeleteHowFarSpecialPlaceCommandRequest(id));

            return NoContent();
        }


    }
}
