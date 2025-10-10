using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAllByHotelId
{
    public class GetAllBillsByHotelIdQueriesHandler : IRequestHandler<GetAllBillsByHotelIdQueriesRequest, List<GetAllBillsByHotelIdQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllBillsByHotelIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllBillsByHotelIdQueriesResponse>> Handle(GetAllBillsByHotelIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetAllWithPaging(enableTracking: false, predicate: x => x.HotelsId == request.HotelId, page: request.PageCount, size: request.PageSize);
            return mp.Map<List<GetAllBillsByHotelIdQueriesResponse>>(bills);
        }
    }
}
