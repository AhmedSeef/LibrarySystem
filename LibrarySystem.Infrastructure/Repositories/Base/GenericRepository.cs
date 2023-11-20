using LibrarySystem.Domain.Entities.Base;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibrarySystem.Infrastructure.Repositories.Base
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted)
        {
            if (includeDeleted)
                return await _context.Set<T>().ToListAsync();
            else
                return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.Now;
            }

            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            entity.EditedAt = DateTime.Now;
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true; // Soft delete

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Set<T>().AnyAsync(x => x.Name == name);
        }
    }
}
