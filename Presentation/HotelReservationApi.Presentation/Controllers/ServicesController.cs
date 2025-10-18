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
using HotelReservationApi.Application.Features.CQRS.Service.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Service.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Service.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _context;

        public ServicesController(IMediator context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return Ok(await _context.Send(new GetAllServiceQueriesRequest()));
        }
        [Authorize(Roles ="HotelManager")]
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(CreateServiceCommandRequest service)
        {
            await _context.Send(service);
            return Ok();
        }
        [Authorize(Roles = "HotelManager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _context.Send(new DeleteServiceCommandRequest(id));

            return NoContent();
        }


    }
}
