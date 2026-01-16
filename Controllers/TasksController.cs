using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Get all tasks
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Search tasks by query string
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> Search(
        [FromQuery] string? query,
        [FromQuery] bool onlyIncomplete = false)
    {
        var tasks = await _taskService.SearchAsync(query, onlyIncomplete);
        return Ok(tasks);
    }

    /// <summary>
    /// Get a specific task by ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem task)
    {
        try
        {
            var created = await _taskService.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing task
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskItem>> Update(int id, [FromBody] TaskItem task)
    {
        var updated = await _taskService.UpdateAsync(id, task);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Export tasks to Excel
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> ExportToExcel(
        [FromQuery] string? query = null,
        [FromQuery] bool onlyIncomplete = false)
    {
        var excelData = await _taskService.ExportToExcelAsync(query, onlyIncomplete);
        var fileName = $"tasks_export_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx";
        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>
    /// Export tasks to CSV
    /// </summary>
    [HttpGet("export/csv")]
    public async Task<IActionResult> ExportToCsv(
        [FromQuery] string? query = null,
        [FromQuery] bool onlyIncomplete = false)
    {
        var csvData = await _taskService.ExportToCsvAsync(query, onlyIncomplete);
        var fileName = $"tasks_export_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
        return File(csvData, "text/csv", fileName);
    }
}
