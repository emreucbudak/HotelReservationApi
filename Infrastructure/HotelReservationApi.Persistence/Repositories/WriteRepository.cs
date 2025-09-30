using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Persistence.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, new()
    {
        private readonly  ApplicationDbContext _context;

        public WriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private DbSet<T> _dbSet {get=> _context.Set<T>();}
        public async Task AddAsync(T model)
        {
            await _dbSet.AddAsync(model);
        }

        public async Task DeleteAsync(T model)
        {
            await Task.Run(() => _dbSet.Remove(model));
        }

        public async Task UpdateAsync(T model)
        {
            await Task.Run(() => _dbSet.Update(model));
        }
    }
}
