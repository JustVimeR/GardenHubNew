using Models;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IService<T> where T : EntityBase
    {
        T? GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null);
        List<T> GetWhere(Expression<Func<T, bool>> predicate);
        List<T> GetWhere(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter);
        List<T> GetAll();
        List<T> GetAll(PaginationFilter paginationFilter, SortFilter sortFilter);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T[]> PostAsync(T[] entities);
        Task<T> PutAsync(T entity);
        Task DeleteAsync(T entity, bool softDelete = false);
        Task DeleteAllAsync(IList<T> entities, bool softDelete = false);
        Task<T> PatchAsync(T entity);
    }
}
