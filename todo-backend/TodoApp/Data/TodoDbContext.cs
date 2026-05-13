using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
namespace TodoApp.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { } 
        //Receives the DB configurations like DB provider, connection string & additional setting from
        //DI container registered in Program.cs and passes them to the DbContext constructor for the
        //DB connection.

        public DbSet<TodoItem> Todos => Set<TodoItem>(); //Creates a table named 'Todos'
    }
}
