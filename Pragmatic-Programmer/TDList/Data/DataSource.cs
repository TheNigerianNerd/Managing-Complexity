using TDList.Contracts;
using TDList.Classes;
namespace TDList.Data;
public class DataSource : IDataSource
{
    public bool Exists(string connection) => true;
    public bool IsValid(string connection) => true;
    public IEnumerable<ToDo> Get(string connection) => new List<ToDo>();
}