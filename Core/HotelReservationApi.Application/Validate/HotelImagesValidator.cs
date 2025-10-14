﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class HotelImagesValidator : AbstractValidator<Domain.Entities.HotelImages>
    {
        public HotelImagesValidator()
        {
            RuleFor(x=> x.ImageTitle).MinimumLength(5).WithMessage("Resim başlığı en az 5 karakter olabilir!").MaximumLength(50).WithMessage("Resim başlığı en fazla 50 karakter olabilir!").NotEmpty().WithMessage("Resim başlığı boş olamaz!");
        }
    }
}
