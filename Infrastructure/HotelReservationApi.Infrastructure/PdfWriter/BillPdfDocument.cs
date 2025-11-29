using HotelReservationApi.Application.RabbitMq.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq;

namespace HotelReservationApi.Infrastructure.PdfWriter
{
    public class BillPdfDocument : IDocument,IAsyncDisposable
    {
        private readonly BillPdfModel billPdfModel;
        private readonly string TotalAmountString;

        public BillPdfDocument(BillPdfModel billPdfModel)
        {
            this.billPdfModel = billPdfModel;
            this.TotalAmountString = $"{billPdfModel.TotalAmount:N2} TL";
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;


        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(25);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Header().Element(ComposeHeader);
                page.Content().PaddingVertical(20).Column(column =>
                {
                    column.Item().Element(ComposeCustomerAndPaymentDetails);
                    column.Item().PaddingVertical(15).LineHorizontal(1); 
                    column.Item().Element(ComposeReservationDetails);
                    column.Item().PaddingVertical(15).LineHorizontal(1); 
                    column.Item().Element(ComposeTotalSummary);
                });
                page.Footer().Element(ComposeFooter);
            });
        }
        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem(2).Column(column => 
                {
                    column.Item().Text(billPdfModel.HotelName)
                        .FontSize(16).SemiBold().FontColor("#0070C0"); 

                    column.Item().Text(text =>
                    {
                        text.Span("Fatura No: ").SemiBold();
                        text.Span(billPdfModel.BillNo.ToString().ToUpper());
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Tarih: ").SemiBold();
                        text.Span(billPdfModel.BillCreateDate.ToShortDateString());
                    });
                });


                row.RelativeItem(1).AlignRight().Text("FATURA / ÖDEME DEKONTU") 
                    .FontSize(18).SemiBold().FontColor("#333");
            });
        }

        void ComposeCustomerAndPaymentDetails(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem(1).Column(column => 
                {
                    column.Item().Text("MÜŞTERİ BİLGİLERİ").SemiBold().Underline();
                    column.Item().Text($"Ad Soyad: {billPdfModel.Name}");
                    column.Item().Text($"E-posta: {billPdfModel.Email}");
                    column.Item().Text($"Telefon: {billPdfModel.PhoneNumber}");
                });
                row.RelativeItem(1).Column(column => 
                {
                    column.Item().Text("ÖDEME DETAYLARI").SemiBold().Underline();
                    column.Item().Text($"Ödeme Yöntemi: {billPdfModel.PaymentMethod}");
                    column.Item().Text($"Ödeme Zamanı: {billPdfModel.PaymentTiming}");
                    column.Item().Text($"Ödenen Tutar: {TotalAmountString}").SemiBold();
                });
            });
        }

        void ComposeReservationDetails(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); 
                    columns.RelativeColumn(1); 
                    columns.RelativeColumn(1); 
                    columns.RelativeColumn(1); 
                });

  
                table.Header(header =>
                {
                    header.Cell().Element(BlockHeaderStyle).Text("Hizmet/Oda Açıklaması").SemiBold();
                    header.Cell().Element(BlockHeaderStyle).Text("Kişi Sayısı").SemiBold();
                    header.Cell().Element(BlockHeaderStyle).Text("Gece Sayısı").SemiBold();
                    header.Cell().Element(BlockHeaderStyle).Text("Tutar").SemiBold().AlignRight();


                    header.Cell().ColumnSpan(4).PaddingVertical(5).LineHorizontal(1);
                });


                IContainer BlockHeaderStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).Background("#E8E8E8");


                int roomCount = billPdfModel.RoomTypes.Count > 0 ? billPdfModel.RoomTypes.Count : 1;
                int peoplePerRoom = billPdfModel.PeopleBooked.Count / roomCount;

                foreach (var roomType in billPdfModel.RoomTypes)
                {
                    table.Cell().Element(BlockCellStyle).Text(roomType);
                    table.Cell().Element(BlockCellStyle).Text($"{peoplePerRoom}");
                    table.Cell().Element(BlockCellStyle).Text(billPdfModel.TotalNights.ToString());
                    table.Cell().Element(BlockCellStyle).Text($"{billPdfModel.TotalAmount / roomCount:N2} TL").AlignRight();
                }
            });
        }
        IContainer BlockCellStyle(IContainer container) => container.BorderBottom(1).BorderColor("#DDD").PaddingVertical(5);

        void ComposeTotalSummary(IContainer container)
        {
            container.AlignRight().Column(column =>
            {
                column.Item().Border(1).BorderColor("#333").Padding(5).Width(200).Row(row =>
                {
                    row.RelativeItem(2).Text("ÖDENECEK TOPLAM TUTAR").FontSize(12).SemiBold(); // Hata düzeltildi
                    row.RelativeItem(1).Text(TotalAmountString).FontSize(12).SemiBold().AlignRight(); // Hata düzeltildi
                });

                column.Item().PaddingTop(5).Text("Bu belge ödeme dekontu yerine geçer.").FontSize(8);
            });
        }

        void ComposeFooter(IContainer container)
        {
            container.Text(x =>
            {
                x.Span($"Konaklama Tarihleri: {billPdfModel.StartDate.ToShortDateString()} - {billPdfModel.EndDate.ToShortDateString()}").FontSize(8);
            });
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}