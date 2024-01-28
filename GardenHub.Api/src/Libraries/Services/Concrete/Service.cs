using Data.Repos.Interfaces;
using Data.UnitOfWork;
using Models;
using Models.DbEntities;
using Models.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class Service<T> : IService<T> where T : EntityBase
    {
        protected readonly IRepository<T> repository;

        public Service(IRepository<T> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }

        public virtual T? GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null)
        {
            return repository.GetFirstOrDefault(predicate);
        }

        public virtual List<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return repository.GetWhere(predicate).ToList();
        }

        public virtual List<T> GetWhere(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            return repository.GetWhere(predicate, paginationFilter, sortFilter).ToList();
        }

        public virtual List<T> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public virtual List<T> GetAll(PaginationFilter paginationFilter, SortFilter sortFilter)
        {
            return repository.GetAll(paginationFilter, sortFilter).ToList();
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await repository.GetFirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T[]> PostAsync(T[] entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            repository.Post(entities);
            await repository.SaveChangesAsync();

            return entities;
        }

        public virtual async Task<T> PutAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            repository.Put(entity);
            await repository.SaveChangesAsync();

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
                repository.Delete(entity);
            }

            await repository.SaveChangesAsync();
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
                    repository.Put(entity);
                }
            }
            else
            {
                repository.DeleteAll(entities);
            }

            await repository.SaveChangesAsync();
        }

        public virtual async Task<T> PatchAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            repository.Patch(entity);
            await repository.SaveChangesAsync();

            return entity;
        }
    }
}
