using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetByName;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Persistence.ApplicationContext;
using MediatR;
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
    public class CouponsController : ControllerBase
    {
        private readonly IMediator _context;

        public CouponsController(IMediator context)
        {
            _context = context;
        }

        // GET: api/Coupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            return Ok(await _context.Send(new GetAllCouponQueriesRequest()));
        }

        // GET: api/Coupons/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Coupon>> GetCoupon(string name)
        {
            return Ok(await _context.Send(new GetCouponByNameQueriesRequest(name)));
            
        }


        // POST: api/Coupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(CreateCouponCommandRequest coupon)
        {
            await _context.Send(coupon);

            return NoContent();
        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _context.Send(new DeleteCouponCommandRequest(id));

            return NoContent();
        }


    }
}
