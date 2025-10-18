using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByMemberId
{
    public class GetAllReservationByMemberIdQueriesHandler : IRequestHandler<GetAllReservationByMemberIdQueriesRequest, List<GetAllReservationByMemberIdQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetAllReservationByMemberIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllReservationByMemberIdQueriesResponse>> Handle(GetAllReservationByMemberIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.readRepository<Domain.Entities.Reservation>().GetAllWithPaging(enableTracking:false,predicate:x=> x.MemberId == request.MemberId,page:request.PageNumber,size:request.PageSize);
            return mapper.Map<List<GetAllReservationByMemberIdQueriesResponse>>(reservation);
        }
    }
}
