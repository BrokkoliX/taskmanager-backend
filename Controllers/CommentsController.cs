using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
    {
        _commentService = commentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all comments
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
    {
        try
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all comments");
            return StatusCode(500, "An error occurred while retrieving comments");
        }
    }

    /// <summary>
    /// Get comments by task ID
    /// </summary>
    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetByTaskId(int taskId)
    {
        try
        {
            var comments = await _commentService.GetByTaskIdAsync(taskId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving comments for task {TaskId}", taskId);
            return StatusCode(500, "An error occurred while retrieving comments");
        }
    }

    /// <summary>
    /// Get a specific comment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetById(int id)
    {
        try
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving comment {Id}", id);
            return StatusCode(500, "An error occurred while retrieving the comment");
        }
    }

    /// <summary>
    /// Create a new comment
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto createCommentDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentService.CreateAsync(createCommentDto);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument when creating comment");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment");
            return StatusCode(500, "An error occurred while creating the comment");
        }
    }

    /// <summary>
    /// Update an existing comment
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> Update(int id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentService.UpdateAsync(id, updateCommentDto);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating comment {Id}", id);
            return StatusCode(500, "An error occurred while updating the comment");
        }
    }

    /// <summary>
    /// Delete a comment
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result = await _commentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment {Id}", id);
            return StatusCode(500, "An error occurred while deleting the comment");
        }
    }
}
