using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TDList.Data;

namespace TDList.Tests
{
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
    }
}