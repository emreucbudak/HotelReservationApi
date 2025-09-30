using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Persistence.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Persistence.UnitOf
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return  await _context.SaveChangesAsync();
        }

        IReadRepository<T> IUnitOfWork.readRepository<T>() => new Repositories.ReadRepository<T>(_context);

        IWriteRepository<T> IUnitOfWork.writeRepository<T>() => new Repositories.WriteRepository<T>(_context);
    }
}
