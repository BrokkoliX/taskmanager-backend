# TaskManager Backend

> **Clean Architecture .NET 9 API with SQLite Database**

This repository contains the backend implementation of the TaskManager application, built using Clean Architecture principles with a clear separation of concerns across multiple layers.

---

## 📁 Project Structure

```
TaskManager.Backend/
├── src/
│   ├── TaskManager.Core/              # Domain Layer (Business Entities & Interfaces)
│   ├── TaskManager.Infrastructure/    # Data Access Layer (Database & Repositories)
│   ├── TaskManager.Application/       # Business Logic Layer (Services & Use Cases)
│   └── TaskManager.Api/               # API Layer (Controllers & Endpoints)
├── tests/
│   ├── TaskManager.Core.Tests/
│   ├── TaskManager.Application.Tests/
│   └── TaskManager.Api.Tests/
├── docs/
│   └── README_BACKEND.md
├── TaskManager.Backend.sln
└── README.md
```

---

## 🏗️ Architecture Overview

### **Clean Architecture Layers**

#### **1. TaskManager.Core** (Domain Layer)
**Purpose:** Pure business domain logic with no external dependencies

**Contains:**
- **Entities:** `TaskItem`, `User`, `Priority` enum
- **Interfaces/Repositories:** `ITaskRepository`, `IUserRepository`
- **Interfaces/Services:** `ITaskService`, `IUserService`
- **DTOs:** Data Transfer Objects for API communication
  - `TaskDto`, `CreateTaskDto`, `UpdateTaskDto`
  - `UserDto`, `CreateUserDto`

**Dependencies:** None (Pure .NET)

**Why:** This layer contains the core business rules and entities that are independent of any infrastructure concerns.

---

#### **2. TaskManager.Infrastructure** (Data Access Layer)
**Purpose:** Database access, persistence, and external dependencies

**Contains:**
- **Data/TaskDbContext.cs:** Entity Framework Core database context
- **Data/SqliteTaskRepository.cs:** Task data access implementation
- **Data/UserRepository.cs:** User data access implementation
- **Migrations:** EF Core database migrations
- **Database:** SQLite database file (taskmanager.db)

**Dependencies:**
- TaskManager.Core
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore

**Why:** Isolates database-specific code, making it easy to swap databases without affecting business logic.

---

#### **3. TaskManager.Application** (Business Logic Layer)
**Purpose:** Application-specific business rules and orchestration

**Contains:**
- **Services/TaskService.cs:** Task business logic
- **Services/UserService.cs:** User business logic
- **Validators:** Business rule validation (optional)
- **Mappings:** Entity ↔ DTO mappings (AutoMapper)

**Dependencies:**
- TaskManager.Core
- AutoMapper (optional)

**Why:** Implements use cases and orchestrates between the API and data layers without concerning itself with HTTP or database details.

---

#### **4. TaskManager.Api** (Presentation Layer)
**Purpose:** HTTP API endpoints and request/response handling

**Contains:**
- **Controllers/TasksController.cs:** RESTful task endpoints
- **Controllers/UsersController.cs:** RESTful user endpoints
- **Program.cs:** Application configuration and dependency injection
- **Middleware:** Error handling, authentication (optional)
- **appsettings.json:** Configuration

**Dependencies:**
- TaskManager.Core
- TaskManager.Application
- TaskManager.Infrastructure
- Swashbuckle.AspNetCore (Swagger)

**Why:** Handles HTTP concerns separate from business logic, making the API easily testable and maintainable.

---

## 🚀 Getting Started

### **Prerequisites**
- .NET 9 SDK
- IDE (Visual Studio 2022, VS Code, or Rider)
- SQLite (included)

### **Installation**

1. **Clone the repository**
   ```bash
   git clone <backend-repo-url>
   cd TaskManager.Backend
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build TaskManager.Backend.sln
   ```

4. **Run database migrations**
   ```bash
   cd src/TaskManager.Api
   dotnet ef database update
   ```

5. **Run the API**
   ```bash
   dotnet run --project src/TaskManager.Api/TaskManager.Api.csproj
   ```

6. **Access Swagger UI**
   - Navigate to: `http://localhost:5000/swagger`

---

## 📊 API Endpoints

### **Tasks**

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks |
| GET | `/api/tasks/{id}` | Get task by ID |
| POST | `/api/tasks` | Create new task |
| PUT | `/api/tasks/{id}` | Update existing task |
| DELETE | `/api/tasks/{id}` | Delete task |

### **Users**

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get user by ID |
| POST | `/api/users` | Create new user |
| PUT | `/api/users/{id}` | Update user |
| DELETE | `/api/users/{id}` | Delete user |

---

## 🗄️ Database Schema

### **TaskItems Table**
```sql
CREATE TABLE TaskItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    IsCompleted BOOLEAN NOT NULL DEFAULT 0,
    DueDate DATETIME,
    Priority INTEGER NOT NULL DEFAULT 0,
    AssignedToId INTEGER,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME,
    FOREIGN KEY (AssignedToId) REFERENCES Users(Id)
);
```

### **Users Table**
```sql
CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    Role TEXT,
    CreatedAt DATETIME NOT NULL
);
```

---

## 🔧 Development

### **Running Tests**
```bash
dotnet test
```

### **Database Migrations**

**Create a new migration:**
```bash
cd src/TaskManager.Api
dotnet ef migrations add MigrationName --project ../TaskManager.Infrastructure
```

**Apply migrations:**
```bash
dotnet ef database update
```

**Rollback migration:**
```bash
dotnet ef database update PreviousMigrationName
```

### **Code Generation**

**Add new controller:**
```bash
dotnet aspnet-codegenerator controller -name NewController -api -outDir Controllers
```

---

## 📦 NuGet Packages

### **Core**
- `Microsoft.EntityFrameworkCore` (9.0.0)
- `Microsoft.EntityFrameworkCore.Sqlite` (9.0.0)

### **Application**
- `AutoMapper` (13.0.1)
- `EPPlus` (7.5.2)

### **API**
- `Swashbuckle.AspNetCore` (7.2.0)
- `Microsoft.EntityFrameworkCore.Design` (9.0.0)

---

## 🧪 Testing Strategy

### **Unit Tests** (`*.Tests`)
- Test individual components in isolation
- Mock dependencies using Moq
- Fast execution, no database required

### **Integration Tests**
- Test full request/response cycles
- Use in-memory database
- Validate business logic with real data flow

### **API Tests**
- Test HTTP endpoints
- Use WebApplicationFactory
- Validate status codes and responses

---

## 🔒 Security

- Input validation using Data Annotations
- SQL Injection protection via EF Core parameterization
- CORS configuration for allowed origins
- Optional: JWT Authentication (add if needed)
- Optional: Rate limiting (add if needed)

---

## 🎯 Best Practices

### **Dependency Injection**
All services and repositories are registered in `Program.cs`:
```csharp
builder.Services.AddScoped<ITaskRepository, SqliteTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
```

### **Async/Await**
All data access and service methods use `async/await` for better performance

### **DTOs**
Never expose entities directly - always use DTOs for API communication

### **Error Handling**
Use try-catch blocks and return appropriate HTTP status codes

---

## 🐛 Troubleshooting

### **Build Errors**
```bash
# Clean and rebuild
dotnet clean
dotnet build
```

### **Database Locked**
```bash
# Stop all running instances
pkill -f "dotnet.*TaskManager"
# Delete and recreate database
rm taskmanager.db
dotnet ef database update
```

### **Port Already in Use**
```bash
# Change port in Properties/launchSettings.json
# Or use environment variable
export ASPNETCORE_URLS="http://localhost:5001"
```

---

## 📚 Additional Documentation

- [Architecture Migration Plan](../ARCHITECTURE_MIGRATION_PLAN.md)
- [API Documentation](./API_DOCUMENTATION.md) (generated by Swagger)
- [Database Schema](./DATABASE_SCHEMA.md)

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License.

---

## 📞 Support

For issues or questions, please open an issue in the repository.

---

**Built with ❤️ using Clean Architecture principles**
