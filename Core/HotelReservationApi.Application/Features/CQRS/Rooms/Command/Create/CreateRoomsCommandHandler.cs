using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Rooms.Command.Create
{
    public class CreateRoomsCommandHandler : IRequestHandler<CreateRoomsCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mp;

        public CreateRoomsCommandHandler(IUnitOfWork unitOfWork, IMapper mp)
        {
            this.unitOfWork = unitOfWork;
            this.mp = mp;
        }

        public async Task Handle(CreateRoomsCommandRequest request, CancellationToken cancellationToken)
        {
            var newRoom = mp.Map<Domain.Entities.Rooms>(request);
            await unitOfWork.writeRepository<Domain.Entities.Rooms>().AddAsync(newRoom);
            await unitOfWork.SaveAsync();   
        }
    }
}
