using FluentValidation;
using HotelReservationApi.Domain.Entities;

namespace HotelReservationApi.Application.Validate
{
    public class DiscountListValidator : AbstractValidator<DiscountList>
    {
        public DiscountListValidator()
        {
            RuleFor(x => x.DiscountPercentage)
                .GreaterThan(0).WithMessage("İndirim yüzdesi 0'dan büyük olmalıdır!")
                .NotNull().WithMessage("İndirim oranı boş olamaz");

      
                RuleFor(x => x.DiscountStartTime)
                    .NotNull().WithMessage("İndirim başlangıç zamanı null olamaz!")
                    .LessThan(x => x.DiscountEndTime ?? DateTime.MaxValue)
                    .WithMessage("İndirim başlangıç zamanı, indirim bitiş zamanından önce olmalıdır!");

                RuleFor(x => x.DiscountEndTime)
                    .NotNull().WithMessage("İndirim bitiş zamanı null olamaz!")
                    .GreaterThan(x => x.DiscountStartTime ?? DateTime.MinValue)
                    .WithMessage("İndirim bitiş zamanı, indirim başlangıç zamanından sonra olmalıdır!");

                RuleFor(x => x.BookingStartDate)
                    .NotNull().WithMessage("Rezervasyon başlangıç tarihi null olamaz!")
                    .LessThan(x => x.BookingEndDate ?? DateTime.MaxValue)
                    .WithMessage("Rezervasyon başlangıç tarihi, rezervasyon bitiş tarihinden önce olmalıdır!");

                RuleFor(x => x.BookingEndDate)
                    .NotNull().WithMessage("Rezervasyon bitiş tarihi null olamaz!")
                    .GreaterThan(x => x.BookingStartDate ?? DateTime.MinValue)
                    .WithMessage("Rezervasyon bitiş tarihi, rezervasyon başlangıç tarihinden sonra olmalıdır!");
    
        }
    }
}
