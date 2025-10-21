using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetAll
{
    public class GetAllCouponQueriesHandler : IRequestHandler<GetAllCouponQueriesRequest, List<GetAllCouponQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public GetAllCouponQueriesHandler(IUnitOfWork unitOfWork, IMapper mp, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task<List<GetAllCouponQueriesResponse>> Handle(GetAllCouponQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = "allCoupon";
            var allCoupon = await cache.GetStringAsync(cacheKey);
            if (allCoupon is not null) {
                return JsonSerializer.Deserialize<List<GetAllCouponQueriesResponse>>(allCoupon);
            }
            var coupons = await unitOfWork.readRepository<Domain.Entities.Coupon>().GetAllAsync(enableTracking: false);

            var mapped = mp.Map<List<GetAllCouponQueriesResponse>>(coupons);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), 
                SlidingExpiration = TimeSpan.FromMinutes(2) 
            }; 
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(mapped),options);
            return mapped;
        }
    }
}
