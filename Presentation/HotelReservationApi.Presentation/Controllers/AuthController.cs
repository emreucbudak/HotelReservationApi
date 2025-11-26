using HotelReservationApi.Application.Features.CQRS.Auth.Login;
using HotelReservationApi.Application.Features.CQRS.Auth.RefreshToken;
using HotelReservationApi.Application.Features.CQRS.Auth.Register;
using HotelReservationApi.Application.Features.CQRS.Auth.Revoke;
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
        public async Task<IActionResult> TwoFactorAuthenticate(twofactor request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }


    }
}
