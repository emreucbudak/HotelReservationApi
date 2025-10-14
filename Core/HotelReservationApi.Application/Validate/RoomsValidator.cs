using FluentValidation;
using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
