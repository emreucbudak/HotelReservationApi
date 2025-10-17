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
using HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetByHotelId;
using HotelReservationApi.Application.Features.CQRS.Reviews.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Reviews.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.Reviews.Command.Update;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _context;

        public ReviewsController(IMediator context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return Ok(await _context.Send(new GetAllReviewsQueriesRequest()));
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReviews(int id)
        {
            return Ok(new GetReviewsByHotelIdQueriesRequest(id));
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutReviews([FromBody]UpdateReviewsCommandRequest req)
        {
            await _context.Send(req);
            

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(CreateReviewsCommandRequest reviews)
        {
            await _context.Send(reviews);
            return NoContent();
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(int id)
        {
            await _context.Send(new DeleteReviewsCommandRequest(id));

            return NoContent();
        }


    }
}
