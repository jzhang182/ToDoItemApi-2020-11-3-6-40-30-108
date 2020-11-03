using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoItemApi.Models;
using ToDoItemApi.Interfaces;

namespace ToDoItemApi
{
    public class MemoryRepository: IRepository
    {
        private readonly Dictionary<long, ToDoItem> _dic = new Dictionary<long, ToDoItem>();

        public MemoryRepository()
        {
            Init();
        }

        public async Task<ToDoItem> GetAsync(long id)
        {
            _dic.TryGetValue(id, out var item);
            return item;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return _dic.Values.ToList();
        }

        public async Task UpsertAsync(ToDoItem model)
        {
            _dic[model.Id] = model;
        }

        public async Task DeleteAsync(long id)
        {
            if (_dic.ContainsKey(id))
                _dic.Remove(id);
        }

        private void Init()
        {
            for (var i = 1; i < 4; i++)
            {
                var item = new ToDoItem()
                {
                    Id = i,
                    Name = $"test item {i}",
                    IsComplete = false
                };
                _dic[item.Id] = item;
            }
        }
    }
}
