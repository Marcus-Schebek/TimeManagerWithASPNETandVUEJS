
namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool isManager { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public ICollection<WorkInterval> WorkIntervals { get; set; } = new List<WorkInterval>();

    }

}
