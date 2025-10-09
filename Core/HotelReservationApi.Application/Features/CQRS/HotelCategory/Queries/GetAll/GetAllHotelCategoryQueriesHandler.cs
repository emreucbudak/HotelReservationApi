using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Queries.GetAll
{
    public class GetAllHotelCategoryQueriesHandler : IRequestHandler<GetAllHotelCategoryQueriesRequest, List<GetAllHotelCategoryQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllHotelCategoryQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<GetAllHotelCategoryQueriesResponse>> Handle(GetAllHotelCategoryQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotelCategories = await unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HotelCategory>().GetAllAsync(enableTracking: false);
            return mapper.Map<List<GetAllHotelCategoryQueriesResponse>>(hotelCategories);
        }
    }
}
