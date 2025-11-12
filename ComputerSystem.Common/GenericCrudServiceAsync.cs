using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerSystem.Common
{
    public class GenericCrudServiceAsync<T> : ICrudServiceAsync<T>, IEnumerable<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _storage = new();
        private readonly object _syncObj = new();

        public string FilePath { get; }

        public GenericCrudServiceAsync(string filePath)
        {
            FilePath = filePath;
        }

        private Guid GetId(T element)
        {
            var prop = typeof(T).GetProperty("Id");
            if (prop == null)
                throw new InvalidOperationException("T must have an Id property of type Guid");

            var value = prop.GetValue(element) ?? throw new InvalidOperationException("Id property is null");
            if (value is Guid id)
                return id;

            throw new InvalidOperationException("Id property must be of type Guid");
        }

        public Task<bool> CreateAsync(T element)
        {
            var id = GetId(element);
            return Task.FromResult(_storage.TryAdd(id, element));
        }

        public Task<T?> ReadAsync(object id)
        {
            if (id is Guid guid)
            {
                _storage.TryGetValue(guid, out var value);
                return Task.FromResult(value);
            }
            return Task.FromResult<T?>(null);
        }

        public Task<IEnumerable<T>> ReadAllAsync()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            if (page < 1) page = 1;
            if (amount < 1) amount = 10;

            var skip = (page - 1) * amount;
            var result = _storage.Values.Skip(skip).Take(amount).ToList().AsEnumerable();

            return Task.FromResult(result);
        }

        public Task<bool> UpdateAsync(T element)
        {
            var id = GetId(element);
            _storage.AddOrUpdate(id, element, (_, _) => element);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveAsync(T element)
        {
            var id = GetId(element);
            var removed = _storage.TryRemove(id, out _);
            return Task.FromResult(removed);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var snapshot = _storage.Values.ToList();
                await Serializer.SaveToFileAsync(snapshot, FilePath).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LoadAsync()
        {
            try
            {
                var items = await Serializer.LoadFromFileAsync<List<T>>(FilePath).ConfigureAwait(false);
                if (items is null)
                    return false;

                _storage.Clear();
                foreach (var item in items)
                {
                    var id = GetId(item);
                    _storage.TryAdd(id, item);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator() => _storage.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}