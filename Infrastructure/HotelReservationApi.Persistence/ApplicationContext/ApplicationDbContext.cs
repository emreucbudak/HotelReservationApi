using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace HotelReservationApi.Persistence.ApplicationContext
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<HotelAdress> HotelAdresses { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<HotelImages> HotelImages { get; set; }
        public DbSet<HowFarSpecialPlace> HowFarSpecialPlaces { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<HotelInformation> HotelInformations { get; set; }
        public DbSet<HotelsPoliticy> HotelsPoliticies { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomTypes> RoomTypes { get; set; }
        public DbSet<TypesFeatures> TypesFeatures { get; set; }

        public DbSet<HotelServices> HotelServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SpecialPlaceCategory> SpecialPlaceCategories { get; set; }
        public DbSet<AdsBanner> AdsBanner { get; set; }
        public DbSet<DiscountList> discountLists { get; set; }
        public DbSet<NewsPopUp> newsPopUps { get; set; }
        public DbSet<Bills> bills { get; set; }
        public DbSet<PaymentMethod> paymentMethods { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<Gender>  genders { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<FAQ> fAQs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.HotelManager)
                .WithOne(h => h.User)
                .HasForeignKey<HotelManager>(h => h.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Reception)
                .WithOne(r => r.User)
                .HasForeignKey<Reception>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<Member>(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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




            });
        }
    }
}
