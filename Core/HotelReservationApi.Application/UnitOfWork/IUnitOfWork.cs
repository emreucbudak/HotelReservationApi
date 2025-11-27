using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Domain.Common;

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
