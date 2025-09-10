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
    private readonly string _expectedFileName;
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

    [Fact]
    public void Create_FileAlreadyExists_ReturnsTrue()
    {
        //Arrange
        File.Create(_expectedFileName).Dispose();

        //Act
        var result = _JSONdataSource.Create();

        //Assert
        Assert.True(result);
        Assert.True(File.Exists(_expectedFileName));
    }
    [Fact]
    public void Create_IoException_ReturnsFalse()
    {
        // Test that Create method returns false when an I/O exception occurs (e.g. file is already open and locked)
        var fileStream = File.Open(_expectedFileName, FileMode.Open, FileAccess.Write, FileShare.None);

        // Act
        var result = _JSONdataSource.Create();

        // Assert
        Assert.False(result);
        Assert.True(File.Exists(_expectedFileName));
        fileStream.Dispose();
    }
    [Fact]
    public void Create_UnauthorizedAccessException_ReturnsFalse()
    {
        // Arrange
        var fileStream = File.Open(_expectedFileName, FileMode.Open, FileAccess.Write, FileShare.None);
        File.SetAttributes(_expectedFileName, FileAttributes.ReadOnly);

        // Act
        var result = _JSONdataSource.Create();

        // Assert
        Assert.False(result);
        Assert.True(File.Exists(_expectedFileName));
        File.SetAttributes(_expectedFileName, FileAttributes.Normal);
        fileStream.Dispose();
    }
    #endregion
    #region 'Read' Tests - Tests to assert the datasource reads the prescribed JSON file
    [Fact]
    public void Read_FileExists()
    {
        //Arrange
        string fileName = _expectedFileName;

        //Act
        var result = _JSONdataSource.Read();

        //Assert
        Assert.True(result);
    }
    [Fact]
    public void Read_FileHasText()
    {
        //Arrange
        string fileName = _expectedFileName;

        //Act
        var file = File.ReadAllText(_expectedFileName);

        //Assert
        Assert.NotEmpty(file);
    }
    [Fact]
    public void Read_FileIsValidJSON()
    {
        //Arrange
        var file = File.OpenText(_expectedFileName);

        //Act
        var result = JsonSerializer.Deserialize<ToDo>(file.ReadToEnd());

        //Assert
        Assert.NotNull(result);    
    }
    #endregion
}
