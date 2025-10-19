using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByHotelsId
{
    public class GetDiscountListByHotelsIdQueriesHandler : IRequestHandler<GetDiscountListByHotelsIdQueriesRequest, List<GetDiscountListByHotelsIdQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetDiscountListByHotelsIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetDiscountListByHotelsIdQueriesResponse>> Handle(GetDiscountListByHotelsIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var discountList = await _unitOfWork.readRepository<Domain.Entities.DiscountList>().GetAllAsync(enableTracking:false,predicate:x=> x.HotelsId == request.HotelsId);
            return mapper.Map<List<GetDiscountListByHotelsIdQueriesResponse>>(discountList);
        }
    }
}
