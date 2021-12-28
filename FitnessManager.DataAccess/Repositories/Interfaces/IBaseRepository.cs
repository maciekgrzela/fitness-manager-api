using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Entities.Interfaces;

namespace FitnessManager.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task Delete(Guid id);
        Task Delete(TEntity entity);
        Task<bool> Exists(Guid id);
        void Update(TEntity entity);
        TEntity Clone(TEntity oldEntity);
    }
}