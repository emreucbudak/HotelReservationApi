using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetByRoomTypeId
{
    public class GetDiscountByRoomTypeIdQueriesHandler : IRequestHandler<GetDiscountByRoomTypeIdQueriesRequest, List<GetDiscountByRoomTypeIdQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetDiscountByRoomTypeIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetDiscountByRoomTypeIdQueriesResponse>> Handle(GetDiscountByRoomTypeIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var discountList = await _unitOfWork.readRepository<Domain.Entities.DiscountList>().GetAllAsync(enableTracking:false,predicate:x=> x.RoomTypeId == request.RoomTypeId);
            return mapper.Map<List<GetDiscountByRoomTypeIdQueriesResponse>>(discountList);
        }
    }
}
