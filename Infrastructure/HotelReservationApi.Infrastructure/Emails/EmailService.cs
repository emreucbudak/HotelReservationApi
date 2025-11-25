using HotelReservationApi.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string to, string subject, string body, int? verificationCode)
        {
            try
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.To.Add(to);
                    mailMessage.Subject = verificationCode is null ? subject : "Doğrulama Kodu";
                    mailMessage.Body = verificationCode is null ? body : $"Doğrulama onay Kodunuz : {verificationCode} {DateTime.Now}";
                    mailMessage.From = new MailAddress("hotelapideneme@gmail.com");

                    using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential("hotelapideneme@gmail.com", "qbzyeaooxbuufhzi");
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Email gönderilirken bir hata oluştu.", ex);


            }
        }
    }
}
