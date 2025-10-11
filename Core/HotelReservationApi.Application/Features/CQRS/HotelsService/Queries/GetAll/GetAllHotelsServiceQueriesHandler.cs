using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Queries.GetAll
{
    public class GetAllHotelsServiceQueriesHandler : IRequestHandler<GetAllHotelsServiceQueriesRequest, List<GetAllHotelsServiceQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllHotelsServiceQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllHotelsServiceQueriesResponse>> Handle(GetAllHotelsServiceQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotelsServices = await unitOfWork.readRepository<Domain.Entities.HotelServices>().GetAllAsync(enableTracking: false,predicate:x=> x.HotelsId == request.HotelsId);
            return mp.Map<List<GetAllHotelsServiceQueriesResponse>>(hotelsServices);
        }
    }
}
