using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Coupon.Queries.GetByName
{
    public class GetCouponByNameQueriesHandler : IRequestHandler<GetCouponByNameQueriesRequest, GetCouponByNameQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetCouponByNameQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<GetCouponByNameQueriesResponse> Handle(GetCouponByNameQueriesRequest request, CancellationToken cancellationToken)
        {
            var coupon = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Coupon>().GetByExpression(enableTracking:false,predicate:x=> x.CouponCode == request.CouponCode);
            var mapped = mp.Map<GetCouponByNameQueriesResponse>(request);
            return mapped;
        }
    }
}
