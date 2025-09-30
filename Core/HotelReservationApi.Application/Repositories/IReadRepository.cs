using HotelReservationApi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApi.Application.Repositories
{
    public interface IReadRepository <T> : IBaseEntity
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T,bool>>?predicate = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? ordered = null,
            Func<IQueryable<T>,IIncludableQueryable<T,object>>? includable = null,
            bool enableTracking = true
            );
        Task<IList<T>> GetAllWithPaging (
            int page,
            int size,
            Expression<Func<T,bool>>?predicate = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? ordered = null,
            Func<IQueryable<T>,IIncludableQueryable<T,object>>? includable = null,
            bool enableTracking = true
            );
        Task<T> GetByExpression(bool enableTracking = true, Expression<Func<T, bool>>? predicate = null);
    }
    
    }

