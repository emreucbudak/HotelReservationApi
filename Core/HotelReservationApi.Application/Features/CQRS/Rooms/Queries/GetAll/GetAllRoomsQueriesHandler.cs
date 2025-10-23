using HotelReservationApi.Application.Features.CQRS.Rooms.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAll
{
    public class GetAllRoomsQueriesHandler : IRequestHandler<GetAllRoomsQueriesRequest, List<GetAllRoomsQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache cache;

        public GetAllRoomsQueriesHandler(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            this.cache = cache;
        }

        public async Task<List<GetAllRoomsQueriesResponse>> Handle(GetAllRoomsQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"rooms_{request.HotelId}_page_{request.Page}_size_{request.Size}";
            var getRooms = await cache.GetStringAsync(cacheKey);
            if (getRooms is not null)
            {
                return JsonSerializer.Deserialize<List<GetAllRoomsQueriesResponse>>(getRooms);
            }
            var rooms = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetAllWithPaging(enableTracking: false, predicate: x => x.RoomTypes.HotelsId == request.HotelId, page:request.Page,size:request.Size,includable:x=> x.Include(y=> y.RoomTypes).ThenInclude(x=> x.TypesFeatures));
            if (rooms is null)
            {
                throw new RoomsGetByIdNotFoundExceptions(request.HotelId);
            }
            var allRoom =  rooms.Select(x => new GetAllRoomsQueriesResponse
            {
                RoomNumber = x.RoomNumber,
                IsAvailable = x.IsAvailable,
                TypeName = x.RoomTypes.TypeName,
                HowManyPeople = x.RoomTypes.HowManyPeople,
                Price = x.RoomTypes.DailyPrice,
                FeatureName = x.RoomTypes.TypesFeatures.Select(z => z.FeatureName).ToList()
            }).ToList();
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(allRoom),options);
            return allRoom;
        }
    }
}
