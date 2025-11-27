using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class FAQValidator : AbstractValidator<Domain.Entities.FAQ>
    {
        public FAQValidator()
        {
            RuleFor(x => x.Question)
                .NotEmpty().WithMessage("Soru alanı boş olamaz!")
                .MaximumLength(500).WithMessage("Soru  en fazla 500 karakter olabilir!");
            RuleFor(x => x.Answer)
                .NotEmpty().WithMessage("Cevap alanı boş olamaz!")
                .MaximumLength(2000).WithMessage("Cevap  en fazla 2000 karakter olabilir!");
            RuleFor(x => x.HotelID)
                .GreaterThan(0).WithMessage("Otel Id'si 0 dan büyük olmalı");
        }
    }
}
