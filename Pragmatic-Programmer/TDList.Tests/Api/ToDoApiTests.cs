using Moq;
using TDList.Classes;
using TDList.Contracts;
using TDList.Data;
using Microsoft.AspNetCore.Mvc;

namespace TDList.Tests;

public class ToDoControllerTests
{
    [Fact]
    public void GetTest()
    {
        var mockDataSource = new Mock<DataSource>();
        var controller = new ToDoController(mockDataSource.Object);
        var result = controller.Get();
        Assert.NotNull(result);
    }
/*************  ✨ Windsurf Command ⭐  *************/
/// <summary>
/// Test that the ToDoController Post method returns a CreatedResult
/// when given a valid ToDo object.
/// </summary>
/*******  87ad2678-1839-4f43-bbe9-c4c09e06a70c  *******/
    [Fact]
    public async Task PostTest()
    {
        var mockDataSource = new Mock<DataSource>();
        var controller = new ToDoController(mockDataSource.Object);
        var toDo = new ToDoBuilder()
            .WithId(Guid.NewGuid())
            .WithTitle("Test")
            .WithDescription("Test")
            .WithDateLogged(DateTime.Now)
            .WithIsComplete(false)
            .Build();

        var result = await controller.Post((ToDo)toDo);
        
        Assert.NotNull(result);
        Assert.IsType<CreatedResult>(result.Result);
    }
}
