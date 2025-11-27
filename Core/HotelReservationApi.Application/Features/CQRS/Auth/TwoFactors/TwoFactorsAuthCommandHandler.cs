using HotelReservationApi.Application.Tokens;
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

namespace HotelReservationApi.Application.Features.CQRS.Auth.TwoFactors
{
    public class TwoFactorsAuthCommandHandler : IRequestHandler<TwoFactorsAuthCommandRequest, TwoFactorsAuthCommandResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;
        private readonly IDistributedCache cache;

        public TwoFactorsAuthCommandHandler(UserManager<User> userManager, ITokenService tokenService, IConfiguration configuration, IDistributedCache cache)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.configuration = configuration;
            this.cache = cache;
        }

        public async Task<TwoFactorsAuthCommandResponse> Handle(TwoFactorsAuthCommandRequest request, CancellationToken cancellationToken)
        {
            var claimsPrincipal =  tokenService.GetPrincipalFromTempToken(request.TempToken);
            var email = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            string verificationCode = await cache.GetStringAsync($"2fa-{email}");
            if (verificationCode is null || verificationCode != request.Code)
            {
                throw new InvalidOperationException("Geçersiz veya yanlış onay kodu");
            }
            var user = await userManager.FindByEmailAsync(email);
            IList<string> roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken token = tokenService.CreateToken(user, roles);
            _ = int.TryParse(configuration["JwtSettings:RefreshTokenExpirationDays"], out int refreshTokenValidityInDays);
            string refreshToken = tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);
            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);
            string _token = new JwtSecurityTokenHandler().WriteToken(token);
            await userManager.SetAuthenticationTokenAsync(user, "default", "AccessToken", _token);
            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

        }
    }
}
