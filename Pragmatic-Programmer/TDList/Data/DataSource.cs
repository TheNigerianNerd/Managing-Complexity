using System.Text.Json;
using TDList.Contracts;
using TDList.Classes;
using System.Reflection.Metadata.Ecma335;

namespace TDList.Data;

public class DataSource : IDataSource
{
    /*<summary>
    Data source class for a JSON file
    <summary>
    */
    private static readonly ConnectionProvider _connectionProvider = ConnectionProvider.Instance;
    public static string FileName = _connectionProvider.GetConnectionString();
    public DataSource() { }
    public bool Create()
    {
        try
        {
            if (!File.Exists(FileName)) return false;

            File.Create(FileName).Dispose();

            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access to the file was denied: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return false;
        }
    }
    public void InsertData()
    {
        if (File.Exists(FileName) && new FileInfo(FileName).Length == 0)
        {
            var todo1 = (ToDo)new ToDoBuilder()
                    .WithId(new Guid("d5e375a6-ff6b-4b76-8a5d-5c0a6d3c0e5c"))
                    .WithTitle("AGILE Methodology")
                    .WithDescription("Short iterations, short sprints")
                    .WithDateLogged(DateTime.Now)
                    .WithIsComplete(false)
                    .Build();
            var todo2 = (ToDo)new ToDoBuilder()
                        .WithId(Guid.NewGuid())
                        .WithTitle("Code Fluency")
                        .WithDescription("Rapid prototyping")
                        .WithDateLogged(DateTime.Now)
                        .WithIsComplete(false)
                        .Build();

            var todos = new ToDo[] { todo1, todo2 };
            var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }       
    }
    public List<ToDo> Read()
    {
        List<ToDo> todos = new List<ToDo>();

        //if (!File.Exists(FileName) || new FileInfo(FileName).Length == 0) return todos;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<List<ToDo>>(json);

        if (data == null)
        {
            return todos;
        }

        return data;
    }

    /// <summary>
    /// Update a to-do item in the data store.
    /// </summary>
    /// <param name="id">The id of the to-do item to update.</param>
    /// <param name="isComplete">The new value to set the IsComplete property to.</param>
    /// <returns>True if the update was successful, false otherwise.</returns>
    public bool Update(Guid id, bool isComplete)
    {
        if (!File.Exists(FileName)) return false;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<List<ToDo>>(json);

        if (data == null || data.Count == 0) return false;

        var todo = data.FirstOrDefault(t => t.Id == id);
        if (todo == null) return false;

        todo.IsComplete = isComplete;

        json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);

        return true;
    }

    /// <summary>
    /// Add a new to-do item to the data store.
    /// </summary>
    /// <param name="builder">The builder to use to create the new to-do item.</param>
    /// <returns>True if the add was successful, false otherwise.</returns>
    public bool Add(IToDoBuilder builder)
    {
        if (!File.Exists(FileName)) return false;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<List<ToDo>>(json);

        if (data == null) data = new List<ToDo>();

        var todo = (ToDo)builder.Build();
        data.Add(todo);

        json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);

        return true;
    }
}