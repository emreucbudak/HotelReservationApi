using HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
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

        public UpdateCouponCommandHandler(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var coupon = await _unitOfWork.readRepository<Domain.Entities.Coupon>().GetByExpression(enableTracking:false,predicate:x=> x.CouponCode == request.CouponCode);
            if (coupon is null)
            {
                throw new CouponNotFoundExceptions(request.CouponCode);
            }

            coupon.CurrentUsageCount += coupon.CurrentUsageCount;
            coupon.IsDeleted = coupon.CurrentUsageCount == coupon.MaxUsageCount ? true : false;
            await _unitOfWork.writeRepository<Domain.Entities.Coupon>().UpdateAsync(coupon);
            await _unitOfWork.SaveAsync();

        }
    }
}
