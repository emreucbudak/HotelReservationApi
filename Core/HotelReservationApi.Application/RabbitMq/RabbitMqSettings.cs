using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.RabbitMq
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UsernameFile { get; set; }
        public string PasswordFile { get; set; }
    }
}
