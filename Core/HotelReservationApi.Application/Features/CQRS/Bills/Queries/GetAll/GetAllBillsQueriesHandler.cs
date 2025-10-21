using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll
{
    public class GetAllBillsQueriesHandler : IRequestHandler<GetAllBillsQueriesRequest, List<GetAllBillsQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public GetAllBillsQueriesHandler(IUnitOfWork unitOfWork, IMapper mp, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task<List<GetAllBillsQueriesResponse>> Handle(GetAllBillsQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"bills_page_{request.PageCount}_size_{request.PageSize}";
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (cachedData is not null)
            {
                return JsonSerializer.Deserialize<List<GetAllBillsQueriesResponse>>(cachedData)!;
            }
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetAllWithPaging(enableTracking:false,page:request.PageCount,size:request.PageSize);
            var mappedBills=  mp.Map<List<GetAllBillsQueriesResponse>>(bills);
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedBills));
            return mappedBills;
        }
    }
}
