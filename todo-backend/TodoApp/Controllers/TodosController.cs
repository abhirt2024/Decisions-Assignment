using Microsoft.AspNetCore.Mvc;
using TodoApp.DTOs;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService) //Dependency Injection, loose coupling.
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos([FromQuery] string? search, [FromQuery] TodoCategory? category)
        {
            var todos = await _todoService.GetTodoAsync(search, category);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodo(CreatedToDoDto dto)
        {
            var createdTodo = await _todoService.CreateTodoAsync(dto);
            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id}, createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, UpdateTodoDto dto)
        {
            await _todoService.UpdateTodoAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
