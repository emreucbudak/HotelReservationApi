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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return Ok(await _context.Send(new GetAllReviewsQueriesRequest()));
        }
        [Authorize(Roles ="Member,Admin,HotelManager,Reception")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReviews([FromBody]int id,int? number, int? size)
        {
            return Ok(new GetReviewsByHotelIdQueriesRequest(id,number,size));
        }
        [Authorize(Roles ="Member")]
        [HttpPut]
        public async Task<IActionResult> PutReviews([FromBody]UpdateReviewsCommandRequest req)
        {
            await _context.Send(req);
            

            return NoContent();
        }
        [Authorize(Roles ="Member")]
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(CreateReviewsCommandRequest reviews)
        {
            await _context.Send(reviews);
            return NoContent();
        }
        [Authorize(Roles ="Member")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(int id)
        {
            await _context.Send(new DeleteReviewsCommandRequest(id));

            return NoContent();
        }


    }
}
