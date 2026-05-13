using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; } //nullable string
        public bool IsCompleted { get; set; } //whether task is completed or not
        public PriorityLevel Priority { get; set; } //Enum {LOW, MEDIUM, HIGH}
        public TodoCategory Category { get; set; } // Enum {WORK, PERSONAL}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
