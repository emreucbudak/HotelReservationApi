using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Queries.GetAllByHotelId
{
    public class GetAllReservationByHotelIdQueriesHandler : IRequestHandler<GetAllReservationByHotelIdQueriesRequest, List<GetAllReservationByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public GetAllReservationByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<List<GetAllReservationByHotelIdQueriesResponse>> Handle(GetAllReservationByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"reservation_hotel_{request.HotelsId}_page_{request.Page}_size_{request.PageSize}";
            var allReservationHotel = await cache.GetStringAsync(cacheKey);
            if (allReservationHotel is not null)
            {
                return JsonSerializer.Deserialize<List<GetAllReservationByHotelIdQueriesResponse>>(allReservationHotel);
            }
            var reservations = await unitOfWork.readRepository<Domain.Entities.Reservation>().GetAllWithPaging(enableTracking:false,page:request.Page,size:request.PageSize,predicate:x=> x.HotelsId == request.HotelsId);
            var mappedReservations = mapper.Map<List<GetAllReservationByHotelIdQueriesResponse>>(reservations);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2),
            };
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(mappedReservations),options);
            return mappedReservations;
        }
    }
}
