using TDList.Contracts;
using TDList.Data;
using TDList.Models;
using TDList.Tests.Fakes;
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
    public void Exists_WithValidConnection_ReturnsTrue()
    {
        Assert.False(_JSONdataSource.Exists("TDList.json"));
    }
}
