public class CreateWorkIntervalDTO
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
    public required int TodoItemId { get; set; }
    
}
