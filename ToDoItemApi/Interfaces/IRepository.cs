using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoItemApi.Models;

namespace ToDoItemApi.Interfaces
{
    public interface IRepository
    {
        Task<ToDoItem> GetAsync(long id);
        Task<List<ToDoItem>> GetAllAsync();
        Task UpsertAsync(ToDoItem model);
        Task DeleteAsync(long id);
    }
}