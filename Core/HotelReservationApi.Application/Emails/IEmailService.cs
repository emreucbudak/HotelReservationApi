using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Emails
{
    public interface IEmailService
    {
        IEmailService To(string to);
        IEmailService Subject(string subject);
        IEmailService Body(string? body,string? verificationCode);
        IEmailService Attachment(bool isIncludeFile, string filePath);
        Task SendAsync();
    }
}
