using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class BillsValidator : AbstractValidator<Domain.Entities.Bills>
    {
        public BillsValidator()
        {
            RuleFor(x=> x.Email).NotEmpty().WithMessage("Email gereklidir")
                .EmailAddress().WithMessage("Yanlış email formatı");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^(?:\+90|0)?5\d{9}$")
                .WithMessage("Geçerli bir telefon numarası giriniz.");

        }
    }
}
