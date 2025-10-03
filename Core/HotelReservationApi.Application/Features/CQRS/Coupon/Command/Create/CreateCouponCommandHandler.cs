using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
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
        private readonly IMapper mp;

        public CreateCouponCommandHandler(IUnitOfWork unitofWork, IMapper mp)
        {
            _unitofWork = unitofWork;
            this.mp = mp;
        }

        public async Task Handle(CreateCouponCommandRequest request, CancellationToken cancellationToken)
        {
            var coupon =  mp.Map<HotelReservationApi.Domain.Entities.Coupon>(request);
            await _unitofWork.writeRepository<HotelReservationApi.Domain.Entities.Coupon>().AddAsync(coupon);
            await _unitofWork.SaveAsync();
        }
    }
}
