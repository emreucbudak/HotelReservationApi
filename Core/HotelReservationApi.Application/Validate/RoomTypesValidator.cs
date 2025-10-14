using FluentValidation;
using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class RoomTypesValidator : AbstractValidator<RoomTypes>
    {
        public RoomTypesValidator()
        {
            RuleFor(rt => rt.TypeName)
                .NotEmpty().WithMessage("Oda tipi adı boş olamaz")
                .MinimumLength(10).WithMessage("Oda tipi adı en az 10 karakter olabilir!")
                .MaximumLength(50).WithMessage("Oda tipi adı en fazla 50 karakter olabilir!");
            RuleFor(rt => rt.HowManyPeople)
                .InclusiveBetween(1, 10).WithMessage("Oda tipi en az 1 en fazla 10 kişi olabilir!")
                .NotEmpty().WithMessage("Oda tipi kişi sayısı boş olamaz");
        }
    }
}
