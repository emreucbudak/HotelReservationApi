using HotelReservationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HotelReservationApi.Persistence.DataSeed
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "a1b2c3d4-e5f6-7890-ab12-cdef34567890"
                },
                new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "HotelManager",
                    NormalizedName = "HOTELMANAGER",
                    ConcurrencyStamp = "b2c3d4e5-f6a7-8901-bc23-def456789012"
                },
                new Role
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Reception",
                    NormalizedName = "RECEPTION",
                    ConcurrencyStamp = "c3d4e5f6-a7b8-9012-cd34-ef5678901234"
                },
                new Role
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = "d4e5f6a7-b8c9-0123-de45-f67890123456"
                }
            );
        }
    }
}
