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
    private readonly IDataSource _JSONdataSource;
    private string _expectedFileName;
    public JSONDataSourceTests()
    {
        _JSONdataSource = new DataSource();
        _expectedFileName = DataSource.FileName;
    }
    #region 'Create' Tests - Tests to assert the datasource creates a JSON file
    [Fact]
    public void Create_FileCreated_ReturnsTrue()
    {
        // Act
        var result = _JSONdataSource.Create();

        // Assert
        Assert.True(result);
        Assert.True(File.Exists(_expectedFileName));
    }
    #endregion
    #region 'Insert' Tests - Tests to assert the datasource inserts data into the prescribed JSON file
    [Fact]
    public void InsertData()
    {
        //Act
        _JSONdataSource.InsertData();
        Assert.NotEmpty(File.ReadAllText(_expectedFileName));
    }
    #endregion
    #region 'Read' Tests - Tests to assert the datasource reads the prescribed JSON file
    [Fact]
    public void Read_FileExists_ReturnsListOfToDoObjects()
    {
        //Act
        var result = _JSONdataSource.Read();

        //Assert
        Assert.True(result is List<ToDo>);
    }
    #endregion
    #region 'Update' Tests - Tests to assert the datasource updates the prescribed JSON file
    [Fact]
    public void Update_UpdatesToDoItem_ReturnsTrue()
    {
        //Arrange
        var id = new Guid("d5e375a6-ff6b-4b76-8a5d-5c0a6d3c0e5c");
        //Act
        _JSONdataSource.InsertData();
        var result = _JSONdataSource.Update(id, true);

        //Assert
        var todos = JsonSerializer.Deserialize<List<ToDo>>(File.ReadAllText(_expectedFileName));
        var todo = todos?.First(t => t.Id == id);
        if(todo != null) Assert.True(todo.IsComplete);
    }
    #endregion
}
