using HotelReservationApi.Application.Features.CQRS.Payments.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Authorize(Roles ="Member",Policy ="Verified2FA")]
        [HttpPost("reservation-payment")]
        public async Task<IActionResult> ProcessReservationPayment(CreatePaymentCommandRequest req)
        {
            await mediator.Send(req);
            return Ok();
        }
    }
}
