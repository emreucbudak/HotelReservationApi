using FluentValidation;
using HotelReservationApi.Domain.Entities;

namespace HotelReservationApi.Application.Validate
{
    public class RoomsValidator : AbstractValidator<Rooms>
    {
        public RoomsValidator()
        {
            RuleFor(r=> r.RoomNumber)
                .NotEmpty().WithMessage("Oda Numarası Boş Olamaz")
                .GreaterThan(0).WithMessage("Oda Numarası 0'dan büyük olmalıdır!");
        }
    }
}
