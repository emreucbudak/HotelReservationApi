using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetByName
{
    public class GetCouponByNameQueriesHandler : IRequestHandler<GetCouponByNameQueriesRequest, GetCouponByNameQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public GetCouponByNameQueriesHandler(IUnitOfWork unitOfWork, IMapper mp, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task<GetCouponByNameQueriesResponse> Handle(GetCouponByNameQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"coupon_{request.CouponCode}";
            var cacheCoupon = await cache.GetAsync(cacheKey);
            if (cacheCoupon is not null) {
                return  JsonSerializer.Deserialize<GetCouponByNameQueriesResponse>(cacheCoupon);
            }

            var coupon = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Coupon>().GetByExpression(enableTracking:false,predicate:x=> x.CouponCode == request.CouponCode);
            if (coupon is null) {
                throw new CouponNotFoundExceptions(request.CouponCode);
            }
            var mapped = mp.Map<GetCouponByNameQueriesResponse>(request);
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(mapped));
            return mapped;
        }
    }
}
