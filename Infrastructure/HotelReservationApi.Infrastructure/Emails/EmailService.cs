using HotelReservationApi.Application.Emails;
using HotelReservationApi.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace HotelReservationApi.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {

            this.emailSettings = emailSettings.Value;
        }

        public IEmailBuilder Builder()
        {
            return new EmailBuilder(emailSettings);
        }
    }
}
