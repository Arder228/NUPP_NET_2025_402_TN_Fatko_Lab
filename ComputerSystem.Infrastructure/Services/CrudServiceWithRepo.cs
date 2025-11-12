using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerSystem.Common;
using ComputerSystem.Infrastructure.Repositories;

namespace ComputerSystem.Infrastructure.Services
{
    public class CrudServiceWithRepo<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repo;

        public CrudServiceWithRepo(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repo.AddAsync(element);
            return true;
        }

        public async Task<T?> ReadAsync(object id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var all = (await _repo.GetAllAsync()).ToList();
            if (page < 1) page = 1;
            if (amount < 1) amount = 10;
            return all.Skip((page - 1) * amount).Take(amount);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _repo.UpdateAsync(element);
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _repo.DeleteAsync(element);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoadAsync()
        {
            await Task.CompletedTask;
            return true;
        }
    }
}