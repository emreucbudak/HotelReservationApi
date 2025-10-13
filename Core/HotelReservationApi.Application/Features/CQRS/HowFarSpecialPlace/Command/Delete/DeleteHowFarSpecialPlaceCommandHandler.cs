using HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HowFarSpecialPlace.Command.Delete
{
    public class DeleteHowFarSpecialPlaceCommandHandler : IRequestHandler<DeleteHowFarSpecialPlaceCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHowFarSpecialPlaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHowFarSpecialPlaceCommandRequest request, CancellationToken cancellationToken)
        {
            var howFarSpecialPlace = await  unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HowFarSpecialPlace>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            if(howFarSpecialPlace is null)
            {
                throw new HowFarSpecialNotFoundExceptions(request.Id);
            }
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HowFarSpecialPlace>().DeleteAsync(howFarSpecialPlace);
            await unitOfWork.SaveAsync();
        }
    }
}
