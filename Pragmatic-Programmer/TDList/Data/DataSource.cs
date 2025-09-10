using System.Text.Json;
using TDList.Contracts;
using TDList.Classes;

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
            // Create a ToDo object
            var todo = new ToDo();

            // Serialize the ToDo object to JSON
            var json = JsonSerializer.Serialize(todo, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to a file
            File.WriteAllText(FileName, json);

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
    public bool Read()
    {
        if (!File.Exists(FileName)) return false;

        if (string.IsNullOrEmpty(File.ReadAllText(FileName))) return false;

        var json = File.ReadAllText(FileName);    
        var data = JsonSerializer.Deserialize<ToDo>(json);

        if (data == null) return false;

        return true;
    }

}