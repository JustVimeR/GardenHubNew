using Models;
using Models.DbEntities;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repos.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate = null);
        IPagedList<T> GetWhere(Expression<Func<T, bool>> predicate, PaginationFilter paginationFilter, SortFilter sortFilter);
        IQueryable<T> GetAll();
        IPagedList<T> GetAll(PaginationFilter paginationFilter, SortFilter sortFilter);


        void Post(T[] entities);
        void Put(T entity);

        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entities);

        void Patch(T entity);

        Task SaveChangesAsync();
    }
}
