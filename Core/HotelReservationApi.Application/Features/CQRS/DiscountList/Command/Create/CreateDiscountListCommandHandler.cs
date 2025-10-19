using AutoMapper;
using HotelReservationApi.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.DiscountList.Command.Create
{
    public class CreateDiscountListCommandHandler : IRequestHandler<CreateDiscountListCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateDiscountListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(CreateDiscountListCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.IsDiscountForOnlyRoomTypes && (request.DiscountCategoryId == 1 || request.DiscountCategoryId == 2))
            {
                var roomType = await unitOfWork
                    .readRepository<Domain.Entities.RoomTypes>()
                    .GetByExpression(enableTracking: true, predicate: x => x.Id == request.RoomTypeId);

               roomType.DiscountedPrice = roomType.DailyPrice - roomType.DailyPrice * request.DiscountPercentage / 100;
                await unitOfWork.SaveAsync();
            }
            else if (!request.IsDiscountForOnlyRoomTypes && (request.DiscountCategoryId == 1 || request.DiscountCategoryId == 2))
            {
                var hotelRooms = await unitOfWork
                    .readRepository<Domain.Entities.Rooms>()
                    .GetAllAsync(enableTracking: true, predicate: x => x.HotelsId == request.HotelsId);

                foreach (var room in hotelRooms)
                {

                    var roomType = room.RoomTypes;

                    if (roomType != null)
                    {

                       roomType.DiscountedPrice = roomType.DailyPrice - roomType.DailyPrice * request.DiscountPercentage / 100;
                    }
                }


                await unitOfWork.SaveAsync();
            }
            else if (request.IsGlobal)
            {
                var roomTypes = await unitOfWork.readRepository<Domain.Entities.RoomTypes>().GetAllAsync(enableTracking: true);
                foreach (var roomType in roomTypes)
                {
                    if (roomType != null)
                    {

                       roomType.DiscountedPrice =  roomType.DailyPrice - roomType.DailyPrice * request.DiscountPercentage / 100;
                    }
                }
                await unitOfWork.SaveAsync();
            }
                var newDiscountList = mapper.Map<Domain.Entities.DiscountList>(request);
            await unitOfWork.writeRepository<Domain.Entities.DiscountList>().AddAsync(newDiscountList);
            await unitOfWork.SaveAsync();
        }
    }
}
