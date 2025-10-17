using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelImages.Queries.GetAllByHotelId
{
    public class GetAllHotelImagesByIdQueriesHandler : IRequestHandler<GetAllHotelImagesByIdQueriesRequest, List<GetAllHotelImagesByIdQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper map;

        public GetAllHotelImagesByIdQueriesHandler(IUnitOfWork unitOfWork, IMapper map)
        {
            _unitOfWork = unitOfWork;
            this.map = map;
        }

        public async Task<List<GetAllHotelImagesByIdQueriesResponse>> Handle(GetAllHotelImagesByIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var getAllHotelImages = await _unitOfWork.readRepository<Domain.Entities.HotelImages>().GetAllAsync(predicate: x=> x.Id == request.HotelId,enableTracking:false);
            return map.Map<List<GetAllHotelImagesByIdQueriesResponse>>(getAllHotelImages);
        }
    }
}
