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
    private const string FileName = "TDList.json";
    public bool Exists(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))return false;
        try
        {
            return File.Exists(fileName);
        }
        catch
        {
            // Handles cases like invalid path format
            return false;
        }
    }
    public bool IsValid(string fileName)
    {
        return Exists(fileName);
    }
    public IEnumerable<ToDo> Get(string fileName)
    {
        return new List<ToDo>();
    }
}