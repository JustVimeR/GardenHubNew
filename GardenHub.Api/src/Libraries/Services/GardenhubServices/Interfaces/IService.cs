using Microsoft.EntityFrameworkCore.Query;
using Models;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IService<T> where T : IEntityBase
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(PaginationFilter paginationFilter, SortFilter sortFilter);

    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null);
    Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter);

    Task<T> PostAsync(T entity);

    Task<T> PutAsync(T entity);
    Task DeleteAsync(T entity, bool softDelete = false);
    Task DeleteAllAsync(IList<T> entities, bool softDelete = false);
    Task<T> PatchAsync(T entity);

    Task UpdateManyAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
}
