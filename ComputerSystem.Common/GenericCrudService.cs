using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace ComputerSystem.Common
{
	public class GenericCrudService<T> : ICrudService<T> where T : class
	{
		private static readonly ConcurrentDictionary<Guid, T> storage = new ConcurrentDictionary<Guid, T>();


		private Guid GetId(T element)
		{
			var prop = typeof(T).GetProperty("Id");
			if (prop == null) throw new InvalidOperationException("T must have Id property of type Guid");
			return (Guid)prop.GetValue(element);
		}


		public void Create(T element)
		{
			var id = GetId(element);
			storage.TryAdd(id, element);
		}


		public T Read(Guid id)
		{
			if (storage.TryGetValue(id, out var value)) return value;
			return null;
		}


		public IEnumerable<T> ReadAll()
		{
			return storage.Values.ToList();
		}


		public void Update(T element)
		{
			var id = GetId(element);
			storage.AddOrUpdate(id, element, (k, v) => element);
		}


		public void Remove(T element)
		{
			var id = GetId(element);
			storage.TryRemove(id, out _);
		}


		public void Save(string filePath)
		{
			Serializer.SaveToFile(storage.Values, filePath);
		}


		public void Load(string filePath)
		{
			var items = Serializer.LoadFromFile<List<T>>(filePath);
			if (items == null) return;


			storage.Clear();
			foreach (var item in items)
			{
				var id = GetId(item);
				storage.TryAdd(id, item);
			}
		}
	}
}