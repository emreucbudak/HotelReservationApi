using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Queries.GetById
{
    public class GetHotelAdressByIdQueriesHandler : IRequestHandler<GetHotelAdressByIdQueriesRequest, GetHotelAdressByIdQueriesResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetHotelAdressByIdQueriesHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetHotelAdressByIdQueriesResponse> Handle(GetHotelAdressByIdQueriesRequest request, CancellationToken cancellationToken)
        {
            var hotelAdress = await unitOfWork.readRepository<Domain.Entities.HotelAdress>().GetByExpression(predicate:x=> x.HotelsId == request.HotelId,includable:x=> x.Include(x=> x.Neighborhood).Include(y=> y.City).Include(z=> z.District));
            return new GetHotelAdressByIdQueriesResponse()
            {
                DistrictName = hotelAdress.District.Name,
                Boylam = hotelAdress.Boylam,
                Enlem = hotelAdress.Enlem,
                CityName = hotelAdress.City.Name,
                NeighborhoodName = hotelAdress.Neighborhood.Name,
                Street = hotelAdress.Street
            };
        }
    }
}
