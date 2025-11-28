using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.PdfWriter
{
    public interface IPdfWriter
    {
        Task WriteBillPdf(string email,string bill,string paymentMethod,string paymentTiming,IList<string> peopleBooked);
    }
}
