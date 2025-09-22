using System.Text.Json;
using TDList.Contracts;
using TDList.Classes;
using System.Reflection.Metadata.Ecma335;

namespace TDList.Data;

public class DataSource : IDataSource
{
    string connectionString;
    public DataSource()
    {
        connectionString = ConnectionProvider.Instance.GetConnectionString();
    }
    public async Task CreateAsync()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);
        if (!File.Exists(filePath))
        {
            using (var stream = File.Create(filePath))
            {
                await JsonSerializer.SerializeAsync(stream, new List<ToDo>());
            }
        }
    }
    
    public async Task InsertDataAsync(ToDo toDo)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);

        var deserializedList = await ReadAsync();
        deserializedList.Add(toDo);
        using (var stream = File.Open(filePath, FileMode.Truncate, FileAccess.Write))
        {
            await JsonSerializer.SerializeAsync(stream, deserializedList);
        }
    }

    public async Task<List<ToDo>> ReadAsync()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);
        if (File.Exists(filePath))
        {
            try
            {
                await using var stream = File.OpenRead(filePath);
                var deserializedList = await JsonSerializer.DeserializeAsync<List<ToDo>>(stream);
                return deserializedList ?? new List<ToDo>();
            }
            catch (JsonException)
            {
                // Optionally log the error
                return new List<ToDo>();
            }
        }
        else
        {
            return new List<ToDo>();
        }
    }
    public async Task UpdateDataAsync(ToDo toDo)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);

        var deserializedList = await ReadAsync();
        var index = deserializedList.FindIndex(t => t.Id == toDo.Id);
        if (index != -1)
        {
            deserializedList[index] = toDo;
            using (var stream = File.Open(filePath, FileMode.Truncate, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, deserializedList);
            }
        }
    }
}