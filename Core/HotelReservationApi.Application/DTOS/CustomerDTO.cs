using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.DTOS
{
    public record CustomerDTO
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public DateOnly BirthDate { get; init; }
        public int GenderId { get; init; }
    }
}
