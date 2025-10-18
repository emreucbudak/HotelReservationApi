﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;
using MediatR;
using HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId;
using HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Reservation.Command.Delete;
using Microsoft.AspNetCore.Authorization;
using HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByMemberId;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _context;

        public ReservationsController(IMediator context)
        {
            _context = context;
        }
        [Authorize(Roles ="Member")]
        [HttpGet("reservation/{memberId}")]
        public async Task<ActionResult<List<Reservation>>> GetReservationByMemberId(int memberId)
        {
            return Ok(await _context.Send(new GetAllReservationByMemberIdQueriesRequest(memberId)));
        }
        [Authorize(Roles ="HotelManager,Reception")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Reservation>>> GetReservation(int id)
        {
            return Ok(await _context.Send(new GetAllReservationByHotelIdQueriesRequest(id)));
        }
        [Authorize(Roles ="Member")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(CreateReservationCommandRequest reservation)
        {
            await _context.Send(reservation);

            return NoContent();
        }
        [Authorize(Roles ="Member,HotelManager,Reception")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _context.Send(new DeleteReservationCommandRequest(id));

            return NoContent();
        }

    
    }
}
