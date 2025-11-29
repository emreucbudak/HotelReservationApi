using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Reservation.Command.Create.CreateAfterBill
{
    public class CreateReservationAfterBillCommandHandler : IRequestHandler<CreateReservationAfterBillCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateReservationAfterBillCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateReservationAfterBillCommandRequest request, CancellationToken cancellationToken)
        {
            var domainCustomers = mapper.Map<ICollection<Domain.Entities.Customer>>(request.customerDto);
            var domainReservationRooms = mapper.Map<ICollection<Domain.Entities.ReservationRoom>>(request.ReservationRooms);
            var reservation = mapper.Map<Domain.Entities.Reservation>(request);
            reservation.Customer = domainCustomers;
            reservation.reservationRooms = domainReservationRooms;
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().AddAsync(reservation);
            await unitOfWork.SaveAsync();
        }
    }
}
