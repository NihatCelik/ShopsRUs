using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        T AddNoSave(T entity);
        T UpdateNoSave(T entity);
        void DeleteNoSave(T entity);

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression = null);
        Task<int> Execute(FormattableString interpolatedQueryString);
        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null);

    }
}
