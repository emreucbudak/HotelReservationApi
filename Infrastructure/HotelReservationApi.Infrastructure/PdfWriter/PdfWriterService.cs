using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.PdfWriter;
using HotelReservationApi.Application.RabbitMq.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.PdfWriter
{
    public class PdfWriterService : IPdfWriter
    {
        private readonly IEmailService _emailService;

        public PdfWriterService(IEmailService emailService)
        {
            _emailService = emailService;
            QuestPDF.Settings.License = LicenseType.Community;

        }
        public async Task WriteBillPdf(BillPdfModel pdfModel)
        {
            using var pdfStream = GeneratePdfToStream(pdfModel);
            await SendPdfViaEmail(
                pdfModel.Email,
                $"Rezervasyon Dekontu ve Onayınız: {pdfModel.BillNo}",
                 pdfStream,
                $"Dekont_{pdfModel.BillNo}.pdf");
        }
        private MemoryStream GeneratePdfToStream(BillPdfModel pdfModel)
        {
            var stream = new MemoryStream();
             using (var document = new BillPdfDocument(pdfModel))
            {
                document.GeneratePdf(stream);
            }
            stream.Position = 0;

            return stream;
        }
        private async Task SendPdfViaEmail(string toEmail, string subject, Stream pdfStream, string fileName)
        {
            await _emailService
                .To(toEmail)
                .Subject("Rezervasyon Faturası Hk.")
                .Body("Rezervasyon faturanız ekte iletilmiştir.İyi tatiller dileriz!",null)
                .Attachment(true,pdfStream,fileName)
                .SendAsync();
        }
    }
}
