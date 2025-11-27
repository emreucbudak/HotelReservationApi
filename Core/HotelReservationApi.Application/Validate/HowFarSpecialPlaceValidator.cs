using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class HowFarSpecialPlaceValidator : AbstractValidator<Domain.Entities.HowFarSpecialPlace>
    {
        public HowFarSpecialPlaceValidator()
        {
            RuleFor(x => x.PlaceName).NotEmpty().WithMessage("Yer adı boş olamaz!").MinimumLength(7).WithMessage("Yer adı en az 7 karakter olabilir!").MaximumLength(100).WithMessage("Yer adı en fazla 100 karakter olabilir!");
            RuleFor(x => x.SpecialPlaceCategoryId).NotEmpty().WithMessage("Özel yer kategorisi boş olamaz!");
            RuleFor(x => x.Distance).NotEmpty().WithMessage("Mesafe boş olamaz!").GreaterThan(0).WithMessage("Mesafe sıfırdan büyük olmalıdır!");
            RuleFor(x => x.HotelsId).NotEmpty().WithMessage("Otel bilgisi boş olamaz!");
        }
    }
}
