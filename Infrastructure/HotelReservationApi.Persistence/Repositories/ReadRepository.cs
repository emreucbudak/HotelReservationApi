using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Persistence.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private DbSet<T> _dbSet { get => _context.Set<T>(); }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? ordered = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includable = null, bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if (includable != null)
                query = includable(query);
            if (ordered != null)
                query = ordered(query);
            return await query.ToListAsync();

        }

        public async Task<IList<T>> GetAllWithPaging(int page = 1, int size = 5, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? ordered = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includable = null, bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if (includable != null)
                query = includable(query);
            if (ordered != null)
                query = ordered(query);
            return await query.Skip((page - 1) * size).Take(size).ToListAsync();



        }

        public async Task<T> GetByExpression(bool enableTracking = true, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includable = null)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if(includable != null)
                query = includable(query);
            return await  query.FirstOrDefaultAsync();

        }
    }
}
