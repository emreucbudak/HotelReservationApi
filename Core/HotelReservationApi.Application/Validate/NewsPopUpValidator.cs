using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class NewsPopUpValidator : AbstractValidator<Domain.Entities.NewsPopUp>
    {
        public NewsPopUpValidator()
        {
            RuleFor(x => x.Title).MinimumLength(10).MaximumLength(50).WithMessage("Başlık 10 ile 50 karakter arasında olmalı").NotEmpty().WithMessage("Başlık boş olamaz");
            RuleFor(x => x.Description).MinimumLength(50).MaximumLength(200).WithMessage("Açıklama 50 ile 200 karakter arasında olmalı").NotEmpty().WithMessage("Açıklama boş olamaz");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Resim kaynağı boş olamaz");
        }
    }
}
