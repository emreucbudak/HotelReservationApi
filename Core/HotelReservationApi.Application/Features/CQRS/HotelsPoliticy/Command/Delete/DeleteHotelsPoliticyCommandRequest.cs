using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.HotelsPoliticy.Command.Delete
{
    public class DeleteHotelsPoliticyCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
    
    }

