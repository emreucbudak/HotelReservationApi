using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Create;
using HotelReservationApi.Application.Features.CQRS.Coupon.Command.Delete;
using HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetAll;
using HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetByName;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            return Ok(await _context.Send(new GetAllCouponQueriesRequest()));
        }
        [Authorize(Roles = "Member", Policy = "Verified2FA")]
        [HttpGet("{name}")]
        public async Task<ActionResult<Coupon>> GetCoupon(string name)
        {
            return Ok(await _context.Send(new GetCouponByNameQueriesRequest(name)));
            
        }
        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(CreateCouponCommandRequest coupon)
        {
            await _context.Send(coupon);

            return NoContent();
        }
        [Authorize(Roles = "Admin", Policy = "Verified2FA")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _context.Send(new DeleteCouponCommandRequest(id));

            return NoContent();
        }


    }
}
