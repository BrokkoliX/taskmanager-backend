using TaskManager.Core.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITaskRepository _taskRepository;

    public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository)
    {
        _commentRepository = commentRepository;
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<CommentDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        return comments.Select(MapToDto);
    }

    public async Task<IEnumerable<CommentDto>> GetByTaskIdAsync(int taskId)
    {
        var comments = await _commentRepository.GetByTaskIdAsync(taskId);
        return comments.Select(MapToDto);
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        return comment != null ? MapToDto(comment) : null;
    }

    public async Task<CommentDto> CreateAsync(CreateCommentDto createCommentDto)
    {
        // Validate that the task exists
        var task = await _taskRepository.GetByIdAsync(createCommentDto.TaskItemId);
        if (task == null)
        {
            throw new ArgumentException($"Task with ID {createCommentDto.TaskItemId} not found.");
        }

        var comment = new Comment
        {
            Content = createCommentDto.Content,
            TaskItemId = createCommentDto.TaskItemId,
            CreatedByUserId = createCommentDto.CreatedByUserId,
            CreatedAt = DateTime.UtcNow
        };

        var createdComment = await _commentRepository.AddAsync(comment);
        return MapToDto(createdComment);
    }

    public async Task<CommentDto?> UpdateAsync(int id, UpdateCommentDto updateCommentDto)
    {
        var existingComment = await _commentRepository.GetByIdAsync(id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Content = updateCommentDto.Content;
        existingComment.UpdatedAt = DateTime.UtcNow;

        var updatedComment = await _commentRepository.UpdateAsync(existingComment);
        return updatedComment != null ? MapToDto(updatedComment) : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _commentRepository.DeleteAsync(id);
    }

    private static CommentDto MapToDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskItemId = comment.TaskItemId,
            CreatedByUserId = comment.CreatedByUserId,
            CreatedByUserName = comment.CreatedBy?.Name,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }
}
