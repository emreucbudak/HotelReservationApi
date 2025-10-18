using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class AdsBanner : BaseEntity
    {
        public AdsBanner()
        {
        }

        public AdsBanner(string title, string description, string ımageUrl)
        {
            Title = title;
            Description = description;
            ImageUrl = ımageUrl;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
