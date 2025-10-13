using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.Reviews.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reviews.Queries.GetByHotelId
{
    public class GetReviewsByHotelIdQueriesHandler : IRequestHandler<GetReviewsByHotelIdQueriesRequest, List<GetReviewsByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetReviewsByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetReviewsByHotelIdQueriesResponse>> Handle(GetReviewsByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var reviews = await unitOfWork.readRepository<Domain.Entities.Reviews>().GetAllAsync(predicate: x => x.HotelsId == request.HotelsId, enableTracking: false);
            if (reviews is null)
            {
                throw new ReviewsByHotelIdNotFoundExceptions(request.HotelsId);
            }
            var reviewList = mp.Map<List<GetReviewsByHotelIdQueriesResponse>>(reviews);
            return reviewList;
        }
    }
}
