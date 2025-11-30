using HotelReservationApi.Application.Emails;
using HotelReservationApi.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.Emails
{
    internal class EmailBuilder : IEmailBuilder
    {
        private readonly EmailModel _emailModel = new EmailModel();
        private readonly EmailSettings _emailSettings;

        public EmailBuilder(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public IEmailBuilder Attachment(bool isIncludeFile, Stream fileStream, string fileName)
        {
            if (isIncludeFile)
            {
                _emailModel.AttachmentStream = fileStream;
                _emailModel.AttachmentFileName = fileName;
            }
            else
            {
                _emailModel.AttachmentStream = null;
                _emailModel.AttachmentFileName = null;
            }
            return this;
        }

        public IEmailBuilder Body(string? body, string? verificationCode)
        {
            _emailModel.Body = !string.IsNullOrEmpty(verificationCode) ?
                $"İşte Doğrulama Kodunuz: {verificationCode}" : body;
            return this;
        }

        public IEmailBuilder Subject(string subject)
        {
            _emailModel.Subject = subject;
            return this;
        }

        public IEmailBuilder To(string to)
        {
            _emailModel.To = to;
            return this;
        }

        public async Task SendAsync()
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.To.Add(_emailModel.To);
                    message.From = new MailAddress(_emailSettings.User);
                    message.Subject = _emailModel.Subject;
                    message.Body = _emailModel.Body;
                    message.IsBodyHtml = true;

                    if (_emailModel.AttachmentStream != null && !string.IsNullOrEmpty(_emailModel.AttachmentFileName))
                    {
                        using (var attachment = new Attachment(
                            _emailModel.AttachmentStream,
                            _emailModel.AttachmentFileName,
                            "application/pdf"))
                        {
                            message.Attachments.Add(attachment);

                            using (var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
                            {
                                smtpClient.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                                smtpClient.EnableSsl = true;
                                await smtpClient.SendMailAsync(message);
                            }
                        }
                    }
                    else
                    {
                        using (var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
                        {
                            smtpClient.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
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
    }
}