using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<IEnumerable<Comment>> GetByTaskIdAsync(int taskId);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> AddAsync(Comment comment);
    Task<Comment?> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(int id);
}
