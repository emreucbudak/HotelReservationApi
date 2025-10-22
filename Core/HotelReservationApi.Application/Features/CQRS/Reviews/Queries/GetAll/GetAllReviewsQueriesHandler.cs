using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetAll
{
    public class GetAllReviewsQueriesHandler : IRequestHandler<GetAllReviewsQueriesRequest, List<GetAllReviewsQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public GetAllReviewsQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<List<GetAllReviewsQueriesResponse>> Handle(GetAllReviewsQueriesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = "Get_All_Reviews";
            var allReviews = await cache.GetStringAsync(cacheKey);
            if (allReviews is not null)
                return JsonSerializer.Deserialize<List<GetAllReviewsQueriesResponse>>(allReviews);
            var reviews = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Reviews>().GetAllAsync(enableTracking:false,includable:x=> x.Include(y=> y.Hotels));
            var getAllReviews = mapper.Map<List<GetAllReviewsQueriesResponse>>(reviews);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10),
            };
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(getAllReviews),options);
            return getAllReviews;
            
        }
    }
}
