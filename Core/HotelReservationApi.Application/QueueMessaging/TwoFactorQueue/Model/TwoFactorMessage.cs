using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Model
{
    public class TwoFactorMessage
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
