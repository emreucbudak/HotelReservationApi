namespace HotelReservationApi.Application.RabbitMq.Models
{
    public class TwoFactorMessage
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
