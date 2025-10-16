using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HotelReservationApi.Application.Tokens;
using HotelReservationApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservationApi.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenSettings _tokenService;

        public TokenService(UserManager<User> userManager, IOptions<TokenSettings> tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService.Value;
        }

        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(JwtRegisteredClaimNames.Email ,user.Email),

            };
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenService.SecretKey));
            var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _tokenService.Issuer,
                audience: _tokenService.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenService.AccessTokenExpirationMinutes),
                signingCredentials: signIn
                
                                );
            return token;


        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,

                ValidateAudience = false,

                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenService.SecretKey)),
                ValidateIssuerSigningKey = true
            };
            var security = new JwtSecurityTokenHandler();
            var validateToken = security.ValidateToken(token,validationParameters, out SecurityToken sct);
            if (sct is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                throw new SecurityTokenInvalidAlgorithmException("Bilinmeyen token");
            }
            return validateToken;

        }
    }
}
