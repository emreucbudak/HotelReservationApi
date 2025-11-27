using FluentValidation;
using HotelReservationApi.Application.Features.CQRS.Auth.Login;

namespace HotelReservationApi.Application.Validate
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
    .NotEmpty().WithMessage("Email boş olamaz.")
    .EmailAddress().WithMessage("Geçersiz email formatı.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches(@"[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches(@"\d").WithMessage("Şifre en az bir rakam içermelidir.")
                .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olabilir.");

        }
    }
}
