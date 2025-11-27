using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Tokens
{
    public interface ITokenService
    {
        JwtSecurityToken CreateToken(User user, IList<string> claim);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        JwtSecurityToken CreateTempToken (User user, IList<string> claim);
        ClaimsPrincipal? GetPrincipalFromTempToken(string? token);
    }
}
