using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Api.Extensions;

public static class DatabaseSeeder
{
    public static void SeedDatabase(this TaskDbContext context)
    {
        // Check if database is already seeded
        if (context.Users.Any() || context.Tasks.Any())
        {
            Console.WriteLine("⚠️  Database already contains data. Skipping seed.");
            return;
        }

        Console.WriteLine("🌱 Seeding database with sample data...");

        // Create Users
        var users = new List<User>
        {
            new User
            {
                Name = "Alice Johnson",
                Email = "alice.johnson@company.com",
                Department = "Engineering",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Bob Smith",
                Email = "bob.smith@company.com",
                Department = "Product",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Carol Davis",
                Email = "carol.davis@company.com",
                Department = "Design",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "David Wilson",
                Email = "david.wilson@company.com",
                Department = "Marketing",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
        Console.WriteLine($"✅ Created {users.Count} users");

        // Create Tasks
        var tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Title = "Implement user authentication",
                Description = "Add JWT-based authentication to the API",
                Priority = Priority.High,
                AssigneeId = users[0].Id,
                DueDate = DateTime.UtcNow.AddDays(28),
                Category = "Backend",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Design landing page mockup",
                Description = "Create high-fidelity mockups for the new landing page",
                Priority = Priority.Medium,
                AssigneeId = users[2].Id,
                DueDate = DateTime.UtcNow.AddDays(7),
                Category = "Design",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Write API documentation",
                Description = "Document all endpoints with examples and request/response schemas",
                Priority = Priority.Medium,
                AssigneeId = users[0].Id,
                DueDate = DateTime.UtcNow.AddDays(12),
                Category = "Documentation",
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new TaskItem
            {
                Title = "Set up CI/CD pipeline",
                Description = "Configure GitHub Actions for automated testing and deployment",
                Priority = Priority.High,
                AssigneeId = users[0].Id,
                DueDate = DateTime.UtcNow.AddDays(14),
                Category = "DevOps",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Create marketing campaign",
                Description = "Plan Q1 2025 marketing campaign for product launch",
                Priority = Priority.Medium,
                AssigneeId = users[3].Id,
                DueDate = DateTime.UtcNow.AddDays(23),
                Category = "Marketing",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Implement dark mode",
                Description = "Add dark mode theme support to the application",
                Priority = Priority.Low,
                AssigneeId = users[2].Id,
                DueDate = DateTime.UtcNow.AddDays(42),
                Category = "Frontend",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Fix mobile responsiveness",
                Description = "Resolve layout issues on mobile devices",
                Priority = Priority.High,
                AssigneeId = users[2].Id,
                DueDate = DateTime.UtcNow.AddDays(10),
                Category = "Frontend",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Database optimization",
                Description = "Add indexes and optimize slow queries",
                Priority = Priority.Medium,
                AssigneeId = users[0].Id,
                DueDate = DateTime.UtcNow.AddDays(18),
                Category = "Backend",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "User research interviews",
                Description = "Conduct 10 user interviews for feature validation",
                Priority = Priority.Medium,
                AssigneeId = users[1].Id,
                DueDate = DateTime.UtcNow.AddDays(9),
                Category = "Research",
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new TaskItem
            {
                Title = "Prepare product demo",
                Description = "Create demo script and slides for stakeholder presentation",
                Priority = Priority.High,
                AssigneeId = users[1].Id,
                DueDate = DateTime.UtcNow.AddDays(8),
                Category = "Product",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
        Console.WriteLine($"✅ Created {tasks.Count} tasks");

        Console.WriteLine("✨ Database seeding complete!");
    }
}
