using AutoMapper;
using HotelReservationApi.Application.Features.CQRS.FAQ.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.FAQ.Queries.GetAll
{
    public class GetAllFAQQueriesHandler : IRequestHandler<GetAllFAQQueriesRequest, List<GetAllFAQQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllFAQQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async  Task<List<GetAllFAQQueriesResponse>> Handle(GetAllFAQQueriesRequest request, CancellationToken cancellationToken)
        {
            var faq = await unitOfWork.readRepository<Domain.Entities.FAQ>().GetAllAsync(predicate: x => x.HotelID == request.HotelID, enableTracking: false);
            if (faq is null)
            {
                throw new FAQByHotelIdNotFoundExceptions(request.HotelID);
            }
            var faqs = mp.Map<List<GetAllFAQQueriesResponse>>(faq);
            return faqs;
        }
    }
}
