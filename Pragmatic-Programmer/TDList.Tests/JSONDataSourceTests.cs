using TDList.Contracts;
using TDList.Data;
using TDList.Models;
using Xunit;

namespace TDList.Tests;

public class JSONDataSourceTests
{
    private readonly IDataSource _JSONdataSource;

    public JSONDataSourceTests()
    {
        _JSONdataSource = new DataSource();
    }

    [Fact]
    public void Create_JSONFIleExists_ReturnFalse()
    {
        Assert.False(_JSONdataSource.Create());
    }
}
