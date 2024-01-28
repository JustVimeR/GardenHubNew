using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;
using Models.DbEntities;
using Newtonsoft.Json.Linq;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Repos.Concrete
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext dataContext;
        protected readonly DbSet<T> dbSet;

        protected virtual IQueryable<T> PrepareDbSet()
        {
            IEnumerable<Type> classes = typeof(T).Assembly.GetTypes().Where(x => x.FullName.StartsWith("Models.DbEntities.")).ToList();

            var props = typeof(T).GetProperties().Select(x => x).ToList();

            foreach (var prop in props)
            {
                if (classes.Contains(prop.PropertyType))
                {
                    dbSet.Include(prop.Name).Load();
                    var props2 = prop.PropertyType.GetProperties().Select(x => x).ToList();
                    foreach (var prop2 in props2)
                    {
                        if (classes.Contains(prop2.PropertyType))
                        {
                            dbSet.Include(prop.Name + '.' + prop2.Name).Load();
                        }

                        var genericArgs2 = prop2.PropertyType.GetGenericArguments();
                        if (genericArgs2.Length > 0 && classes.Contains(genericArgs2[0]))
                        {
                            dbSet.Include(prop.Name + '.' + prop2.Name).Load();
                        }

                    }
                }

                var genericArgs = prop.PropertyType.GetGenericArguments();
                if (genericArgs.Length > 0 && classes.Contains(genericArgs[0]))
                {
                    dbSet.Include(prop.Name).Load();
                }

            }
            var a = dbSet.ToList();
            return dbSet;
        }

        public Repository(ApplicationDbContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException();
            dbSet = dataContext.Set<T>();
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            var preparedDbSet = PrepareDbSet();

            if (predicate is null)
                return preparedDbSet.FirstOrDefault();

            return preparedDbSet.FirstOrDefault(predicate);
        }

        public virtual Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            var preparedDbSet = PrepareDbSet();

            if (predicate is null)
                return preparedDbSet.FirstOrDefaultAsync();

            return preparedDbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return PrepareDbSet().Where(predicate);
        }

        public virtual IPagedList<T> GetWhere(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));


            if (!string.IsNullOrEmpty(sortFilter.SortBy))
            {

                if (sortFilter.PropertyInfo is not null)
                {
                    if (sortFilter.Descending)
                        return PrepareDbSet()
                            .Where(predicate)
                            .OrderBy(sortFilter.SortBy + " descending")
                            .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
                    else
                        return PrepareDbSet()
                            .Where(predicate)
                            .OrderBy(sortFilter.SortBy)
                            .ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
                }
                else
                {
                    return new List<T>().ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);
                }

            }

            return PrepareDbSet().Where(predicate).ToPagedList(paginationFilter.PageNumber, paginationFilter.PageSize);

        }

        public virtual IQueryable<T> GetAll()
        {
            return PrepareDbSet();
        }

        public virtual IPagedList<T> GetAll(PaginationFilter filter, SortFilter sortFilter)
        {
            if (sortFilter.Descending)
                return PrepareDbSet()
                    .OrderByDescending(item => EF.Property<object>(item, sortFilter.SortBy))
                    .ToPagedList(filter.PageSize, filter.PageNumber);
            return PrepareDbSet()
                    .OrderBy(item => EF.Property<object>(item, sortFilter.SortBy))
                    .ToPagedList(filter.PageNumber, filter.PageSize);
        }

        public virtual void Post(T[] entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            //IEnumerable<Type> classes = typeof(T).Assembly.GetTypes().Where(x => x.FullName.StartsWith("Models.DbEntities.")).ToList();

            //var props = typeof(T).GetProperties();
            //var navProperties = props.Where(x => classes.Contains(x.PropertyType)).ToList();

            //var navListProperties = props.Where(x =>
            //    {
            //        bool predicate = false;
            //        var genericArgs = x.PropertyType.GetGenericArguments();
            //        if(genericArgs.Length>0)
            //        {
            //             predicate =classes.Contains(genericArgs[0]);
            //        }
            //        return predicate;
            //        });

            foreach (var entity in entities)
            {
                //foreach(var navProp in navProperties)
                //{
                //    var setMethod = typeof(DbContext).GetMethod("Set");
                //    var genericSetMethod = setMethod.MakeGenericMethod(navProp.PropertyType);
                //    var dbSet = (IList<IEntityBase>)genericSetMethod.Invoke(dataContext, null);

                //    var dbEntity = dbSet.FirstOrDefault(x => x.Id == entity.Id);
                //}
                //foreach(var navListProp in navListProperties)
                //{

                //}



                dbSet.Add(entity);
            }

        }

        public virtual void Put(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Update(entity);
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
                var entry = dataContext.Entry(entity);
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

        public Task SaveChangesAsync()
        {
            return dataContext.SaveChangesAsync();
        }
    }
}
