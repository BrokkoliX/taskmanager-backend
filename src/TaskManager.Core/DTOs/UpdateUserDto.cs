namespace TaskManager.Core.DTOs;

public class UpdateUserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }
    public bool? IsActive { get; set; }
}
