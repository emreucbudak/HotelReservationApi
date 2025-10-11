using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelInformation.Queries.GetByHotelId
{
    public class GetHotelInformationByIdQueriesHandler : IRequestHandler<GetHotelInformationByIdQueriesRequest, GetHotelInformationByIdQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetHotelInformationByIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GetHotelInformationByIdQueriesResponse> Handle(GetHotelInformationByIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotelInformation = await unitOfWork.readRepository<Domain.Entities.HotelInformation>().GetByExpression(enableTracking: false, predicate: x => x.Id == request.HotelsId);
            return mapper.Map<GetHotelInformationByIdQueriesResponse>(hotelInformation);
        }
    }
}
