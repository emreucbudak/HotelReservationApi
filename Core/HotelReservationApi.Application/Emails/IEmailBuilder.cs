using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Emails
{
    public interface IEmailBuilder
    {
        IEmailBuilder To(string to);
        IEmailBuilder Subject(string subject);
        IEmailBuilder Body(string? body, string? verificationCode);
        IEmailBuilder Attachment(bool isIncludeFile, Stream fileStream, string fileName);
        Task SendAsync();
    }
}
