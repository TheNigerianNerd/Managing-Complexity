using TDList.Contracts;
using TDList.Data;
using TDList.Models;
using Xunit;

namespace TDList.Tests;

public class DataSourceTests : IDataSource
{
    private IDataSource _dataSource;

    public DataSourceTests()
    {
        _dataSource = new DataSource();
    }
    public bool Create()
    {
        return true;
    }
}
