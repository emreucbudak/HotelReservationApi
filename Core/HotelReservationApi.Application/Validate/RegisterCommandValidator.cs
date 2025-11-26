using FluentValidation;
using HotelReservationApi.Application.Features.CQRS.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator() {
            RuleFor(x => x.Name)
    .NotEmpty().WithMessage("İsim boş olamaz.")
    .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz email formatı.")
                .Matches(@"^[^@\s]+@(gmail\.com|hotmail\.com|outlook\.com)$")
                    .WithMessage("Sadece gmail.com, hotmail.com veya outlook.com adreslerine izin verilir.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches(@"[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches(@"\d").WithMessage("Şifre en az bir rakam içermelidir.")
                .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olabilir.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Rol boş olamaz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^(?:\+90|0)?5\d{9}$").WithMessage("Geçersiz numara girişi.");

            RuleFor(x => x.HotelsId)
                .GreaterThan(0).WithMessage("Otel ID'si 0 dan büyük olmalı");

            RuleFor(x => x.ReferansCode)
                .Matches(@"^HC-[A-F0-9]{16}$").WithMessage("Referans kodu 'HC-' ile başlamalı ve 16 karakterlik özel key bulunmalı!");

        }
    }
}
