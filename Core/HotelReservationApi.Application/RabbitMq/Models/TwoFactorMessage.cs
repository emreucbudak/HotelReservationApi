using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.RabbitMq.Models
{
    public class TwoFactorMessage
    {
        public string ReceiverEmail { get; set; }
        public string VerificationCode { get; set; }
    }
}
