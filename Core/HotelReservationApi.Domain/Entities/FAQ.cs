using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Domain.Entities
{
    public class FAQ : BaseEntity
    {
        public FAQ()
        {
        }

        public FAQ(string question, string answer, int hotelID)
        {
            Question = question;
            Answer = answer;
            HotelID = hotelID;
        }


        public string Question { get; set; }
        public string Answer { get; set; }
        public int HotelID  { get; set; }
        public Hotels Hotel { get; set; }   
    }
}
