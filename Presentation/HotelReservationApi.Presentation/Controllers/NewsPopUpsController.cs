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
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Update;
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Create;
using HotelReservationApi.Application.Features.CQRS.NewsPopUp.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPopUpsController : ControllerBase
    {
        private readonly IMediator _context;

        public NewsPopUpsController(IMediator context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsPopUp>>> GetnewsPopUps()
        {
            return Ok(await _context.Send(new GetAllNewsPopUpQueriesRequest()));
        }
        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpPut]
        public async Task<IActionResult> PutNewsPopUp([FromBody] UpdateNewsPopUpCommandRequest req)
        {
            await _context.Send(req);
            

            return NoContent();
        }
        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpPost]
        public async Task<ActionResult<NewsPopUp>> PostNewsPopUp(CreateNewsPopUpCommandRequest newsPopUp)
        {
            await _context.Send(newsPopUp);
            return NoContent();
        }
        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsPopUp(int id)
        {
            await _context.Send(new DeleteNewsPopUpCommandRequest(id));

            return NoContent();
        }


    }
}
