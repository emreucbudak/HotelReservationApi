using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Neighborhood.Queries.GetAll
{
    public class GetAllNeighborhoodQueriesHandler : IRequestHandler<GetAllNeighborhoodQueriesRequest, List<GetAllNeighborhoodQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IMapper _mapper;

        public GetAllNeighborhoodQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllNeighborhoodQueriesResponse>> Handle(GetAllNeighborhoodQueriesRequest request, CancellationToken cancellationToken)
        {
            var neighborhoods = await _unitOfWork.readRepository<Domain.Entities.Neighborhood>().GetAllAsync(x => x.DistrictId == request.DistrictId);
            return _mapper.Map<List<GetAllNeighborhoodQueriesResponse>>(neighborhoods);
        }
    }
}
