namespace HotelReservationApi.Application.RabbitMq.Settings
{
    public class RabbitMqSettings
    {

        public string HostName { get; set; }
        public int Port { get; set; }


        public string UsernameFile { get; set; }
        public string PasswordFile { get; set; }


        public string Username { get; set; }
        public string Password { get; set; }
    }
}
