using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAll
{
    public class GetAllRoomsQueriesHandler : IRequestHandler<GetAllRoomsQueriesRequest, List<GetAllRoomsQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRoomsQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllRoomsQueriesResponse>> Handle(GetAllRoomsQueriesRequest request, CancellationToken cancellationToken)
        {
            var rooms = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetAllWithPaging(enableTracking: false, predicate: x => x.HotelsId == request.HotelId, page:request.Page,size:request.Size,includable:x=> x.Include(y=> y.RoomTypes).ThenInclude(x=> x.TypesFeatures).Include(x=> x.PriceList).ThenInclude(z=> z.DiscountList));
            return rooms.Select(x => new GetAllRoomsQueriesResponse
            {
                RoomNumber = x.RoomNumber,
                IsAvailable = x.IsAvailable,
                TypeName = x.RoomTypes.TypeName,
                HowManyPeople = x.RoomTypes.HowManyPeople,
                Price = x.PriceList.DiscountList.IsDiscountActive ? x.PriceList.Price-x.PriceList.Price*x.PriceList.DiscountList.DiscountPercentage/100:x.PriceList.Price,
                FeatureName = x.RoomTypes.TypesFeatures.Select(z => z.FeatureName).ToList()
            }).ToList();
        }
    }
}
