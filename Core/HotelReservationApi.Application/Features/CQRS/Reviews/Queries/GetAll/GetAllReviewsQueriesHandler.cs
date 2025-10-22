using AutoMapper;
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
        private readonly IMapper mapper;

        public GetAllReviewsQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllReviewsQueriesResponse>> Handle(GetAllReviewsQueriesRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Reviews>().GetAllAsync(enableTracking:false,includable:x=> x.Include(y=> y.Hotels));
            var allReviews = mapper.Map<List<GetAllReviewsQueriesResponse>>(reviews);
            return allReviews;
            
        }
    }
}
