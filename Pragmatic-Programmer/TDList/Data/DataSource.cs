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
    public async Task InsertDataAsync(List<ToDo> toDos)
    {
        var connectionString = ConnectionProvider.Instance.GetConnectionString();
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);
        using (var stream = File.Open(filePath, FileMode.Create))
        {
            var existingToDos = await JsonSerializer.DeserializeAsync<List<ToDo>>(stream);
            if (existingToDos != null)
            {
                existingToDos.AddRange(toDos);
                stream.Position = 0;
                await JsonSerializer.SerializeAsync(stream, existingToDos);
            }
        }
    }

    public async Task<List<ToDo>> ReadAsync()
    {
        var connectionString = ConnectionProvider.Instance.GetConnectionString();
        var filePath = Path.Combine(Environment.CurrentDirectory, connectionString);
        if (File.Exists(filePath))
        {
            using (var stream = File.OpenRead(filePath))
            {
                var deserializedList = await JsonSerializer.DeserializeAsync<List<ToDo>?>(stream);
                return deserializedList ?? new List<ToDo>();
            }
        }
        else
        {
            return new List<ToDo>();
        }
    }

}