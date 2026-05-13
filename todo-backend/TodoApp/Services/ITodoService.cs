using TodoApp.DTOs;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodoAsync(string? search, TodoCategory? category);
        Task<TodoDto> GetTodoByIdAsync(int id);
        Task<TodoDto> CreateTodoAsync(CreatedToDoDto dto);
        Task UpdateTodoAsync(int id, UpdateTodoDto dto);
        Task DeleteTodoAsync(int id);
    }
}
