using TDList.Data;
using TDList.Contracts;
using TDList.Classes;

namespace TDList.Tests.Api;

public class ToDoApiTests
{
    private readonly IDataSource _dataSource;
    private string _expectedFileName;
    private List<ToDo> _expectedTodos;

    public ToDoApiTests()
    {
        _dataSource = new DataSource();
        _expectedFileName = DataSource.FileName;
        _expectedTodos = _dataSource.Read();
    }
}