using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
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


        public CreateReservationCommandHandler(IMapper mapper, IDistributedCache cache)
        {

            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<CreateReservationCommandResponse> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var newReservation = mapper.Map<Domain.Entities.Reservation>(request);
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
