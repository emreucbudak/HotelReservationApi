using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class HotelCategoryValidator : AbstractValidator<Domain.Entities.HotelCategory>
    {
        public HotelCategoryValidator()
        {
            RuleFor(x => x.HotelCategoryName)
                .NotEmpty().WithMessage("Otel Kategori alanı boş olamaz!")
                .MinimumLength(2).WithMessage("Otel Kategori en az 2 karakter olabilir!")
  
                .MaximumLength(20).WithMessage("Otel Kategori en fazla 100 karakter olabilir!");
        }
    }
}
