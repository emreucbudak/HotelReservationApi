using HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Command.Update
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache cache;

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            this.cache = cache;
        }

        public async Task Handle(UpdateCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var coupon = await _unitOfWork.readRepository<Domain.Entities.Coupon>().GetByExpression(enableTracking:false,predicate:x=> x.CouponCode == request.CouponCode);
            if (coupon is null)
                throw new CouponNotFoundExceptions(request.CouponCode);

            if (coupon.IsDeleted)
                throw new CouponUsageReachLimitExceptions(request.CouponCode);

            if (coupon.ExpireDate < DateOnly.FromDateTime(DateTime.Now))
                throw new CouponExpiryTimeExceptions(request.CouponCode);
            var cacheKey = $"coupon_{coupon.CouponCode}";
            if (await cache.GetAsync(cacheKey) is not null)
            {
                await cache.RemoveAsync(cacheKey);
            }
            coupon.CurrentUsageCount += 1;
            coupon.IsDeleted = coupon.CurrentUsageCount == coupon.MaxUsageCount;
            await _unitOfWork.writeRepository<Domain.Entities.Coupon>().UpdateAsync(coupon);
            await _unitOfWork.SaveAsync();

        }
    }
}
