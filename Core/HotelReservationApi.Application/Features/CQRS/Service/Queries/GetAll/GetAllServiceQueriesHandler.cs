using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Service.Queries.GetAll
{
    public class GetAllServiceQueriesHandler : IRequestHandler<GetAllServiceQueriesRequest, List<GetAllServiceQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllServiceQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllServiceQueriesResponse>> Handle(GetAllServiceQueriesRequest request, CancellationToken cancellationToken)
        {
           var service = await  unitOfWork.readRepository<Domain.Entities.Service>().GetAllAsync(predicate: x => x.HotelsId == request.Id, enableTracking: false);
            var services = mp.Map<List<GetAllServiceQueriesResponse>>(service);
            return services;
        }
    }
}
