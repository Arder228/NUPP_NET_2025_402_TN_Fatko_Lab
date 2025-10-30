using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSystem.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ComputerSystemContext _ctx;
        private readonly DbSet<T> _dbSet;

        public EfRepository(ComputerSystemContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var found = await _dbSet.FindAsync(new object[] { id });
            return found;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
