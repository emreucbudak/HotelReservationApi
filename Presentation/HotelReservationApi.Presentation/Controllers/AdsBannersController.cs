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
using HotelReservationApi.Application.Features.CQRS.AdsBanner.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Update;
using HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Create;
using HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsBannersController : ControllerBase
    {
        private readonly IMediator _context;

        public AdsBannersController(IMediator context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdsBanner>>> GetAdsBanner()
        {
            var result = await _context.Send(new GetAllAdsBannerQueriesRequest());
            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdsBanner(UpdateAdsBannerCommandRequest req)
        {
             await _context.Send(req);
            

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AdsBanner>> PostAdsBanner(CreateAdsBannerCommandRequest adsBanner)
        {
            await _context.Send(adsBanner);

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdsBanner(int id)
        {
            DeleteAdsBannerCommandRequest req = new DeleteAdsBannerCommandRequest();
            req.Id = id;
            await _context.Send(req);
            

            return NoContent();
        }

    }
}
