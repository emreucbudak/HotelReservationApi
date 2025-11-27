using FluentValidation;
using HotelReservationApi.Domain.Entities;

namespace HotelReservationApi.Application.Validate
{
    public class ReviewsValidator : AbstractValidator<Reviews>
    {
        public ReviewsValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Yorum başlığı boş olamaz")
                .MinimumLength(5).WithMessage("Başlık en az 5 karakter olabilir!")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir!");
            RuleFor(r => r.Comment)
                 .NotEmpty().WithMessage("Yorum başlığı boş olamaz")
                .MinimumLength(10).WithMessage("Yorum en az 10 karakter olabilir!")
                .MaximumLength(750).WithMessage("Yorum en fazla 750 karakter olabilir!");
            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır!")
                .NotEmpty().WithMessage("Rating Boş Olamaz");
            RuleFor(r => r.ReviewDate)
                .NotEmpty().WithMessage("Yorum Tarihi Boş Olamaz");
            RuleFor(r=> r.UpdatedDate)
                .GreaterThan(r=> r.ReviewDate).When(r=> r.IsUpdated).WithMessage("Güncellenme Tarihi Yorum Tarihinden daha ileri bir tarih olmalıdır!");
        }
    }
}
