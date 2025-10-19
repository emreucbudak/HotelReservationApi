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
using HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Create;
using HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Delete;
using Microsoft.AspNetCore.Authorization;
using HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByHotelsId;
using HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByRoomTypeId;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountListsController : ControllerBase
    {
        private readonly IMediator _context;

        public DiscountListsController(IMediator context)
        {
            _context = context;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountList>>> GetdiscountLists()
        {
            return Ok(await _context.Send(new GetAllDiscountListQueriesRequest()));
        }
        [Authorize(Roles = "HotelManager")]
        [HttpGet("/hotel/{id}")]
        public async Task<ActionResult<IEnumerable<DiscountList>>> Getdiscountlistid(int hotelId)
        {
            return Ok(await _context.Send(new GetDiscountListByHotelsIdQueriesRequest(hotelId)));
        }
        [Authorize(Roles = "HotelManager")]
        [HttpGet("/roomtypes/{id}")]
        public async Task<ActionResult<IEnumerable<DiscountList>>> Getdiscountlistroomid(int roomTypesId)
        {
            return Ok(await _context.Send(new GetDiscountByRoomTypeIdQueriesRequest(roomTypesId)));
        }
        [Authorize(Roles ="HotelManager,Admin")]
        [HttpPost]
        public async Task<ActionResult<DiscountList>> PostDiscountList(CreateDiscountListCommandRequest discountList)
        {
            await _context.Send(discountList);
            return NoContent();
        }
        [Authorize(Roles = "HotelManager,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountList(int id)
        {
            await _context.Send(new DeleteDiscountListCommandRequest(id));

            return NoContent();
        }
    }
}
