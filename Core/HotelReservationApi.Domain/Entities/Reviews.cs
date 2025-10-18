using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class Reviews : BaseEntity
    {
        public Reviews()
        {
        }

        public Reviews(string title, string comment, decimal rating, DateOnly reviewDate, bool ısUpdated, DateOnly? updatedDate, int hotelsId)
        {
            Title = title;
            Comment = comment;
            Rating = rating;
            ReviewDate = reviewDate;
            IsUpdated = ısUpdated;
            UpdatedDate = updatedDate;
            HotelsId = hotelsId;
        }

        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public DateOnly ReviewDate { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateOnly? UpdatedDate { get; set; }
        public int HotelsId { get; set; }
        public Hotels Hotels { get; set; }  


    }
}
