using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId
{
    public class GetAllReservationByHotelIdQueriesHandler : IRequestHandler<GetAllReservationByHotelIdQueriesRequest, List<GetAllReservationByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllReservationByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllReservationByHotelIdQueriesResponse>> Handle(GetAllReservationByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var reservations = await unitOfWork.readRepository<Domain.Entities.Reservation>().GetAllWithPaging(enableTracking:false,page:request.Page,size:request.PageSize,predicate:x=> x.HotelsId == request.HotelsId);
            var mappedReservations = mapper.Map<List<GetAllReservationByHotelIdQueriesResponse>>(reservations);
            return mappedReservations;
        }
    }
}
