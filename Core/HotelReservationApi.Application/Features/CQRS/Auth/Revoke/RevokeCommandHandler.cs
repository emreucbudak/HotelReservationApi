using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Auth.Rules;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Revoke
{
    public class RevokeCommandHandler : IRequestHandler<RevokeCommandRequest>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;

        public RevokeCommandHandler(UserManager<User> userManager, AuthRules authRules, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.authRules = authRules;
        }

        public async Task Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);
            await authRules.EmailAddressShouldBeValid(user);

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);
        }
    }
}
