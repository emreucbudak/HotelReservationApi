using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Delete
{
    public class DeleteAdsBannerCommandHandler : IRequestHandler<DeleteAdsBannerCommandRequest>
    {
        private readonly IUnitOfWork _unit;

        public DeleteAdsBannerCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task Handle(DeleteAdsBannerCommandRequest request, CancellationToken cancellationToken)
        {
            var ads = await _unit.readRepository<HotelReservationApi.Domain.Entities.AdsBanner>().GetByExpression(predicate: x=> x.Id == request.Id,enableTracking:false);
            await _unit.writeRepository<HotelReservationApi.Domain.Entities.AdsBanner>().DeleteAsync(ads);
            await _unit.SaveAsync();
        }
    }
}
