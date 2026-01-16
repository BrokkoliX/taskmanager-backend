using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Api.Controllers;

/// <summary>
/// Controller for managing user resources
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    // GET: api/users
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/users/active
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveUsers()
    {
        var result = await _service.GetActiveUsersAsync();
        return Ok(result);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        
        if (result == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        
        return Ok(result);
    }

    // POST: api/users
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] User newUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(newUser);
            return CreatedAtAction(
                actionName: nameof(GetUserById),
                routeValues: new { id = result.Id },
                value: result);
        }
        catch (ArgumentException argumentEx)
        {
            return BadRequest(new { message = argumentEx.Message });
        }
        catch (InvalidOperationException operationEx)
        {
            return BadRequest(new { message = operationEx.Message });
        }
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateAsync(id, updatedUser);
            
            if (result == null)
                return NotFound(new { message = $"User with ID {id} not found" });
            
            return Ok(result);
        }
        catch (ArgumentException argumentEx)
        {
            return BadRequest(new { message = argumentEx.Message });
        }
        catch (InvalidOperationException operationEx)
        {
            return BadRequest(new { message = operationEx.Message });
        }
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var wasDeleted = await _service.DeleteAsync(id);
        
        if (!wasDeleted)
            return NotFound(new { message = $"User with ID {id} not found" });
        
        return NoContent();
    }
}
