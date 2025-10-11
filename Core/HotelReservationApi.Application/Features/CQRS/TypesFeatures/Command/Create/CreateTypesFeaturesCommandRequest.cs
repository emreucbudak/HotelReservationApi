using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.TypesFeatures.Command.Create
{
    public class CreateTypesFeaturesCommandRequest : IRequest
    {
        public string FeatureName { get; set; }
    }
}
