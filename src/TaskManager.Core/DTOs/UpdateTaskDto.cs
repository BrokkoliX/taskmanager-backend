using TaskManager.Core.Entities;

namespace TaskManager.Core.DTOs;

public class UpdateTaskDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public int? AssigneeId { get; set; }
    public Priority? Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; }
}
