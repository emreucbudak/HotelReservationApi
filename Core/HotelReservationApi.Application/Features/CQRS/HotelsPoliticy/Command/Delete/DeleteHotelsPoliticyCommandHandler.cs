using HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Delete
{
    public class DeleteHotelsPoliticyCommandHandler : IRequestHandler<DeleteHotelsPoliticyCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelsPoliticyCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelsPoliticyCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelsPoliticy = await  unitOfWork.readRepository<HotelReservationApi.Domain.Entities.HotelsPoliticy>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: false);
            if (hotelsPoliticy is null)
            {
                throw new HotelsPoliticyNotFoundExceptions(request.Id);
            }
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HotelsPoliticy>().DeleteAsync(hotelsPoliticy);
            await unitOfWork.SaveAsync();
        }
    }
}
