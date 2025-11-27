using AutoMapper;
using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.Features.CQRS.Auth.Rules;
using HotelReservationApi.Application.Tokens;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly AuthRules authRules;

        public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService, AuthRules authRules, IMapper mapper)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.authRules = authRules;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
           User user = await userManager.FindByEmailAsync(request.Email);
            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
            await authRules.EmailOrPasswordShouldNotBeInvalid(user,checkPassword);
            IList<string> roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken token = tokenService.CreateTempToken(user, roles);
            string tempToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new()
            {
               TempToken = tempToken
            };


        }
    }
}
