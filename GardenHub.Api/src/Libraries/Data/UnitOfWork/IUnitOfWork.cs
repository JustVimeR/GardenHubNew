using Data.Repos.Interfaces;
using Models.DbEntities;
using System;
using System.Threading.Tasks;

namespace Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
    Task<int> Complete();
}
