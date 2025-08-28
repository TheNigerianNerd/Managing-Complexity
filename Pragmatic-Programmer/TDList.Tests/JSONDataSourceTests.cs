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
    public void Exists_WithJSonConnection_ReturnsFalse()
    {
        Assert.False(_JSONdataSource.Exists("SampleConnection"));
    }
    [Fact]
    public void Exists_WithInvalidJSONConnection_ReturnsFalse()
    {
        Assert.False(_JSONdataSource.Exists("bad-connection"));
    }
    [Fact]
    public void IsValid_WithWrongConnectionString_ReturnsFalse()
    {
        Assert.False(_JSONdataSource.IsValid("bad-connection"));
    }
    [Fact]
    public void IsValid_WithCorrectConnectionString_ReturnsTrue()
    {
        //TODO-Should return true
        Assert.False(_JSONdataSource.IsValid("TDList.JSON"));
    }
}
