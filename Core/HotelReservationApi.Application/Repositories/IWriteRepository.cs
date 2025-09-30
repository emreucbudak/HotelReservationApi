using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Repositories
{
    public interface IWriteRepository<T> : IBaseEntity
    {
        public Task AddAsync (T model);
        public Task DeleteAsync (T model);
        public Task UpdateAsync (T model);

    }
}
