using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetByHotelId
{
    public class GetReviewsByHotelIdQueriesHandler : IRequestHandler<GetReviewsByHotelIdQueriesRequest, List<GetReviewsByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;
        private readonly IDistributedCache cache;

        public GetReviewsByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mp, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
            this.cache = cache;
        }

        public async Task<List<GetReviewsByHotelIdQueriesResponse>> Handle(GetReviewsByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"reviews_hotel_{request.HotelsId}_page_{request.pageNumber}_size_{request.pageSize}";
            var cachedReviews = await cache.GetStringAsync(cacheKey);
            if (cachedReviews is not null)
            {
                return JsonSerializer.Deserialize<List<GetReviewsByHotelIdQueriesResponse>>(cachedReviews);
            }
            var reviews = await unitOfWork.readRepository<Domain.Entities.Reviews>().GetAllWithPaging(enableTracking:false,predicate:x=> x.HotelsId == request.HotelsId,page:request.pageNumber,size:request.pageSize);


            if (reviews is null)
            {
                throw new ReviewsByHotelIdNotFoundExceptions(request.HotelsId);
            }
            var reviewList = mp.Map<List<GetReviewsByHotelIdQueriesResponse>>(reviews);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10),
            };
            await cache.SetStringAsync(cacheKey,JsonSerializer.Serialize(reviewList),options);
            return reviewList;
        }
    }
}
