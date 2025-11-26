using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.RabbitMq
{
    public class TwoFactorMessage
    {
        public string ReceiverEmail { get; set; }
        public string VerificationCode { get; set; }
        public string Subject { get; set; }
    }
}
