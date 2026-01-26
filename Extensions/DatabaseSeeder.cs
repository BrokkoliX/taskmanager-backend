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

        // Create 5 Random Users
        var random = new Random(Guid.NewGuid().GetHashCode());
        var firstNames = new[] { "Emma", "James", "Olivia", "Michael", "Sophia", "Daniel", "Isabella", "Alexander", "Ava", "Ethan" };
        var lastNames = new[] { "Thompson", "Garcia", "Rodriguez", "Martinez", "Anderson", "Taylor", "Thomas", "Moore", "Jackson", "Martin" };
        var departments = new[] { "Engineering", "Product", "Design", "Marketing", "Sales", "HR", "Finance", "Operations", "Support", "Security" };
        
        var users = new List<User>();
        for (int i = 0; i < 5; i++)
        {
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];
            var department = departments[random.Next(departments.Length)];
            
            users.Add(new User
            {
                Name = $"{firstName} {lastName}",
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@company.com",
                Department = department,
                IsActive = random.Next(0, 10) > 1, // 90% active
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 90))
            });
        }

        context.Users.AddRange(users);
        context.SaveChanges();
        Console.WriteLine($"✅ Created {users.Count} users");

        // Create 10 Random Tasks
        var taskTitles = new[]
        {
            "Implement feature", "Fix critical bug", "Refactor legacy code", "Write unit tests", "Update documentation",
            "Optimize performance", "Review pull requests", "Design new UI", "Conduct user testing", "Deploy to production",
            "Setup monitoring", "Implement caching", "Security audit", "Database migration", "API redesign"
        };
        
        var taskDescriptions = new[]
        {
            "Complete implementation with full coverage",
            "Investigate root cause and implement fix",
            "Improve code structure and maintainability",
            "Ensure comprehensive test coverage",
            "Update and review all documentation",
            "Identify bottlenecks and optimize",
            "Review code quality and suggest improvements",
            "Create modern and user-friendly design",
            "Gather feedback from end users",
            "Prepare and execute deployment",
            "Set up alerts and dashboards",
            "Implement caching strategy",
            "Perform comprehensive security review",
            "Plan and execute database schema changes",
            "Redesign API endpoints for better consistency"
        };
        
        var categories = new[] { "Backend", "Frontend", "DevOps", "Design", "Testing", "Documentation", "Security", "Performance", "Infrastructure", "Research" };
        
        var tasks = new List<TaskItem>();
        for (int i = 0; i < 10; i++)
        {
            var priority = (Priority)random.Next(0, 3);
            var isCompleted = random.Next(0, 10) > 6; // 30% completed
            var daysFromNow = random.Next(-30, 60);
            var assigneeId = users[random.Next(users.Count)].Id;
            
            tasks.Add(new TaskItem
            {
                Title = taskTitles[random.Next(taskTitles.Length)] + $" #{i + 1}",
                Description = taskDescriptions[random.Next(taskDescriptions.Length)],
                Priority = priority,
                AssigneeId = assigneeId,
                DueDate = DateTime.UtcNow.AddDays(daysFromNow),
                Category = categories[random.Next(categories.Length)],
                IsCompleted = isCompleted,
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(0, 60))
            });
        }

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
        Console.WriteLine($"✅ Created {tasks.Count} tasks");

        Console.WriteLine("✨ Database seeding complete!");
    }
}
