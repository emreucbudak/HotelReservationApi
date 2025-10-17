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
using HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Create;
using HotelReservationApi.Application.Features.CQRS.HotelImages.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelImagesController : ControllerBase
    {
        private readonly IMediator _context;

        public HotelImagesController(IMediator context)
        {
            _context = context;
        }



        // GET: api/HotelImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HotelImages>>> GetHotelImages(int id)
        {
            await _context.Send(new GetAllHotelImagesByIdQueriesRequest(id));
            return NoContent();
        }


        // POST: api/HotelImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelImages>> PostHotelImages(CreateHotelImagesCommandRequest hotelImages)
        {
            await _context.Send(hotelImages);
            return NoContent();
        }

        // DELETE: api/HotelImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelImages(int id)
        {
            await _context.Send(new DeleteHotelImagesCommandRequest(id));
            return NoContent();
        }

    }
}
