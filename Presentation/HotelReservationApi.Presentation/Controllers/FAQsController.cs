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
using HotelReservationApi.Application.Features.CQRS.FAQ.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.FAQ.Command.Update;
using HotelReservationApi.Application.Features.CQRS.FAQ.Command.Create;
using HotelReservationApi.Application.Features.CQRS.FAQ.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQsController : ControllerBase
    {
        private readonly IMediator _context;

        public FAQsController(IMediator context)
        {
            _context = context;
        }

        // GET: api/FAQs
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FAQ>>> GetfAQs(int id)
        {
            return Ok(await _context.Send(new GetAllFAQQueriesRequest(id)));
        }
        [HttpPut]
        public async Task<IActionResult> PutFAQ([FromBody]UpdateFAQCommandRequest req)
        {
            await _context.Send(req);

            return NoContent();
        }

        // POST: api/FAQs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FAQ>> PostFAQ(CreateFAQCommandRequest fAQ)
        {
            await _context.Send(fAQ);
            return NoContent();
        }

        // DELETE: api/FAQs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            await _context.Send(new DeleteFAQCommandRequest(id));

            return NoContent();
        }


    }
}
