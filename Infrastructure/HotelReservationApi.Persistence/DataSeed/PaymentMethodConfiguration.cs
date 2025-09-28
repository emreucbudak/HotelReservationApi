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
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasData(new PaymentMethod
            {
                Id = 1,
                MethodName = "Kredi Kartı"
            },
            new PaymentMethod
            {
                Id = 2,
                MethodName = "Banka Kartı"
            });
        }
    }
}
