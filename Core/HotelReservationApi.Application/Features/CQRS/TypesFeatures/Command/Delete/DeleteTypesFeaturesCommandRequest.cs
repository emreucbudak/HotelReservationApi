using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Delete
{
    public class DeleteTypesFeaturesCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
