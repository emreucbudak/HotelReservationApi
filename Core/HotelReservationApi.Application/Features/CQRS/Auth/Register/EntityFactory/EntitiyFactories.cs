using HotelReservationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Register.EntityFactory
{
    public class EntitiyFactories
    {
        public async static Task<(Type t, object o)> CreateEntityForRegister (string Role,int hotelsId,User user)
        {
            string referansCode = "HC-"+Guid.NewGuid().ToString("N")[..16].ToUpper();
            var createdEntity = Role switch
            {
                "HotelManager" => (object)new HotelManager(hotelsId, user),
                "Admin" => (object)new Admin(user.Id),
                "Reception" => (object)new Reception(user, hotelsId),
                "Member" => (object)new Member(user, referansCode, 0),
                _ => throw new ArgumentException("Bilinmeyen rol seçimi")
            };
            return (createdEntity.GetType(), createdEntity);

        }
    }
}
