using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetAll
{
    public class GetAllReviewsQueriesHandler : IRequestHandler<GetAllReviewsQueriesRequest, List<GetAllReviewsQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllReviewsQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllReviewsQueriesResponse>> Handle(GetAllReviewsQueriesRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Reviews>().GetAllAsync(enableTracking:false,includable:x=> x.Include(y=> y.Hotels));
            return reviews.Select(x => new GetAllReviewsQueriesResponse
            {
                Title = x.Title,
                Comment = x.Comment,
                Rating = x.Rating,
                ReviewDate = x.ReviewDate,
                IsUpdated = x.IsUpdated,
                UpdatedDate = x.UpdatedDate,
                HotelName = x.Hotels.HotelName
            }).ToList();
        }
    }
}
