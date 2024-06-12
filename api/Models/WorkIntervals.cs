using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class WorkInterval
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TodoItemId { get; set; }

        [Required]
        public User User { get; set; } = null!;

        [Required]
        public TodoItem TodoItem { get; set; } = null!;
    }
}
