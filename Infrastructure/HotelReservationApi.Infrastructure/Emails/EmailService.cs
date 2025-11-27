using HotelReservationApi.Application.Emails;
using HotelReservationApi.Domain.Entities;
using System.Net.Mail;

namespace HotelReservationApi.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailModel emailModel = new EmailModel();


        public IEmailService Attachment(bool isIncludeFile, string filePath)
        {
            emailModel.FilePath = isIncludeFile ? filePath : null;
            return this;
        }

        public IEmailService Body(string? body, string? verificationCode)
        {
            emailModel.Body = !string.IsNullOrEmpty(verificationCode) ? $"İşte Doğrulama Kodunuz: {verificationCode}" : body;
           return this;
        }

        public async Task SendAsync()
        {
            try
            {
                  using(var message = new MailMessage())
                  {
                      message.To.Add(emailModel.To);
                      message.Subject = emailModel.Subject;
                      message.Body = emailModel.Body;
                      if (!string.IsNullOrEmpty(emailModel.FilePath))
                      {
                          Attachment attachment = new Attachment(emailModel.FilePath);
                          message.Attachments.Add(attachment);
                      }
                      using(var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                      {
                          smtpClient.Port = 587;
                          smtpClient.Credentials = new System.Net.NetworkCredential("hotelapideneme@gmail.com", "qbzyeaooxbuufhzi");
                          smtpClient.EnableSsl = true;
                          await smtpClient.SendMailAsync(message);
                      }
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email gönderilirken bir hata oluştu: " + ex);
            }
        }

        public IEmailService Subject(string subject)
        {
            emailModel.Subject = subject;
            return this;
        }

        public IEmailService To(string to)
        {
            emailModel.To = to;
            return this;
        }
    }
}
