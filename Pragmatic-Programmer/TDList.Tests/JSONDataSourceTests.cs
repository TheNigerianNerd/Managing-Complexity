using System.IO.Enumeration;
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
        //Arrange
        var fileStream = File.Open(_expectedFileName, FileMode.Open, FileAccess.Write, FileShare.None);

        //Act
        var result = _JSONdataSource.Create();

        //Assert
        Assert.False(result);
        Assert.True(File.Exists(_expectedFileName));
        fileStream.Dispose();
    }
    [Fact]
        public void Create_UnauthorizedAccessException_ReturnsFalse()
        {
            // Arrange
            var dataSource = new DataSource();
            var expectedFileName = DataSource.FileName;
            var fileStream = File.Open(expectedFileName, FileMode.Open, FileAccess.Write, FileShare.None);
            File.SetAttributes(expectedFileName, FileAttributes.ReadOnly);

            // Act
            var result = dataSource.Create();

            // Assert
            Assert.False(result);
            Assert.True(File.Exists(expectedFileName));
            File.SetAttributes(expectedFileName, FileAttributes.Normal);
            fileStream.Dispose();
        }
}
