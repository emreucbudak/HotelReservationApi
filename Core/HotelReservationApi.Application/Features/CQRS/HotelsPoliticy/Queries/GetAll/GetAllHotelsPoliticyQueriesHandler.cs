using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Queries.GetAll
{
    public class GetAllHotelsPoliticyQueriesHandler : IRequestHandler<GetAllHotelsPoliticyQueriesRequest, List<GetAllHotelsPoliticyQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllHotelsPoliticyQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllHotelsPoliticyQueriesResponse>> Handle(GetAllHotelsPoliticyQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotelsPoliticy = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HotelsPoliticy>().GetAllAsync(enableTracking: false,predicate:x=> x.HotelId == request.HotelId);
            if (hotelsPoliticy is null)
            {
                throw new HotelsPoliticyByIdNotFoundExceptions(request.HotelId);
            }
            return mapper.Map<List<GetAllHotelsPoliticyQueriesResponse>>(hotelsPoliticy);
        }
    }
}
