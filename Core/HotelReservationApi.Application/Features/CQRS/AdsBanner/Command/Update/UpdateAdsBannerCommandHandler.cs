using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.AdsBanner.Command.Update
{
    public class UpdateAdsBannerCommandHandler : IRequestHandler<UpdateAdsBannerCommandRequest>
    {
        private readonly IUnitOfWork _unit;

        public UpdateAdsBannerCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task Handle(UpdateAdsBannerCommandRequest request, CancellationToken cancellationToken)
        {
            var ads = await _unit.readRepository<Domain.Entities.AdsBanner>().GetByExpression(predicate: x => x.Id == request.Id, enableTracking: true);
            ads.Title = request.Title;
            ads.Description = request.Description;
            ads.ImageUrl = request.ImageUrl;
            await _unit.writeRepository<Domain.Entities.AdsBanner>().UpdateAsync(ads);
            await _unit.SaveAsync();
        }
    }
}
