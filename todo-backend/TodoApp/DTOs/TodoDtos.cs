using TodoApp.Models;

namespace TodoApp.DTOs //necessary Data Transfer Object (DTOs) in order to not to expose real entity.
{
    public record TodoDto(int Id, string Title, string? Description, bool IsCompleted, string Priority, string Category, DateTime CreatedAt);
    public record CreatedToDoDto(string Title, string? Description, PriorityLevel Priority, TodoCategory Category);
    public record UpdateTodoDto(string Title, string? Description, bool IsCompleted, PriorityLevel Priority, TodoCategory Category);
}
