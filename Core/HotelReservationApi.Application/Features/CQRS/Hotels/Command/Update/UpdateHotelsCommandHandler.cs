using HotelReservationApi.Application.Features.CQRS.Hotels.Exceptions;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Hotels.Command.Update
{
    public class UpdateHotelsCommandHandler : IRequestHandler<UpdateHotelsCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHotelsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateHotelsCommandRequest request, CancellationToken cancellationToken)
        {
            var hotels = await _unitOfWork.readRepository<Domain.Entities.Hotels>().GetByExpression(enableTracking:true,predicate:x=> x.Id == request.HotelsId);
            if (hotels is null)
            {
                throw new HotelsNotFoundExceptions(request.HotelsId);
            }
            hotels.HotelName = request.HotelsName;
            await _unitOfWork.writeRepository<Domain.Entities.Hotels>().UpdateAsync(hotels);
            await _unitOfWork.SaveAsync();
        }
    }
}
