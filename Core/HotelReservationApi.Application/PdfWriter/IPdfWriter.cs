using HotelReservationApi.Application.RabbitMq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.PdfWriter
{
    public interface IPdfWriter
    {
        Task WriteBillPdf(BillPdfModel pdf);
    }
}
