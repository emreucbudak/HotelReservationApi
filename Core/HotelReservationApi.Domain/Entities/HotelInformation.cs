﻿using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Domain.Entities
{
    public class HotelInformation : BaseEntity
    {
        public HotelInformation()
        {
        }

        public HotelInformation(string aboutHotel, TimeSpan checkInTime, TimeSpan checkOutTime, int hotelsId)
        {
            AboutHotel = aboutHotel;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            HotelsId = hotelsId;
        }


        public string AboutHotel { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public  int HotelsId { get; set; }  
        public Hotels Hotels { get; set; }
    }
}
