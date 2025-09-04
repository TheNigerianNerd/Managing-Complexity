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
    public DataSource()
    {

    }
    public bool Create() => false;
}