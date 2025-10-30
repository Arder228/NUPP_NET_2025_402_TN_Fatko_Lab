using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerSystem.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(object id);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
