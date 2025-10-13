using AutoMapper;
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
        private readonly IMapper mapper;

        public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public  async Task Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = mapper.Map<Domain.Entities.Reservation>(request);
            await unitOfWork.writeRepository<Domain.Entities.Reservation>().AddAsync(reservation);
            await unitOfWork.SaveAsync();
        }
    }
}
