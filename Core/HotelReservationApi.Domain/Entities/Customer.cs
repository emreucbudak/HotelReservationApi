using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
        }

        public Customer(string name, string surname, DateOnly birthDate, int genderId)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            GenderId = genderId;
        }


        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
                

    }
}
