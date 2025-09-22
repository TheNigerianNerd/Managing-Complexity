using System.Threading.Tasks;
using Newtonsoft.Json;
using TDList.Classes;
using Xunit;

namespace TDListAcceptanceTests;

public class AcceptanceTest
{
    [Fact]
    public async Task ShouldReturnData()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://localhost:7101/api/todo");
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());
    }
    [Fact]
    public async Task ShouldPrintToDoList()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://localhost:7101/api/todo");
        var toDos = JsonConvert.DeserializeObject<List<ToDo>>(await response.Content.ReadAsStringAsync());
        Assert.NotNull(toDos);
    }
}
