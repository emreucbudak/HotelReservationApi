using HotelReservationApi.Application.Emails;
using HotelReservationApi.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace HotelReservationApi.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailModel emailModel;
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            emailModel = new EmailModel();
            this.emailSettings = emailSettings.Value;
        }

        public IEmailService Attachment(bool isIncludeFile, Stream fileStream, string fileName)
        {
            if (isIncludeFile)
            {
                emailModel.AttachmentStream = fileStream;
                emailModel.AttachmentFileName = fileName;
            }
            else
            {
                emailModel.AttachmentStream = null;
                emailModel.AttachmentFileName = null;
            }
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
                using (var message = new MailMessage())
                {
                    message.To.Add(emailModel.To);
                    message.From = new MailAddress(emailSettings.User);
                    message.Subject = emailModel.Subject;
                    message.Body = emailModel.Body;
                    message.IsBodyHtml = true;

                    if (emailModel.AttachmentStream != null && !string.IsNullOrEmpty(emailModel.AttachmentFileName))
                    {
                        using (var attachment = new Attachment(
                            emailModel.AttachmentStream,
                            emailModel.AttachmentFileName,
                            "application/pdf"))
                        {
                            message.Attachments.Add(attachment);

                            using (var smtpClient = new SmtpClient(emailSettings.Host, emailSettings.Port))
                            {
                                smtpClient.Credentials = new System.Net.NetworkCredential(emailSettings.User, emailSettings.Password);
                                smtpClient.EnableSsl = true;
                                await smtpClient.SendMailAsync(message);
                            }
                        }
                    }
                    else
                    {
                        using (var smtpClient = new SmtpClient(emailSettings.Host, emailSettings.Port))
                        {
                            smtpClient.Credentials = new System.Net.NetworkCredential(emailSettings.User, emailSettings.Password);
                            smtpClient.EnableSsl = true;
                            await smtpClient.SendMailAsync(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email gönderilirken bir hata oluştu: " + ex.Message, ex);
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
