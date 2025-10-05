using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetAll
{
    public class GetAllCouponQueriesHandler : IRequestHandler<GetAllCouponQueriesRequest, List<GetAllCouponQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllCouponQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllCouponQueriesResponse>> Handle(GetAllCouponQueriesRequest request, CancellationToken cancellationToken)
        {
            var coupons = await unitOfWork.readRepository<Domain.Entities.Coupon>().GetAllAsync(enableTracking: false);
            var mapped = mp.Map<List<GetAllCouponQueriesResponse>>(coupons);
            return mapped;
        }
    }
}
