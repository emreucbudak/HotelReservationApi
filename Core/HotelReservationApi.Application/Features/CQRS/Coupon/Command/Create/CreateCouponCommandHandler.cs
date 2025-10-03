using HotelReservationApi.Application.UnitOfWork;
using MediatR;
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

        public CreateCouponCommandHandler(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public Task Handle(CreateCouponCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
