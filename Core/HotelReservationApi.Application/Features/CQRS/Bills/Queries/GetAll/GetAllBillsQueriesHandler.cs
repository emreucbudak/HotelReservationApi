using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Bills.Queries.GetAll
{
    public class GetAllBillsQueriesHandler : IRequestHandler<GetAllBillsQueriesRequest, List<GetAllBillsQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public GetAllBillsQueriesHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task<List<GetAllBillsQueriesResponse>> Handle(GetAllBillsQueriesRequest request, CancellationToken cancellationToken)
        {
            var bills = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Bills>().GetAllWithPaging(enableTracking:false,page:request.PageCount,size:request.PageSize);
            return mp.Map<List<GetAllBillsQueriesResponse>>(bills);
        }
    }
}
