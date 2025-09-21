using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationApi.Domain.Entities;
namespace HotelReservationApi.Persistence.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelAdress>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Hotels)
                      .WithOne(h => h.HotelAdress)
                      .HasForeignKey<Hotels>(h => h.HotelAdressId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.City)
                      .WithMany()
                      .HasForeignKey(e => e.CityId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(e => e.District)
                      .WithMany() 
                      .HasForeignKey(e => e.DistrictId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Neighborhood)
                      .WithMany() 
                      .HasForeignKey(e => e.NeighborhoodId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.Property(e => e.Location)
                      .HasColumnType("geometry (point,4326)");
            });
        }
    }
}
