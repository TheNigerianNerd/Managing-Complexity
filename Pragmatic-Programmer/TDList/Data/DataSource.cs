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
    public static string FileName = "TDList.json";
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
                        .WithId(Guid.NewGuid())
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

        if (!File.Exists(FileName) || new FileInfo(FileName).Length == 0) return todos;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<List<ToDo>>(json);

        if (data == null)
        {
            return todos;
        }

        return data;
    }
    public bool Update(Guid id, bool isComplete)
    {
        if (!File.Exists(FileName)) return false;

        var json = File.ReadAllText(FileName);
        var data = JsonSerializer.Deserialize<ToDo>(json);

        if (data == null || data.Id != id) return false;

        data.IsComplete = isComplete;

        json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);

        return true;
    }
}