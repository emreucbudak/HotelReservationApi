using HotelReservationApi.Domain.Common;

namespace HotelReservationApi.Application.Repositories
{
    public interface IWriteRepository<T> : IBaseEntity
    {
        public Task AddAsync (T model);
        public Task DeleteAsync (T model);
        public Task UpdateAsync (T model);

    }
}
