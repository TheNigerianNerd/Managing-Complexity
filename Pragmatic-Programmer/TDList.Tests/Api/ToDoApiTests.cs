using Moq;
using TDList.Classes;
using TDList.Contracts;
using TDList.Data;

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
}
