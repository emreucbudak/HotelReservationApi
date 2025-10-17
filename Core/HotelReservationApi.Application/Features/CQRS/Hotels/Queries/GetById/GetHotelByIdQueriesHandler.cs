using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Queries.GetById
{
    public class GetHotelByIdQueriesHandler : IRequestHandler<GetHotelByIdQueriesRequest, GetHotelByIdQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetHotelByIdQueriesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetHotelByIdQueriesResponse> Handle(GetHotelByIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotel = await unitOfWork.readRepository<Domain.Entities.Hotels>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.HotelsId,includable:x=> x.Include(y=> y.HotelAdress).ThenInclude(z=> z.City).Include(y => y.HotelAdress).ThenInclude(z=> z.Neighborhood ).Include(y => y.HotelAdress).ThenInclude(z=>z.District).Include(x=> x.HotelCategory));
            return new GetHotelByIdQueriesResponse()
            {
                HotelName = hotel.HotelName,
                DistrictName = hotel.HotelAdress.District.Name,
                HotelCategoryName = hotel.HotelCategory.HotelCategoryName,
                CityName = hotel.HotelAdress.City.Name,
                PostalCode = hotel.HotelAdress.City.PostalCode,
                NeighboorhoodName = hotel.HotelAdress.Neighborhood.Name,
                Street = hotel.HotelAdress.Street
            };
            
        }
    }
}
