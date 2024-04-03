using Models;
using Models.DbEntities;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Data.Repos.Interfaces;

public interface IRepository<T> where T : IEntityBase
{
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool ignorePrepareDbSet = false);

    Task<IPagedList<T>> GetWhere(Expression<Func<T, bool>>? predicate, PaginationFilter paginationFilter,
        SortFilter sortFilter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<IPagedList<T>> GetAll(PaginationFilter paginationFilter, SortFilter sortFilter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task Post(T entity);

    void Put(T entity);
    Task UpdateMany(Expression<Func<T, bool>> predicate,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);

    void Delete(T entity);
    void DeleteAll(IEnumerable<T> entities);

    void Patch(T entity);

    Task SaveChangesAsync();
}