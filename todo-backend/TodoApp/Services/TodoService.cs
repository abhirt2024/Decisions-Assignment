using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.DTOs;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoDto> CreateTodoAsync(CreatedToDoDto dto)
        {
            var todo = new TodoItem // accepting the data through DTO and assigning each field of DTO to the entity fields.
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Category = dto.Category
            };
            _context.Todos.Add(todo); 
            await _context.SaveChangesAsync(); //saving the changes to the DB.
            return MapToDto(todo);
        }

        public async Task DeleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) throw new KeyNotFoundException($"Todo item with Id {id} was not found!"); //throwing exception if the data with primary key {id} is not found.
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoDto>> GetTodoAsync(string? search, TodoCategory? category)
        {
            var query = _context.Todos.AsQueryable(); //IQueryable enables deferred execution and query composition.
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.ToLower();
                query = query.Where(t=>t.Title.ToLower().Contains(searchTerm) || 
                (t.Description != null && t.Description.ToLower().Contains(searchTerm)));
            }
            if (category.HasValue)
            {
                query = query.Where(t=>t.Category == category.Value);
            }

            var todos = await query.ToListAsync(); //here the actual query runs in the DB.
            return todos.Select(MapToDto);
        }

        public async Task<TodoDto> GetTodoByIdAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) throw new KeyNotFoundException($"Todo item with Id {id} was not found!");
            return MapToDto(todo);
        }

        public async Task UpdateTodoAsync(int id, UpdateTodoDto dto)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) throw new KeyNotFoundException($"Todo item with Id {id} was not found!");

            todo.Title = dto.Title;
            todo.Description = dto.Description; 
            todo.Category = dto.Category;
            todo.IsCompleted = dto.IsCompleted;
            todo.Priority = dto.Priority;

            await _context.SaveChangesAsync();
        }

        private static TodoDto MapToDto(TodoItem t) //helper method to convert entity to DTO to return as the result with changes made.
        {
            return new TodoDto(
                t.Id,
                t.Title,
                t.Description,
                t.IsCompleted,
                t.Priority.ToString(),
                t.Category.ToString(),
                t.CreatedAt
            );
        }
    }
}
