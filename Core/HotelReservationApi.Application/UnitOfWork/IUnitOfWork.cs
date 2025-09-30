using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IReadRepository<T> readRepository<T>() where T : class,IBaseEntity,new();
        public IWriteRepository<T> writeRepository<T>() where T : class,IBaseEntity,new();
        Task<int> SaveAsync();
        int Save();
    }
}
