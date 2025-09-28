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
    public class PaymentTimingConfiguration : IEntityTypeConfiguration<PaymentTiming>
    {
        public void Configure(EntityTypeBuilder<PaymentTiming> builder)
        {
            builder.HasData(new PaymentTiming
            {
                Id = 1,
                TimingName = "Hemen Öde"
            },  new PaymentTiming
            {
                Id = 2,
                TimingName = "Otelde Öde"
            });
        }
    }
}
