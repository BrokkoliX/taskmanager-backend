using TaskManager.Core.Entities;

namespace TaskManager.Core.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
}
