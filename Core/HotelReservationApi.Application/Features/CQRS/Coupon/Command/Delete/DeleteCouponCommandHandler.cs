using HotelReservationApi.Application.Features.CQRS.Coupon.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
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

        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var coupon =  await _unitOfWork.readRepository<Domain.Entities.Coupon>().GetByExpression(predicate: x=> x.Id == request.Id, enableTracking:true);
            if(coupon is null)
            {
                throw new CouponNotFoundExceptions(request.Id);
            }
            coupon.IsDeleted = true;
            await _unitOfWork.writeRepository<Domain.Entities.Coupon>().UpdateAsync(coupon);
            await _unitOfWork.SaveAsync();


        }
    }
}
