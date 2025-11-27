using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class CouponValidator : AbstractValidator<Domain.Entities.Coupon>
    {
        public CouponValidator()
        {
            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("Kupon kodu boş olamaz!")
                .MinimumLength(5).WithMessage("Kupon kodu en az 5 karakter olmalıdır!")
                .MaximumLength(12).WithMessage("Kupon kodu en fazla 12 karakter olmalıdır!").NotNull().NotEmpty().WithMessage("Kupon kodu boş veya null olamaz!");
            RuleFor(x => x.DiscountPercentage).GreaterThan(0).WithMessage("İndirim yüzdesi 0'dan büyük olmalıdır!").NotEmpty().WithMessage("İndirim oranı boş olamaz");
            RuleFor(x => x.MaxUsageCount).GreaterThan(0).WithMessage("Maksimum kullanım sayısı 0'dan büyük olmalıdır!").NotEmpty().WithMessage("Maksimum kullanım sayısı boş olamaz");
            RuleFor(x => x.CurrentUsageCount).GreaterThanOrEqualTo(0).WithMessage("Mevcut kullanım sayısı 0 veya daha büyük olmalıdır!").NotEmpty().WithMessage("Mevcut kullanım sayısı boş olamaz");
        }
    }
}
