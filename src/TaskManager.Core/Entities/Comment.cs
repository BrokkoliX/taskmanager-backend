namespace TaskManager.Core.Entities;

public class Comment
{
    public int Id { get; set; }

    public required string Content { get; set; }

    // Foreign key to TaskItem
    public int TaskItemId { get; set; }

    // Navigation property to Task
    public TaskItem? TaskItem { get; set; }

    // Foreign key to User (who created the comment)
    public int? CreatedByUserId { get; set; }

    // Navigation property to User
    public User? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
