using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.DTOs;

public class UpdateCommentDto
{
    [Required(ErrorMessage = "Content is required")]
    [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
    public string Content { get; set; } = string.Empty;
}
