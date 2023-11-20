using LibrarySystem.Domain.Entities.Base;
using System.Linq.Expressions;

namespace LibrarySystem.Domain.Interfaces.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<bool> ExistsByNameAsync(string name);
    }
}
