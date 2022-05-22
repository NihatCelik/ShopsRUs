using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
            where TEntity : class, IEntity
            where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfEntityRepositoryBase(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = Context.Add(entity).Entity;
            await Context.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public TEntity AddNoSave(TEntity entity)
        {
            var result = Context.Add(entity).Entity;
            return result;
        }

        public TEntity UpdateNoSave(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }

        public void DeleteNoSave(TEntity entity)
        {
            Context.Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? await Context.Set<TEntity>().AsNoTracking().ToListAsync() :
                 await Context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Context.Set<TEntity>().AsNoTracking() : Context.Set<TEntity>().AsNoTracking().Where(expression);
        }

        public Task<int> Execute(FormattableString interpolatedQueryString)
        {
            return Context.Database.ExecuteSqlInterpolatedAsync(interpolatedQueryString);
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return await Context.Set<TEntity>().AsNoTracking().CountAsync();
            else
                return await Context.Set<TEntity>().AsNoTracking().CountAsync(expression);
        }
    }
}
