namespace HotelReservationApi.Application.Emails
{
    public interface IEmailService
    {
        IEmailService To(string to);
        IEmailService Subject(string subject);
        IEmailService Body(string? body,string? verificationCode);
        IEmailService Attachment(bool isIncludeFile,Stream fileStream, string fileName);
        Task SendAsync();
    }
}
