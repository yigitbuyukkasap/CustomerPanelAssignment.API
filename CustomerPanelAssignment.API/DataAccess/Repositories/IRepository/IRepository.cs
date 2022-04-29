using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Find(Guid id);

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
            );

        Task<T> FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true
            );

        Task<T> Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

        void Save();
    }
}
