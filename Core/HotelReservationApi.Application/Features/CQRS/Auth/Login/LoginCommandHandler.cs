using AutoMapper;
using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.Features.CQRS.Auth.Rules;
using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Model;
using HotelReservationApi.Application.RabbitMq.Interfaces;
using HotelReservationApi.Application.RabbitMq.Models;
using HotelReservationApi.Application.Tokens;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache distributedCache;
        private readonly IMessageQueueService queue;

        public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService, AuthRules authRules, IMapper mapper, IDistributedCache distributedCache, IMessageQueueService queue)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.authRules = authRules;
            this.distributedCache = distributedCache;
            this.queue = queue;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
           User user = await userManager.FindByEmailAsync(request.Email);
            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
            await authRules.EmailOrPasswordShouldNotBeInvalid(user,checkPassword);
            IList<string> roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken token = tokenService.CreateTempToken(user, roles);
            int verificationCode = new Random().Next(100000, 999999);
            await distributedCache.SetStringAsync($"2fa-{user.Email}", verificationCode.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            await queue.PublishAsync("TwoFactorQueue", new TwoFactorMessage()
            {
                Email = user.Email,
                VerificationCode = verificationCode.ToString()
            });
            string tempToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new()
            {
               TempToken = tempToken
            };


        }
    }
}
