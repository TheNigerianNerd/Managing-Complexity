using TDList.Data;
using TDList.Contracts;

namespace TDList.Tests.Api;
public class ToDoApiTests 
{
    private readonly IDataSource _dataSource;
    private string _expectedFileName;
    public ToDoApiTests()
    {
        _dataSource = new DataSource();
        _expectedFileName = DataSource.FileName;
    }
}