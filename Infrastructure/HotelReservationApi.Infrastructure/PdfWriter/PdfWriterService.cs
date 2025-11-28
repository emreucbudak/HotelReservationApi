using HotelReservationApi.Application.PdfWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Infrastructure.PdfWriter
{
    public class PdfWriterService : IPdfWriter
    {
        public Task WriteBillPdf(string bill, string paymentMethod, string paymentTiming, IList<string> peopleBooked)
        {
            throw new NotImplementedException();
        }
    }
}
