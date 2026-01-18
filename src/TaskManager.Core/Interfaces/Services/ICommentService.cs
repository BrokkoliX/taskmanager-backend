using TaskManager.Core.DTOs;

namespace TaskManager.Core.Interfaces.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllAsync();
    Task<IEnumerable<CommentDto>> GetByTaskIdAsync(int taskId);
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto> CreateAsync(CreateCommentDto createCommentDto);
    Task<CommentDto?> UpdateAsync(int id, UpdateCommentDto updateCommentDto);
    Task<bool> DeleteAsync(int id);
}
