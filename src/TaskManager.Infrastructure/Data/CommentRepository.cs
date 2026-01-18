using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;

namespace TaskManager.Infrastructure.Data;

public class CommentRepository : ICommentRepository
{
    private readonly TaskDbContext _context;

    public CommentRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await _context.Comments
            .Include(c => c.CreatedBy)
            .Include(c => c.TaskItem)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetByTaskIdAsync(int taskId)
    {
        return await _context.Comments
            .Include(c => c.CreatedBy)
            .Where(c => c.TaskItemId == taskId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
            .Include(c => c.CreatedBy)
            .Include(c => c.TaskItem)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        
        // Reload to get navigation properties
        await _context.Entry(comment)
            .Reference(c => c.CreatedBy)
            .LoadAsync();
        await _context.Entry(comment)
            .Reference(c => c.TaskItem)
            .LoadAsync();
        
        return comment;
    }

    public async Task<Comment?> UpdateAsync(Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Content = comment.Content;
        existingComment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        // Reload navigation properties
        await _context.Entry(existingComment)
            .Reference(c => c.CreatedBy)
            .LoadAsync();
        await _context.Entry(existingComment)
            .Reference(c => c.TaskItem)
            .LoadAsync();
        
        return existingComment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return false;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }
}
