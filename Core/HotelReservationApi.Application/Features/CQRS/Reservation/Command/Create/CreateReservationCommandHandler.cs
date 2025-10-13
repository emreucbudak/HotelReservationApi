using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateReservationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            List<HotelReservationApi.Domain.Entities.Customer> customers = new();
            List<HotelReservationApi.Domain.Entities.Rooms> rooms = new();
            foreach
        }
    }
}
