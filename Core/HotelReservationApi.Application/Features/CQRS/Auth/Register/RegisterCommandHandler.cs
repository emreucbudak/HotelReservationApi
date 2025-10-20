using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Auth.Register.EntityFactory;
using HotelReservationApi.Application.Features.CQRS.Auth.Rules;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Common;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;
        private readonly RoleManager<Role> roleManager;
   

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, AuthRules authRules, RoleManager<Role> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.authRules = authRules;
            this.roleManager = roleManager;
        }

        public async Task Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var use = await userManager.FindByEmailAsync(request.Email);
            await authRules.UserShouldNotBeExist(use);
            User user = mapper.Map<User>(request);
            IdentityResult result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                if(! await roleManager.RoleExistsAsync(request.Role))
                {
                    await roleManager.CreateAsync(new Role()
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Role,
                        NormalizedName = request.Role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                    await userManager.AddToRoleAsync(user, request.Role);
                }
            }
            (Type t, object o) = await EntitiyFactories.CreateEntityForRegister(request.Role, request.HotelsId, user);
            var method = typeof(UnitExtensions).GetMethod("AddEntityAsync").MakeGenericMethod(o.GetType());
            await (Task)method.Invoke(null, new object[] { unitOfWork, o });
            var refUser = await unitOfWork.readRepository<Member>().GetByExpression(predicate:x=> x.ReferansCode == request.ReferansCode,enableTracking:true);
            refUser.CoinCount += 10;
            await unitOfWork.writeRepository<Member>().UpdateAsync(refUser);
            await unitOfWork.SaveAsync();


        }
    }
}
