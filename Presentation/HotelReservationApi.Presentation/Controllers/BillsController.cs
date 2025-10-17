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
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.Bills.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Bills.Command.Delete;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IMediator _context;

        public BillsController(IMediator context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bills>>> Getbills()
        {
            return Ok(await _context.Send(new GetAllBillsQueriesRequest()));
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Bills>>> GetBills(
    int id,
    [FromQuery] int? pageCount,
    [FromQuery] int ?pageSize)
        {
            GetAllBillsByHotelIdQueriesRequest request = new(id,pageCount,pageSize);
            return Ok(await _context.Send(request));

        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBills(CreateBillsCommandRequest bills)
        {
            await _context.Send(bills);
            return NoContent();
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBills(int id)
        {
           DeleteBillsCommandRequest req = new(id);
            await _context.Send(req);

            return NoContent();
        }
    }
}
