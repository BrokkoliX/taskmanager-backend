# 🎯 TaskManager Backend API

> **A Clean Architecture .NET 9 RESTful API for Task Management**

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![SQLite](https://img.shields.io/badge/Database-SQLite-blue)](https://www.sqlite.org/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-green)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)

A production-ready backend API built with Clean Architecture principles, featuring a multi-layered design that separates concerns and ensures maintainability, testability, and scalability.

---

## 🌟 Features

- ✅ **Clean Architecture** - Separation of concerns across 4 distinct layers
- ✅ **RESTful API** - Standard HTTP methods with proper status codes
- ✅ **Entity Framework Core** - Modern ORM with SQLite database
- ✅ **Swagger/OpenAPI** - Interactive API documentation
- ✅ **Dependency Injection** - Built-in IoC container
- ✅ **Async/Await** - Asynchronous operations for better performance
- ✅ **DTOs** - Data Transfer Objects for clean API contracts
- ✅ **Repository Pattern** - Abstracted data access layer
- ✅ **Service Layer** - Business logic separation
- ✅ **CORS Support** - Cross-origin resource sharing enabled

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│                   TaskManager.Api                    │
│              (Presentation Layer)                    │
│  Controllers | Middleware | Configuration           │
└────────────────────┬────────────────────────────────┘
                     │
                     ↓
┌─────────────────────────────────────────────────────┐
│              TaskManager.Application                 │
│               (Business Logic Layer)                 │
│     Services | Use Cases | Orchestration            │
└────────────────────┬────────────────────────────────┘
                     │
        ┌────────────┴────────────┐
        ↓                         ↓
┌──────────────────┐    ┌─────────────────────┐
│ TaskManager.Core │    │ TaskManager.        │
│  (Domain Layer)  │←───│  Infrastructure     │
│                  │    │  (Data Access)      │
│ Entities         │    │                     │
│ Interfaces       │    │ DbContext           │
│ DTOs            │    │ Repositories        │
└──────────────────┘    └─────────────────────┘
```

---

## 🚀 Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Git](https://git-scm.com/)
- IDE (VS Code, Visual Studio 2022, or Rider)

### Installation

```bash
# Clone the repository
git clone https://github.com/your-org/taskmanager-backend.git
cd taskmanager-backend

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run database migrations
cd src/TaskManager.Api
dotnet ef database update

# Run the application
dotnet run
```

The API will be available at:
- **HTTP:** http://localhost:5000
- **Swagger UI:** http://localhost:5000/swagger

---

## 📁 Project Structure

```
TaskManager.Backend/
│
├── src/
│   ├── TaskManager.Core/              # 🎯 Domain Layer
│   │   ├── Entities/                  #    Business entities
│   │   ├── Interfaces/                #    Contracts
│   │   │   ├── Repositories/          #    Repository interfaces
│   │   │   └── Services/              #    Service interfaces
│   │   └── DTOs/                      #    Data Transfer Objects
│   │
│   ├── TaskManager.Infrastructure/    # 🗄️ Data Access Layer
│   │   └── Data/                      #    DbContext & Repositories
│   │
│   ├── TaskManager.Application/       # 💼 Business Logic Layer
│   │   └── Services/                  #    Service implementations
│   │
│   └── TaskManager.Api/               # 🌐 Presentation Layer
│       ├── Controllers/               #    API Controllers
│       ├── Program.cs                 #    App configuration
│       └── appsettings.json           #    Configuration
│
├── tests/                             # 🧪 Test Projects
├── docs/                              # 📚 Documentation
│   └── README_BACKEND.md              #    Detailed backend docs
│
├── TaskManager.Backend.sln            # 📦 Solution file
├── .gitignore                         # 🚫 Git ignore rules
└── README.md                          # 📖 This file
```

---

## 🔌 API Endpoints

### Tasks API

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| `GET` | `/api/tasks` | Get all tasks | - |
| `GET` | `/api/tasks/{id}` | Get task by ID | - |
| `POST` | `/api/tasks` | Create new task | `CreateTaskDto` |
| `PUT` | `/api/tasks/{id}` | Update task | `UpdateTaskDto` |
| `DELETE` | `/api/tasks/{id}` | Delete task | - |

### Users API

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| `GET` | `/api/users` | Get all users | - |
| `GET` | `/api/users/{id}` | Get user by ID | - |
| `POST` | `/api/users` | Create new user | `CreateUserDto` |
| `PUT` | `/api/users/{id}` | Update user | `UpdateUserDto` |
| `DELETE` | `/api/users/{id}` | Delete user | - |

### Example Request

```bash
# Create a new task
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Implement feature X",
    "description": "Add new feature to the app",
    "priority": 1,
    "dueDate": "2024-12-31T23:59:59Z"
  }'
```

---

## 🗄️ Database Schema

### TaskItems

| Column | Type | Description |
|--------|------|-------------|
| `Id` | INTEGER | Primary key (auto-increment) |
| `Title` | TEXT | Task title (required) |
| `Description` | TEXT | Task description |
| `IsCompleted` | BOOLEAN | Completion status |
| `DueDate` | DATETIME | Due date |
| `Priority` | INTEGER | Priority (0=Low, 1=Medium, 2=High) |
| `AssignedToId` | INTEGER | Foreign key to Users |
| `CreatedAt` | DATETIME | Creation timestamp |
| `UpdatedAt` | DATETIME | Last update timestamp |

### Users

| Column | Type | Description |
|--------|------|-------------|
| `Id` | INTEGER | Primary key (auto-increment) |
| `Name` | TEXT | User name (required) |
| `Email` | TEXT | Email (unique, required) |
| `Role` | TEXT | User role |
| `CreatedAt` | DATETIME | Creation timestamp |

---

## 🛠️ Development

### Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName --project src/TaskManager.Infrastructure

# Update database
dotnet ef database update

# Rollback
dotnet ef database update PreviousMigrationName

# Remove last migration
dotnet ef migrations remove
```

### Code Quality

```bash
# Format code
dotnet format

# Analyze code
dotnet build /p:TreatWarningsAsErrors=true
```

---

## 📦 Dependencies

### Core Libraries
- **Microsoft.EntityFrameworkCore** (9.0.0) - ORM framework
- **Microsoft.EntityFrameworkCore.Sqlite** (9.0.0) - SQLite provider

### Development Tools
- **Swashbuckle.AspNetCore** (7.2.0) - Swagger/OpenAPI
- **AutoMapper** (13.0.1) - Object-to-object mapping
- **EPPlus** (7.5.2) - Excel export functionality

---

## 🔒 Security Features

- ✅ Input validation using Data Annotations
- ✅ SQL Injection protection (EF Core parameterization)
- ✅ CORS configuration
- ⏳ JWT Authentication (planned)
- ⏳ Rate limiting (planned)
- ⏳ API versioning (planned)

---

## 🚢 Deployment

### Docker (Recommended)

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]
```

```bash
# Build and run with Docker
docker build -t taskmanager-backend .
docker run -p 5000:80 taskmanager-backend
```

### Azure App Service

```bash
# Publish to Azure
az webapp up --name taskmanager-api --resource-group TaskManager-RG
```

---

## 📊 Performance

- **Response Time:** < 100ms (average)
- **Database:** SQLite (development) / PostgreSQL or SQL Server (production recommended)
- **Async Operations:** All I/O operations are asynchronous
- **Connection Pooling:** Enabled by default

---

## 🧪 Testing Strategy

### Unit Tests
- Test individual components in isolation
- Mock dependencies using Moq
- Target: 80%+ code coverage

### Integration Tests
- Test full request/response cycles
- Use in-memory database
- Validate business logic

### API Tests
- Test HTTP endpoints
- Use WebApplicationFactory
- Validate status codes and responses

---

## 📚 Documentation

- [Backend Architecture Guide](docs/README_BACKEND.md)
- [API Documentation](http://localhost:5000/swagger) (when running)
- [Migration Plan](ARCHITECTURE_MIGRATION_PLAN.md)
- [Database Schema](docs/DATABASE_SCHEMA.md)

---

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Commit Convention

We follow [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: Add new feature
fix: Fix bug
docs: Update documentation
refactor: Refactor code
test: Add tests
chore: Update dependencies
```

---

## 🐛 Troubleshooting

### Common Issues

**Issue:** Database locked error
```bash
# Solution: Stop all instances and reset database
pkill -f "dotnet.*TaskManager"
dotnet ef database drop --force
dotnet ef database update
```

**Issue:** Port already in use
```bash
# Solution: Change port in appsettings.json or use environment variable
export ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

**Issue:** Build errors after pull
```bash
# Solution: Clean and restore
dotnet clean
dotnet restore
dotnet build
```

---

## 📈 Roadmap

- [ ] Add JWT Authentication
- [ ] Implement API versioning
- [ ] Add Redis caching
- [ ] Add rate limiting
- [ ] Add health checks
- [ ] Add logging with Serilog
- [ ] Add monitoring with Application Insights
- [ ] Migrate to PostgreSQL for production
- [ ] Add background jobs with Hangfire
- [ ] Add real-time updates with SignalR

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👥 Team

- **Backend Lead:** [Your Name]
- **Contributors:** [List contributors]

---

## 📞 Support

- **Issues:** [GitHub Issues](https://github.com/your-org/taskmanager-backend/issues)
- **Discussions:** [GitHub Discussions](https://github.com/your-org/taskmanager-backend/discussions)
- **Email:** support@taskmanager.com

---

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- Inspired by [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- Database powered by [SQLite](https://www.sqlite.org/)

---

<div align="center">

**⭐ If you find this project useful, please consider giving it a star! ⭐**

Made with ❤️ using Clean Architecture

</div>
