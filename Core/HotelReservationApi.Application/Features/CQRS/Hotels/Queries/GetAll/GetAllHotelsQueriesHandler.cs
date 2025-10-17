using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetAll
{
    public class GetAllHotelsQueriesHandler : IRequestHandler<GetAllHotelsQueriesRequest, List<GetAllHotelsQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mp;
        public GetAllHotelsQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            _unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllHotelsQueriesResponse>> Handle(GetAllHotelsQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotels = await _unitOfWork.readRepository<Domain.Entities.Hotels>().GetAllWithPaging(page:request.pageNumber,size:request.pageSize,enableTracking:false);
            return mp.Map<List<GetAllHotelsQueriesResponse>>(hotels);
        }
    }
}
