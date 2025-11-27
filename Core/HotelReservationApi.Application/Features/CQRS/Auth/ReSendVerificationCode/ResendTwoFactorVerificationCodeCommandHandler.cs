using HotelReservationApi.Application.RabbitMq.Interfaces;
using HotelReservationApi.Application.Tokens;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.ReSendVerificationCode
{
    public class ResendTwoFactorVerificationCodeCommandHandler : IRequestHandler<ResendTwoFactorVerificationCodeCommandRequest>
    {
        private readonly ITokenService token;
        private readonly IDistributedCache cache;
        private readonly IMessageQueueService queue;

        public ResendTwoFactorVerificationCodeCommandHandler(ITokenService token, IDistributedCache cache, IMessageQueueService queue)
        {
            this.token = token;
            this.cache = cache;
            this.queue = queue;
        }

        public async Task Handle(ResendTwoFactorVerificationCodeCommandRequest request, CancellationToken cancellationToken)
        {
            var claim = token.GetPrincipalFromTempToken(request.TempToken);
            string email =  claim.FindFirstValue(ClaimTypes.Email);
            bool isOnCoolDown = await cache.GetStringAsync($"2fa-cooldown-{email}") is not null;
            if (isOnCoolDown)
            {
                throw new InvalidOperationException("Lütfen tekrar kod istemeden önce bekleyin.");
            }
            int verificationCode = RandomNumberGenerator.GetInt32(100000, 1000000);
            await cache.SetStringAsync($"2fa-{email}", verificationCode.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            await cache.SetStringAsync($"2fa-cooldown-{email}", "1", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
            await queue.PublishAsync("TwoFactorQueue", new RabbitMq.Models.TwoFactorMessage()
            {
                Email = email,
                VerificationCode = verificationCode.ToString()
            });

        }
    }
}
