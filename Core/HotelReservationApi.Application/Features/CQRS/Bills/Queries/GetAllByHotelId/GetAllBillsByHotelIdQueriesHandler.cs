using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Bills.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId
{
    public class GetAllBillsByHotelIdQueriesHandler : IRequestHandler<GetAllBillsByHotelIdQueriesRequest, List<GetAllBillsByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public GetAllBillsByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mp, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task<List<GetAllBillsByHotelIdQueriesResponse>> Handle(GetAllBillsByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"bills_hotel_{request.HotelId}_page_{request.PageCount}_size_{request.PageSize}";
            var allBills = await cache.GetStringAsync(cacheKey);
            if (allBills is not null) {
                return JsonSerializer.Deserialize<List<GetAllBillsByHotelIdQueriesResponse>>(allBills);
            }
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetAllWithPaging(enableTracking: false, predicate: x => x.Reservation.HotelsId == request.HotelId, page: request.PageCount, size: request.PageSize);
            if (bills is null || bills.Count == 0)
            {
                throw new BillsNotFoundExceptions(request.HotelId);
            }
            var mappedBills =  mp.Map<List<GetAllBillsByHotelIdQueriesResponse>>(bills);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            };
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(mappedBills),options);
            return mappedBills;
        }
    }
}
