using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities.Interfaces;
using FitnessManager.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected DataContext Context { get; }

        public BaseRepository(DataContext context)
        {
            Context = context;
        }
        
        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            return (await Context.Set<TEntity>().AddAsync(entity)).Entity;
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            await Delete(entity);
        }

        public virtual Task Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);

            return Task.CompletedTask;
        }

        public virtual Task<bool> Exists(Guid id)
        {
            return Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
        }

        public virtual void Update(TEntity entity)
        { 
            Context.Set<TEntity>().Update(entity);
        }

        public virtual TEntity Clone(TEntity oldEntity)
        {
            var newEntity = (TEntity)Context.Entry(oldEntity).CurrentValues.Clone().ToObject();
            newEntity.Id = new Guid();
            return newEntity;
        }
    }
}