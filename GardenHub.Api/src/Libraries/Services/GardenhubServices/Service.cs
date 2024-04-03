using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DbEntities;
using Models.Enums;
using Services.GardenhubServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Services.GardenhubServices;

public class Service<T> : IService<T> where T : IEntityBase
{
    protected readonly IRepository<T> _repository;

    public Service(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
    }

    public virtual async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return await _repository.GetWhere(predicate).ToListAsync();
    }

    public virtual async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var pagedList = await _repository.GetWhere(predicate, paginationFilter, sortFilter);

        return pagedList.ToList();
    }

    public virtual Task<List<T>> GetAllAsync()
    {
        return _repository.GetAll().ToListAsync();
    }

    public virtual async Task<List<T>> GetAllAsync(PaginationFilter paginationFilter, SortFilter sortFilter)
    {
        IPagedList<T> entities = await _repository.GetAll(paginationFilter, sortFilter);
        return entities.ToList();
    }

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return await _repository.GetFirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return await _repository.GetFirstAsync(predicate);
    }

    public virtual async Task<T> PostAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        await _repository.Post(entity);

        await _repository.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> PutAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _repository.Put(entity);
        await _repository.SaveChangesAsync();

        return entity;
    }

    public virtual async Task DeleteAsync(T entity, bool softDelete = false)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        if (softDelete)
        {
            entity.RecordStatus = RecordStatus.Deleted;
        }
        else
        {
            _repository.Delete(entity);
        }

        await _repository.SaveChangesAsync();
    }

    public virtual async Task DeleteAllAsync(IList<T> entities, bool softDelete = false)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));


        if (entities.Count == 0) return;

        if (softDelete)
        {
            foreach (var entity in entities)
            {
                entity.RecordStatus = RecordStatus.Deleted;
                _repository.Put(entity);
            }
        }
        else
        {
            _repository.DeleteAll(entities);
        }

        await _repository.SaveChangesAsync();
    }

    public virtual async Task<T> PatchAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _repository.Patch(entity);
        await _repository.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateManyAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        await _repository.UpdateMany(predicate, setPropertyCalls);
    }
}

