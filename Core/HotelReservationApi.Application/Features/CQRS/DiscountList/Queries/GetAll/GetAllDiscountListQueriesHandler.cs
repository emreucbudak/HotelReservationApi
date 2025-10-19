using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Queries.GetAll
{
    public class GetAllDiscountListQueriesHandler : IRequestHandler<GetAllDiscountListQueriesRequest, List<GetAllDiscountListQueriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetAllDiscountListQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GetAllDiscountListQueriesResponse>> Handle(GetAllDiscountListQueriesRequest request, CancellationToken cancellationToken)
        {
            var discountList = await _unitOfWork.readRepository<Domain.Entities.DiscountList>().GetAllAsync(enableTracking:false);
            return mapper.Map<List<GetAllDiscountListQueriesResponse>>(discountList);
        }
    }
}
