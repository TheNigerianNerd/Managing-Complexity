using System.Diagnostics;
using System.IO.Enumeration;
using System.Text.Json;
using TDList.Classes;
using TDList.Contracts;
using TDList.Data;
using TDList.Models;
using Xunit;

namespace TDList.Tests;

public class JSONDataSourceTests
{
    private readonly DataSource _JSONDataSource;
    private string _expectedFileName;

    public JSONDataSourceTests()
    {
        _JSONDataSource = new DataSource();
        _expectedFileName = ConnectionProvider.Instance.GetConnectionString();
    }

    [Fact]
    public async Task CreateAsync()
    {
        await _JSONDataSource.CreateAsync();
        Assert.True(File.Exists(_expectedFileName));
    }
}

