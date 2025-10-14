using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Validate
{
    public class TypesFeaturesValidator : AbstractValidator<Domain.Entities.TypesFeatures>
    {
        public TypesFeaturesValidator()
        {
            RuleFor(tf => tf.FeatureName)
                .NotEmpty().WithMessage("Özellik adı boş olamaz")
                .MinimumLength(3).WithMessage("Özellik adı en az 3 karakter olabilir!")
                .MaximumLength(50).WithMessage("Özellik adı en fazla 50 karakter olabilir!");
        }
    }
}
