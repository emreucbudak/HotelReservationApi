using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Queries.GetAll
{
    public class GetAllAdsBannerQueriesHandler : IRequestHandler<GetAllAdsBannerQueriesRequest, GetAllAdsBannerQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllAdsBannerQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<GetAllAdsBannerQueriesResponse> Handle(GetAllAdsBannerQueriesRequest request, CancellationToken cancellationToken)
        {
            var ads = await unitOfWork.readRepository<Domain.Entities.AdsBanner>().GetByExpression(predicate: x=> x.IsDeleted == false , enableTracking:false);
            var mapped = mp.Map<GetAllAdsBannerQueriesResponse>(ads);
            return mapped;



        }
    }
}
