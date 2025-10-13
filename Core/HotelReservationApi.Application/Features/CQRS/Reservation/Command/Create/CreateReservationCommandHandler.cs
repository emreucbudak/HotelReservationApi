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

        public  async Task Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            List<HotelReservationApi.Domain.Entities.Customer> customers = new();
            List<HotelReservationApi.Domain.Entities.Rooms> rooms = new();
            foreach (int customerId in request.Customer)
            {
                var customer = unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Customer>().GetByExpression(predicate: x => x.Id == customerId, enableTracking: false).Result;
                customers.Add(customer);
   
            }
            foreach (int roomId in request.Rooms)
            {
                var room = unitOfWork.readRepository<HotelReservationApi.Domain.Entities.Rooms>().GetByExpression(predicate: x => x.Id == roomId, enableTracking: false).Result;
                rooms.Add(room);
            }
            Domain.Entities.Reservation reservation = new Domain.Entities.Reservation
            {
                Rooms = rooms,
                Customer = customers,
                StartDate = request.StartDate,
                EndDate = request.EndDate

            };
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().AddAsync(reservation);
            await unitOfWork.SaveAsync();
        }
    }
}
