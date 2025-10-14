using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class PriceListValidator : AbstractValidator<Domain.Entities.PriceList>
    {
        public PriceListValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı");
            RuleFor(x=> x.DiscountListId).NotEmpty().WithMessage("İndirim  boş olamaz").GreaterThan(0).WithMessage("İndirim idsi 0 dan büyük olmalı!");
        }
    }
}
