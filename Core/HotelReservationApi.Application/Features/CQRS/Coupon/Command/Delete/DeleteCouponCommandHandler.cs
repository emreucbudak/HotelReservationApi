using HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Command.Delete
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache cache;
        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            this.cache = cache;
        }

        public async Task Handle(DeleteCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var coupon =  await _unitOfWork.readRepository<Domain.Entities.Coupon>().GetByExpression(predicate: x=> x.Id == request.Id, enableTracking:true);
            if(coupon is null)
            {
                throw new CouponNotFoundExceptions(request.Id);
            }
            var cacheKey =  $"coupon_{coupon.CouponCode}";
            if (await cache.GetAsync(cacheKey) is not null)
            {
                await cache.RemoveAsync(cacheKey);
            }
            coupon.IsDeleted = true;
            await _unitOfWork.writeRepository<Domain.Entities.Coupon>().UpdateAsync(coupon);
            await _unitOfWork.SaveAsync();


        }
    }
}
