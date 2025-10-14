using FluentValidation;
using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    internal class HotelInformationValidator : AbstractValidator<HotelInformation>
    {
        public HotelInformationValidator()
        {
            RuleFor(x=> x.AboutHotel).MinimumLength(20).WithMessage("Otel hakkında en az 20 karakter olabilir!").MaximumLength(1500).WithMessage("Otel hakkında en fazla 1500 karakter olabilir!").NotEmpty().WithMessage("Otel hakkında boş olamaz!");
            RuleFor(x => x.CheckInTime).NotEmpty().WithMessage("Check-in zamanı boş olamaz!");
            RuleFor(x => x.CheckOutTime).NotEmpty().WithMessage("Check-out zamanı boş olamaz!");
        }
    }
}
