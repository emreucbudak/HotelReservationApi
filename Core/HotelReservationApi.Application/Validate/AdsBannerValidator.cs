using FluentValidation;
using HotelReservationApi.Domain.Entities;

namespace HotelReservationApi.Application.Validate
{
    public class AdsBannerValidator : AbstractValidator<AdsBanner>
    {
        public AdsBannerValidator()
        {
            RuleFor(x=> x.Title).MinimumLength(20).MaximumLength(50).WithMessage("Başlık 20 ile 50 karakter arasında olmalı").NotEmpty().WithMessage("Başlık boş olamaz");
            RuleFor(x=> x.Description).MinimumLength(50).MaximumLength(500).WithMessage("Açıklama 50 ile 500 karakter arasında olmalı").NotEmpty().WithMessage("Açıklama boş olamaz");
            RuleFor(x=> x.ImageUrl).NotEmpty().WithMessage("Resim kaynağı boş olamaz");
        }
    }
}
