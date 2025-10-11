using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Create
{
    public class CreateHotelsServiceCommandHandler : IRequestHandler<CreateHotelsServiceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateHotelsServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateHotelsServiceCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelsService = new Domain.Entities.HotelServices
            {
                HotelsId = request.HotelsId,
                ServiceId = request.ServiceId
            };
            await unitOfWork.writeRepository<Domain.Entities.HotelServices>().AddAsync(hotelsService);
            await  unitOfWork.SaveAsync();
        }
    }
}
