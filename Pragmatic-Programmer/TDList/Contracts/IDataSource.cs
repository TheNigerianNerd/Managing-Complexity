using TDList.Classes;
using System.Threading.Tasks;

namespace TDList.Contracts;

public interface IDataSource
{ 
    Task CreateAsync();
    Task InsertDataAsync(ToDo toDo);
    Task<List<ToDo>> ReadAsync();
    //Task<bool> UpdateAsync(Guid id, bool isComplete);
    // Task<bool> AddAsync(IToDoBuilder builder);
}
