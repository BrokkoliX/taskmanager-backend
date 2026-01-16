using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<User>> GetAllAsync() => _repository.GetAllAsync();

    public Task<IEnumerable<User>> GetActiveUsersAsync() => _repository.GetActiveUsersAsync();

    public Task<User?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<User?> GetByEmailAsync(string email) => _repository.GetByEmailAsync(email);

    public async Task<User> CreateAsync(User user)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name is required.", nameof(user));
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("User email is required.", nameof(user));
        }

        // Check if email already exists
        var existing = await _repository.GetByEmailAsync(user.Email);
        if (existing != null)
        {
            throw new InvalidOperationException($"A user with email '{user.Email}' already exists.");
        }

        user.Id = 0;
        return await _repository.AddAsync(user);
    }

    public async Task<User?> UpdateAsync(int id, User user)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
        {
            return null;
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name is required.", nameof(user));
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("User email is required.", nameof(user));
        }

        // Check if email is being changed and if new email already exists
        if (user.Email != existing.Email)
        {
            var emailExists = await _repository.GetByEmailAsync(user.Email);
            if (emailExists != null)
            {
                throw new InvalidOperationException($"A user with email '{user.Email}' already exists.");
            }
        }

        existing.Name = user.Name;
        existing.Email = user.Email;
        existing.Department = user.Department;
        existing.IsActive = user.IsActive;

        return await _repository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}
