using HotelReservationApi.Application.Features.CQRS.HotelAdress.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelAdress.Command.Delete
{
    public class DeleteHotelAdressCommandHandler : IRequestHandler<DeleteHotelAdressCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteHotelAdressCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHotelAdressCommandRequest request, CancellationToken cancellationToken)
        {
            var hotelAdress = await unitOfWork.readRepository<Domain.Entities.HotelAdress>().GetByExpression(enableTracking:false,predicate:x=> x.Id == request.HotelAdressId);
            if (hotelAdress is null) {
                throw new HotelAdressNotFoundExceptions(request.HotelAdressId);
                }
            await unitOfWork.writeRepository<Domain.Entities.HotelAdress>().DeleteAsync(hotelAdress);
            await unitOfWork.SaveAsync();
        }
    }
}
