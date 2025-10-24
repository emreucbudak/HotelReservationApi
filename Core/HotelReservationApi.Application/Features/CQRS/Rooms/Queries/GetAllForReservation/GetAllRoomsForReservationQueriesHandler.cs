using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Queries.GetAllForReservation
{
    public class GetAllRoomsForReservationQueriesHandler : IRequestHandler<GetAllRoomsForReservationQueriesRequest, List<GetAllRoomsForReservationQueriesResponse>>
    {
        private readonly IUnitOfWork unitOfWork;


        public GetAllRoomsForReservationQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
      
        }

        public async Task<List<GetAllRoomsForReservationQueriesResponse>> Handle(GetAllRoomsForReservationQueriesRequest request, CancellationToken cancellationToken)
        {
            var rooms = await unitOfWork.readRepository<Domain.Entities.Rooms>().GetAllWithPaging(enableTracking:false,predicate: x=> x.RoomTypes.Hotels.HotelAdress.City.Name == request.City && x.RoomTypes.Hotels.HotelAdress.District.Name == request.District && !x.reservationRooms.Any(z =>    z.Reservation.StartDate < request.leftDate && z.Reservation.EndDate > request.entryDate)&& x.RoomTypes.HowManyPeople == request.HowManyPeopleStay,page:request.Page,size:request.PageSize,includable:x=> x.Include(x=> x.RoomTypes));
            List<GetAllRoomsForReservationQueriesResponse> allRoom = new();
            foreach (var room in rooms)
            {
                GetAllRoomsForReservationQueriesResponse roomsForReservation = new()
                {
                    TypeName = room.RoomTypes.TypeName,
                    DailyPrice = room.RoomTypes.DailyPrice,
                    DiscountedPrice = room.RoomTypes.DiscountedPrice,
                    HotelId = room.RoomTypes.HotelsId,
                    HowManyPeople = room.RoomTypes.HowManyPeople,
                    typesFeatures = room.RoomTypes.TypesFeatures,
                };
                allRoom.Add(roomsForReservation);

            }
            return allRoom;


        }
    }
}
