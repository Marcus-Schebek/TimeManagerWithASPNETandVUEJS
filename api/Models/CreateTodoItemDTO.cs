using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class CreateTodoItemDTO
    {
        [Required]
        public required string Description { get; set; }
        [Required]
        public int AssignedToUserId { get; set; }
        public required CreateWorkIntervalDTO WorkInterval { get; set; }
    }

}
