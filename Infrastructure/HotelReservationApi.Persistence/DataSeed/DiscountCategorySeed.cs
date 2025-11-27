using HotelReservationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationApi.Persistence.DataSeed
{
    public class DiscountCategorySeed : IEntityTypeConfiguration<DiscountCategory>
    {
        public void Configure(EntityTypeBuilder<DiscountCategory> builder)
        {
            builder.HasData(new DiscountCategory()
            {
                Id = 1,
                DiscountCategoryName = "Gün İndirimi"
            },new DiscountCategory()
            {
                Id = 2,
                DiscountCategoryName = "Rezervasyon Tarihi İndirimi"
            }, new DiscountCategory()
            {
                Id = 3,
                DiscountCategoryName = "Kalma Tarihi İndirimi"
            });
        }
    }
}
