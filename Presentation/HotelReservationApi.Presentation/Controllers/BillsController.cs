﻿using HotelReservationApi.Application.Features.CQRS.Bills.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Bills.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId;
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
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IMediator _context;

        public BillsController(IMediator context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bills>>> Getbills()
        {
            return Ok(await _context.Send(new GetAllBillsQueriesRequest()));
        }
        [Authorize(Roles = "HotelManager,Reception")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Bills>>> GetBills(
    int id,
    [FromQuery] int? pageCount,
    [FromQuery] int ?pageSize)
        {
            GetAllBillsByHotelIdQueriesRequest request = new(id,pageCount,pageSize);
            return Ok(await _context.Send(request));

        }
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBills(CreateBillsCommandRequest bills)
        {
            await _context.Send(bills);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager,Reception")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBills(int id)
        {
           DeleteBillsCommandRequest req = new(id);
            await _context.Send(req);

            return NoContent();
        }
    }
}
