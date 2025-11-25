using HotelReservationApi.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(string to, string subject, string body, int verificationCode = 0)
        {
            throw new NotImplementedException();
        }
    }
}
