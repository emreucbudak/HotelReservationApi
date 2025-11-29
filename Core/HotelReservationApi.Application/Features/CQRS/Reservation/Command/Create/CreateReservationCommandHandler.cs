using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest,CreateReservationCommandResponse>
    {
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;
        private readonly IUnitOfWork unitOfWork;


        public CreateReservationCommandHandler(IMapper mapper, IDistributedCache cache, IUnitOfWork unitOfWork)
        {

            this.mapper = mapper;
            this.cache = cache;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateReservationCommandResponse> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.readRepository<Member>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.MemberId,includable:x=> x.Include(y=> y.User));
            var hotel = await unitOfWork.readRepository<Domain.Entities.Hotels>().GetByExpression(enableTracking:false,predicate: x=> x.Id == request.HotelsId);
            var rooms = await unitOfWork.readRepository<ReservationRoom>()
                .GetAllAsync(predicate:x => request.ReservationRooms.Contains(x.RoomId),
                    includable: x => x.Include(r => r.Room).ThenInclude(rt => rt.RoomTypes));

            var reservationRooms = rooms.ToList();

            var newReservation = new Domain.Entities.Reservation()
            {
                Member = member,
                Hotels = hotel,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ReservationDate = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = request.TotalPrice,
                reservationRooms = reservationRooms
            };

            var paymentReservationToken = Guid.NewGuid().ToString();
            await cache.SetStringAsync(paymentReservationToken, System.Text.Json.JsonSerializer.Serialize(newReservation), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            return new()
            {
                ReservationTempCode = paymentReservationToken
            };
        }
    }
}
