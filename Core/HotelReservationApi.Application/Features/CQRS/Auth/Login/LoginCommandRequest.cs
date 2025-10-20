using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        [DefaultValue("emreucbudak63@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("emreucbudak")]
        public string Password { get; set; }
    }
}
