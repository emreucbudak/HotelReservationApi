using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class HotelsPoliticyValidator : AbstractValidator<Domain.Entities.HotelsPoliticy>
    {
        public HotelsPoliticyValidator()
        {
            RuleFor(x => x.PoliticyName).NotEmpty().WithMessage("Politika adı boş olamaz!").MinimumLength(5).WithMessage("Politika adı en az 5 karakter olabilir!").MaximumLength(70).WithMessage("Politika adı en fazla 70 karakter olabilir!");
            RuleFor(x => x.PoliticyDescription).NotEmpty().WithMessage("Politika açıklaması boş olamaz!").MinimumLength(10).WithMessage("Politika açıklaması en az 10 karakter olabilir!").MaximumLength(1000).WithMessage("Politika açıklaması en fazla 1000 karakter olabilir!");
        }
    }
}
