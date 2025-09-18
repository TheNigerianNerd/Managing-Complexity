using Moq;
using TDList.Classes;
using TDList.Contracts;
using TDList.ToDoApi;

namespace TDList.Tests;
public class ToDoControllerTests
{
    [Fact]
    public async Task ReadAsync_ReturnsListOfToDoObjects_WhenCalledWithValidParameters()
    {
        // Arrange
        var dataSourceMock = new Mock<IDataSource>();
        dataSourceMock
        .Setup(ds => ds.ReadAsync())
        .Returns(Task.FromResult(new List<ToDo>()));


        var toDoController = new ToDoController(new TDList.Data.DataSource());

        // Act
        var result = await toDoController.Get();

        // Assert
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
    }

}
