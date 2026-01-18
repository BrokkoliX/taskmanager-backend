# 🚀 TaskManager Backend - Quick Start

> **Get up and running in 5 minutes**

---

## ⚡ Prerequisites

- .NET 9 SDK installed
- Git installed
- Your favorite IDE

---

## 🏃 Quick Start (Local)

```bash
# 1. Navigate to project
cd /Users/robbie/Tab/TabnineTaskDemo/TaskManager.Api

# 2. Restore dependencies
dotnet restore

# 3. Build solution
dotnet build TaskManager.Backend.sln

# 4. Run the API
dotnet run --project TaskManager.Api.csproj

# 5. Access Swagger UI
open http://localhost:5000/swagger
```

**That's it!** 🎉 Your API is running!

---

## 🧪 Test the API

```bash
# Get all tasks
curl http://localhost:5000/api/tasks

# Create a task
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "My First Task",
    "description": "Testing the API",
    "priority": 1
  }'

# Get all users
curl http://localhost:5000/api/users
```

---

## 📁 Project Structure

```
src/
├── TaskManager.Core/          # Domain (Entities, Interfaces, DTOs)
├── TaskManager.Infrastructure/ # Data (DbContext, Repositories)
├── TaskManager.Application/   # Business Logic (Services)
└── [API at root]/             # Controllers, Program.cs
```

---

## 🔧 Common Commands

```bash
# Clean solution
dotnet clean

# Rebuild
dotnet build --no-incremental

# Run with hot reload
dotnet watch run

# Run tests (when implemented)
dotnet test

# Database migrations
dotnet ef migrations add MigrationName --project src/TaskManager.Infrastructure
dotnet ef database update
```

---

## 📦 Migration to Separate Repository

When you're ready to push to a separate repo:

```bash
# 1. Copy gitignore
cp .gitignore.backend .gitignore

# 2. Initialize git (if needed)
git init

# 3. Add files
git add .

# 4. Commit
git commit -m "feat: Initial commit - Clean Architecture Backend"

# 5. Add remote
git remote add origin YOUR_REPO_URL

# 6. Push
git push -u origin main
```

**Full guide:** [docs/REPOSITORY_MIGRATION_GUIDE.md](docs/REPOSITORY_MIGRATION_GUIDE.md)

---

## 🆘 Troubleshooting

**Port already in use?**
```bash
export ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

**Database locked?**
```bash
pkill -f "dotnet.*TaskManager"
rm taskmanager.db
dotnet ef database update
```

**Build errors?**
```bash
dotnet clean
dotnet restore
dotnet build
```

---

## 📚 Documentation

- **Architecture Guide**: [docs/README_BACKEND.md](docs/README_BACKEND.md)
- **Full README**: [README.backend.md](README.backend.md)
- **Migration Plan**: [ARCHITECTURE_MIGRATION_PLAN.md](ARCHITECTURE_MIGRATION_PLAN.md)
- **Completion Summary**: [MIGRATION_COMPLETE.md](MIGRATION_COMPLETE.md)

---

## 🎯 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | List all tasks |
| GET | `/api/tasks/{id}` | Get task by ID |
| POST | `/api/tasks` | Create new task |
| PUT | `/api/tasks/{id}` | Update task |
| DELETE | `/api/tasks/{id}` | Delete task |
| GET | `/api/users` | List all users |
| GET | `/api/users/{id}` | Get user by ID |
| POST | `/api/users` | Create user |
| PUT | `/api/users/{id}` | Update user |
| DELETE | `/api/users/{id}` | Delete user |

---

## 🔗 Quick Links

- **Swagger UI**: http://localhost:5000/swagger
- **API Base**: http://localhost:5000/api
- **Health Check**: http://localhost:5000/health (if implemented)

---

## ✅ Verification Checklist

After starting the API:

- [ ] Swagger UI loads at http://localhost:5000/swagger
- [ ] GET `/api/tasks` returns 200 OK
- [ ] GET `/api/users` returns 200 OK
- [ ] Can create a task via POST
- [ ] Can create a user via POST
- [ ] No errors in console

---

**Need more help?** Check the full documentation or open an issue!

**Happy coding!** 🚀
