using System.Diagnostics;
using System.IO.Enumeration;
using System.Text.Json;
using Microsoft.VisualBasic;
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

    [Fact]
    public async Task ReadAsync_ReturnsDeserializedList_WhenFileExists()
    {
        var toDos = new List<IToDo>
        {
            new ToDoBuilder()
                .WithId(Guid.NewGuid())
                .WithTitle("Test")
                .WithDescription("Test")
                .WithDateLogged(DateTime.Now)
                .WithIsComplete(false)
                .Build(),
            new ToDoBuilder()
                .WithId(Guid.NewGuid())
                .WithTitle("Test2")
                .WithDescription("Test2")
                .WithDateLogged(DateTime.Now)
                .WithIsComplete(false)
                .Build()
        };

        var filePath = Path.Combine(Environment.CurrentDirectory, _expectedFileName);
        using (var stream = File.Create(filePath))
        {
            await JsonSerializer.SerializeAsync(
                stream,
                toDos,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }

        var result = await _JSONDataSource.ReadAsync();
        Assert.Equal(2, result.Count);
        Assert.Equal(toDos[0].Title, result[0].Title);
        Assert.Equal(toDos[1].Title, result[1].Title);
    }
    [Fact]
    public async Task InsertDataAsync_WhenFileExists_AddsToDoToList_ThenWritesListToFile()
    {
        var toDo = new ToDoBuilder()
            .WithId(Guid.NewGuid())
            .WithTitle("Test")
            .WithDescription("Test")
            .WithDateLogged(DateTime.Now)
            .WithIsComplete(false)
            .Build();

        var filePath = Path.Combine(Environment.CurrentDirectory, _expectedFileName);
        await _JSONDataSource.CreateAsync();
        Assert.True(File.Exists(filePath));

        await _JSONDataSource.InsertDataAsync((ToDo)toDo);
        var result = await _JSONDataSource.ReadAsync();
        Assert.Equal(toDo.Id, result.Last().Id);
    }
}

