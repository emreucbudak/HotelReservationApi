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
using HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Create;
using HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesFeaturesController : ControllerBase
    {
        private readonly IMediator _context;

        public TypesFeaturesController(IMediator context)
        {
            _context = context;
        }




        // POST: api/TypesFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypesFeatures>> PostTypesFeatures(CreateTypesFeaturesCommandRequest typesFeatures)
        {
            await _context.Send(typesFeatures);
            return Ok();
        }

        // DELETE: api/TypesFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypesFeatures(int id)
        {
            await _context.Send(new DeleteTypesFeaturesCommandRequest(id));

            return NoContent();
        }


    }
}
