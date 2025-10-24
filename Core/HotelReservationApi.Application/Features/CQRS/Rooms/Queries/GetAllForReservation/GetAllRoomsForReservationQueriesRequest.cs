using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAllForReservation
{
    public class GetAllRoomsForReservationQueriesRequest : IRequest<List<GetAllRoomsForReservationQueriesResponse>>
    {
        public GetAllRoomsForReservationQueriesRequest(int? page, int? pageSize, DateOnly entryDate, DateOnly leftDate, int howManyPeopleStay, string city, string district)
        {
            Page = page ?? 1;
            PageSize = pageSize ?? 10;
            this.entryDate = entryDate;
            this.leftDate = leftDate;

            HowManyPeopleStay = howManyPeopleStay;
            City = city;
            District = district;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public DateOnly entryDate {  get; set; }
        public DateOnly leftDate { get; set; }

        public int HowManyPeopleStay { get; set; }
        public string City { get; set; }
        public string District { get; set; }

    }
}
