using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelCategory.Command.Create
{
    public class CreateHotelCategoryCommandHandler : IRequestHandler<CreateHotelCategoryCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateHotelCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateHotelCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var newHotelCategory = new Domain.Entities.HotelCategory
            {
                HotelCategoryName = request.HotelCategoryName
            };
            await unitOfWork.writeRepository<HotelReservationApi.Domain.Entities.HotelCategory>().AddAsync(newHotelCategory);
            await unitOfWork.SaveAsync();
        }
    }
}
