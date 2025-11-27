using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HotelReservationApi.Application.Tokens;
using HotelReservationApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservationApi.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenService;
        private readonly TwoFactorAuthSettings _twoFactorAuthSettings;

        public TokenService(UserManager<User> userManager, IOptions<TokenSettings> tokenService, IOptions<TwoFactorAuthSettings> twoFactorAuthSettings)
        {
            _tokenService = tokenService.Value;
            _twoFactorAuthSettings = twoFactorAuthSettings.Value;
        }

        public JwtSecurityToken CreateTempToken(User user, IList<string> claim)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email ,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("2fa_status", "pending")

            };
            foreach (var role in claim)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenService.SecretKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _twoFactorAuthSettings.Issuer,
                audience: _twoFactorAuthSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_twoFactorAuthSettings.TempTokenExpirationMinutes),
                signingCredentials: signIn
                                );
            return token;
        }

        public JwtSecurityToken CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email ,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("2fa_status", "verified")

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

        public ClaimsPrincipal? GetPrincipalFromTempToken(string? token)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenService.SecretKey)),
                ValidateIssuerSigningKey = true
            };
            var security = new JwtSecurityTokenHandler();
            var validateToken = security.ValidateToken(token, validationParameters, out SecurityToken sct);
            if (sct is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenInvalidAlgorithmException("Bilinmeyen token");
            }
            return validateToken;
        }
    }
}
