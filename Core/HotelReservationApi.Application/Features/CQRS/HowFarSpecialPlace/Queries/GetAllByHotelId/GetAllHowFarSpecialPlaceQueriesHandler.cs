using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Queries.GetAllByHotelId
{
    public class GetAllHowFarSpecialPlaceQueriesHandler : IRequestHandler<GetAllHowFarSpecialPlaceQueriesRequest, List<GetAllHowFarSpecialPlaceQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllHowFarSpecialPlaceQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllHowFarSpecialPlaceQueriesResponse>> Handle(GetAllHowFarSpecialPlaceQueriesRequest request, CancellationToken cancellationToken)
        {
           var getAllHowFarSpecialPlace = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HowFarSpecialPlace>().GetAllAsync(enableTracking: false,predicate:x=> x.HotelsId == request.HotelId);
            return mapper.Map<List<GetAllHowFarSpecialPlaceQueriesResponse>>(getAllHowFarSpecialPlace);
        }
    }
}
