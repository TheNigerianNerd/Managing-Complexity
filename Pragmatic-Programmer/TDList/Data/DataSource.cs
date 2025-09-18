using System.Text.Json;
using TDList.Contracts;
using TDList.Classes;
using System.Reflection.Metadata.Ecma335;

namespace TDList.Data;

public class DataSource : IDataSource
{
    public async Task CreateAsync()
    {
        var connectionString = ConnectionProvider.Instance.GetConnectionString();
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);
        if (!File.Exists(filePath))
        {
            using (var stream = File.Create(filePath))
            {
                await JsonSerializer.SerializeAsync(stream, new List<ToDo>());
            }
        }
    }

    public async Task InsertDataAsync()
    {
        await Task.CompletedTask;
    }

    public async Task<List<ToDo>> ReadAsync()
    {
        return await Task.FromResult<List<ToDo>>(new List<ToDo>());
    }

    public async Task<bool> UpdateAsync(Guid id, bool isComplete)
    {
        return await Task.FromResult<bool>(false);
    }

    public async Task<bool> AddAsync(IToDoBuilder builder)
    {
        return await Task.FromResult<bool>(false);
    }
}