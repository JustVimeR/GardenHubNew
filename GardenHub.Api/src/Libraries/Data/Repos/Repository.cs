using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using System.Linq.Dynamic.Core;

namespace Data.Repos;

public class Repository<T> : IRepository<T> where T : class, IEntityBase
{
    protected readonly ApplicationDbContext dataContext;
    protected readonly DbSet<T> dbSet;

    protected virtual IQueryable<T> PrepareDbSet()
    {
        return dbSet;
    }

    public Repository(ApplicationDbContext dataContext)
    {
        this.dataContext = dataContext ?? throw new ArgumentNullException();
        dbSet = dataContext.Set<T>();
    }

    public virtual T? GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        if (predicate is null)
            return preparedDbSet.FirstOrDefault();

        return preparedDbSet.FirstOrDefault(predicate);
    }

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        if (predicate is null)
            return await preparedDbSet.FirstOrDefaultAsync();

        return await preparedDbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        if (predicate is null)
            return await preparedDbSet.FirstAsync();

        return await preparedDbSet.FirstAsync(predicate);
    }

    public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool ignoreDbSet = false)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        IQueryable<T> preparedDbSet = dataContext.Set<T>();
        if (!ignoreDbSet)
            preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        return preparedDbSet.Where(predicate);
    }

    public virtual IPagedList<T> GetWhere(Expression<Func<T, bool>>? predicate,
        PaginationFilter paginationFilter, SortFilter sortFilter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        if (!string.IsNullOrEmpty(sortFilter.SortBy))
        {
            if (sortFilter.PropertyInfo is not null)
            {
                if (sortFilter.Descending)
                    return preparedDbSet.Where(predicate)
                        .OrderBy(sortFilter.SortBy + " descending")
                        .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
                else
                    return preparedDbSet.Where(predicate)
                        .OrderBy(sortFilter.SortBy)
                        .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
            }
            else
            {
                return new List<T>().ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
            }
        }

        return preparedDbSet.Where(predicate)
            .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
    }

    public virtual IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        return preparedDbSet;
    }

    public virtual async Task<IPagedList<T>> GetAll(PaginationFilter filter, SortFilter sortFilter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var preparedDbSet = PrepareDbSet();

        if (include != null)
        {
            preparedDbSet = include(preparedDbSet);
        }

        if (sortFilter.Descending)
            return preparedDbSet.OrderByDescending(item => EF.Property<object>(item, sortFilter.SortBy))
                .ToPagedList(filter.PageSize, filter.PageNumber);

        return await preparedDbSet.OrderBy(item => EF.Property<object>(item, sortFilter.SortBy))
                .ToPagedListAsync(filter.PageNumber, filter.PageSize);
    }

    public async Task Post(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await dbSet.AddAsync(entity);
    }

    public virtual void Put(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        dbSet.Update(entity);
    }

    public virtual void UpdateMany(Expression<Func<T, bool>> predicate,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        var preparedDbSet = PrepareDbSet();

        preparedDbSet.Where(predicate).ExecuteUpdate(setPropertyCalls);
    }

    public virtual void Delete(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        var entry = dataContext.Entry(entity);
        entry.State = EntityState.Deleted;
        dbSet.Remove(entity);
    }

    public virtual void DeleteAll(IEnumerable<T> entities)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
        {
            var entry = dataContext.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            dbSet.Remove(entity);
        }
    }

    public virtual void Patch(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        dbSet.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await dataContext.SaveChangesAsync();
    }
}
