using FluentValidation;

namespace HotelReservationApi.Application.Validate
{
    public class ReservationValidator : AbstractValidator<Domain.Entities.Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi boş olamaz").LessThan(x => x.EndDate).WithMessage("Başlangıç tarihi, bitiş tarihinden önce olmalıdır");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Bitiş tarihi boş olamaz").GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalıdır");
            RuleFor(x => x.HotelsId).NotEmpty().WithMessage("Otel ID'si boş olamaz").GreaterThan(0).WithMessage("Otel ID'si 0'dan büyük olmalı");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalı");
            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Rezervasyon tarihi boş olamaz").LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Rezervasyon tarihi bugünden sonraki bir tarih olamaz");
        }
    }
}
