using System.ComponentModel.DataAnnotations;

namespace api.Models{
public class TodoItem {
    [Required]
    public int Id { get; set; }
    
    [Required]
    public required string Description { get; set; }
    
    public bool IsCompleted { get; set; }
    public int HoursWorked { get; set; }
    
    [Required]
    public required User AssignedToUser { get; set; }
    
    [Required]
    public required ICollection<WorkInterval> WorkIntervals { get; set; }
}

}
