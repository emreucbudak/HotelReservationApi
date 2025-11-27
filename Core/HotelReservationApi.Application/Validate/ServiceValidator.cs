using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class ServiceValidator : AbstractValidator<Domain.Entities.Service>
    {
        public ServiceValidator()
        {
            RuleFor(s => s.ServiceName)
                .NotEmpty().WithMessage("Hizmet adı boş olamaz")
                .MinimumLength(5).WithMessage("Hizmet adı en az 5 karakter olabilir!")
                .MaximumLength(50).WithMessage("Hizmet adı en fazla 50 karakter olabilir!");
        }
    }
}
