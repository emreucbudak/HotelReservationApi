using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Command.Create
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommandRequest>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public CreateCouponCommandHandler(IUnitOfWork unitofWork, IMapper mp, IDistributedCache cache)
        {
            _unitofWork = unitofWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task Handle(CreateCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"coupon_{request.CouponCode}";
            if (await cache.GetAsync(cacheKey) is not null)
            {
                await cache.RemoveAsync(cacheKey);
            }
            var coupon =  mp.Map<HotelReservationApi.Domain.Entities.Coupon>(request);
            await _unitofWork.writeRepository<HotelReservationApi.Domain.Entities.Coupon>().AddAsync(coupon);
            await _unitofWork.SaveAsync();
        }
    }
}
