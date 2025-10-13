using HotelReservationApi.Application.Features.CQRS.HotelsService.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsService.Command.Delete
{
    public class DeleteHotelsServiceCommandHandler : IRequestHandler<DeleteHotelsServiceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelsServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelsServiceCommandRequest request, CancellationToken cancellationToken)
        {
           var hotelsService =  await unitOfWork.readRepository<Domain.Entities.HotelServices>().GetByExpression(enableTracking: false, predicate: x => x.Id == request.Id);
            if(hotelsService is null )
            {
                throw new HotelsServiceNotFoundExceptions(request.Id);
            }
             await unitOfWork.writeRepository<Domain.Entities.HotelServices>().DeleteAsync(hotelsService);
              await unitOfWork.SaveAsync();
        }

    }
}
