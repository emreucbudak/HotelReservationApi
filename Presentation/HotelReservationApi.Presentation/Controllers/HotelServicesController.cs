using HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.HotelsService.Queries.GetAll;
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
    [Authorize(Roles ="HotelManager",Policy ="Verified2FA")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelServicesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelServicesController(IMediator context)
        {
            _context = context;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<HotelServices>>> GetHotelServices(int HotelsId)
        {
            return Ok(await _context.Send(new GetAllHotelsServiceQueriesRequest(HotelsId)));
        }
        // POST: api/HotelServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelServices>> PostHotelServices(CreateHotelsServiceCommandRequest hotelServices)
        {
            await _context.Send(hotelServices);
            return NoContent();
        }

        // DELETE: api/HotelServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelServices(int id)
        {
            await _context.Send(new DeleteHotelsServiceCommandRequest(id));

            return NoContent();
        }


    }
}
