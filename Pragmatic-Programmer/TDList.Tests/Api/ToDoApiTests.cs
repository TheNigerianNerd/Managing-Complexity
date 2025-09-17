using TDList.Data;
using TDList.Contracts;
using TDList.Classes;

namespace TDList.Tests.Api;

public class ToDoApiTests
{
    private readonly IDataSource _dataSource;
    private string _expectedFileName;
    private List<ToDo> _expectedTodos;

    public ToDoApiTests()
    {
        _dataSource = new DataSource();
        _expectedFileName = DataSource.FileName;
        _expectedTodos = _dataSource.Read();
    }
    // [Fact]
    // public async Task GetWeatherForecast_ReturnsOkResult()
    // {
    //     // Arrange
    //     var client = new HttpClient();
    //     var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7101/weatherforecast");

    //     // Act
    //     var response = await client.SendAsync(request);

    //     // Assert
    //     Assert.True(response.IsSuccessStatusCode);
    // }
}