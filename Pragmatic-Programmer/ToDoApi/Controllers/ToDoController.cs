using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TDList.Classes;
using TDList.Data;

namespace TDList.Tests;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly DataSource _dataSource;

    public ToDoController(DataSource dataSource)
    {
        _dataSource = dataSource;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var toDos = _dataSource.ReadAsync();
        return Ok(toDos);
    }
    [HttpPost]
    public async Task<ActionResult<ToDo>> Post([FromBody] ToDo toDo)
    {
        await _dataSource.InsertDataAsync(toDo);
        return Created(uri: "/api/todos/" + toDo.Id.ToString(), value: toDo);
    }
}