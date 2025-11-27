using HotelReservationApi.Application.Features.CQRS.Auth.Login;
using HotelReservationApi.Application.Features.CQRS.Auth.RefreshToken;
using HotelReservationApi.Application.Features.CQRS.Auth.Register;
using HotelReservationApi.Application.Features.CQRS.Auth.ReSendVerificationCode;
using HotelReservationApi.Application.Features.CQRS.Auth.Revoke;
using HotelReservationApi.Application.Features.CQRS.Auth.TwoFactors;
using MediatR;
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
        [HttpPost("two-factor-authenticate")]
        public async Task<IActionResult> TwoFactorAuthenticate(TwoFactorsAuthCommandRequest request)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) &&
                authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {

                request.TempToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            var response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("resend-two-factor-code")]
        public async Task<IActionResult> ResendTwoFactorCode(ResendTwoFactorVerificationCodeCommandRequest request)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) &&
                authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                request.TempToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            }
            await mediator.Send(request);
            return Ok();


        }
    }
}

