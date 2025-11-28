using HotelReservationApi.Application.Features.CQRS.Auth.Login;
using HotelReservationApi.Application.Features.CQRS.Auth.RefreshToken;
using HotelReservationApi.Application.Features.CQRS.Auth.Register;
using HotelReservationApi.Application.Features.CQRS.Auth.ReSendVerificationCode;
using HotelReservationApi.Application.Features.CQRS.Auth.Revoke;
using HotelReservationApi.Application.Features.CQRS.Auth.TwoFactors;
using HotelReservationApi.Presentation.ActionFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
        {
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }
        [Authorize(Policy = "OnlyPending2FA")]
        [ServiceFilter(typeof(TwoFactorAuthenticationFilter))]
        [HttpPost("two-factor-authenticate")]
        public async Task<IActionResult> TwoFactorAuthenticate(TwoFactorsAuthCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        [Authorize(Policy = "OnlyPending2FA")]
        [ServiceFilter(typeof(TwoFactorAuthenticationFilter))]
        [HttpPost("resend-two-factor-code")]
        public async Task<IActionResult> ResendTwoFactorCode(ResendTwoFactorVerificationCodeCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
    }
}

