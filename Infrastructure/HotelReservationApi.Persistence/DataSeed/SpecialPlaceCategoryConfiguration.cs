using HotelReservationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Persistence.DataSeed
{
    public class SpecialPlaceCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.SpecialPlaceCategory>
    {
        public void Configure(EntityTypeBuilder<SpecialPlaceCategory> builder)
        {
            builder.HasData(new SpecialPlaceCategory
            {
                Id = 1,
                Name = "Havalimanı"
            }, new SpecialPlaceCategory
            {
                Id = 2,
                Name = "Gezilecek Yerler"
            }, new SpecialPlaceCategory
            {
                Id = 3,
                Name = "Alışveriş Merkezleri"
            },
            new SpecialPlaceCategory
            {
                Id = 4,
                Name = "Otobüs Terminali"
            },
            new SpecialPlaceCategory
            {
                Id = 5,
                Name = "Müze"
            }, new SpecialPlaceCategory
            {
                Id = 6,
                Name = "Müze"
            },
            new SpecialPlaceCategory
            {
                Id = 7,
                Name = "Tarihi Mekanlar"
            });
        }
    }
}
