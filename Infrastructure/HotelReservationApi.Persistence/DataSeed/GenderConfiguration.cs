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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new Gender
            {
                Id = 1,
                GenderName = "Erkek"
            },
            new Gender
            {
                Id = 2,
                GenderName = "Kadın"
            });
        }
    }
}
