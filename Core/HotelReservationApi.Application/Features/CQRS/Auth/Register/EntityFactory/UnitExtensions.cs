using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Features.CQRS.Auth.Register.EntityFactory
{
    public static class UnitExtensions
    {
        public static async Task AddEntityAsync<T>(this IUnitOfWork unit, T entity) where T : class, IBaseEntity,new()
        {
            var forAddEntity = unit.writeRepository<T>();
            await forAddEntity.AddAsync(entity);
            await unit.SaveAsync();
        }

    }
}
