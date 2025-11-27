using FluentValidation;
using HotelReservationApi.Domain.Entities;

namespace HotelReservationApi.Application.Validate
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz!")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır!")
                .MaximumLength(30).WithMessage("İsim en fazla 30 karakter olmalıdır!").NotNull().NotEmpty().WithMessage("İsim boş veya null olamaz!");
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim boş olamaz!")
                .MinimumLength(2).WithMessage("Soyisim en az 2 karakter olmalıdır!")
                .MaximumLength(30).WithMessage("Soyisim en fazla 30 karakter olmalıdır!").NotNull().NotEmpty().WithMessage("Soyisim boş veya null olamaz!");
            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi boş olamaz.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now))
                    .WithMessage("Doğum tarihi bugünden küçük olmalıdır!")
                .GreaterThan(DateOnly.FromDateTime(new DateTime(1900, 1, 1)))
                    .WithMessage("Doğum tarihi 1 Ocak 1900'den büyük olmalıdır!");

        }
    }
}
