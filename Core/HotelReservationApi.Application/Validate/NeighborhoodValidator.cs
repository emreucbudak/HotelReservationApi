using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class NeighborhoodValidator : AbstractValidator<Domain.Entities.Neighborhood>
    {
        public NeighborhoodValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Mahalle adı boş olamaz!").MinimumLength(2).WithMessage("Mahalle adı en az 3 karakter olabilir!").MaximumLength(30).WithMessage("Mahalle adı en fazla 30 karakter olabilir!");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("İlçe bilgisi boş olamaz!");
        }
    }
}
