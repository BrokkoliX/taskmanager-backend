# 🚀 Quick Start with Sample Data

**Updated:** January 18, 2025  
**Status:** Ready to run with auto-seeding

---

## ⚡ Super Quick Start (Automatic Seeding)

The application now automatically seeds the database with sample data on first run!

### **Step 1: Reset Database**
```bash
cd TaskManager.Api
rm taskmanager.db
```

### **Step 2: Run Application**
```bash
dotnet run
```

**That's it!** The application will:
1. ✅ Create the database
2. ✅ Create the Comments table
3. ✅ Automatically add 4 sample users
4. ✅ Automatically add 10 sample tasks

---

## 📊 What Gets Created Automatically

### **4 Users:**
| ID | Name | Email | Department |
|----|------|-------|------------|
| 1 | Alice Johnson | alice.johnson@company.com | Engineering |
| 2 | Bob Smith | bob.smith@company.com | Product |
| 3 | Carol Davis | carol.davis@company.com | Design |
| 4 | David Wilson | david.wilson@company.com | Marketing |

### **10 Tasks:**
| # | Title | Priority | Assignee | Category | Status |
|---|-------|----------|----------|----------|--------|
| 1 | Implement user authentication | High | Alice | Backend | Open |
| 2 | Design landing page mockup | Medium | Carol | Design | Open |
| 3 | Write API documentation | Medium | Alice | Documentation | ✅ Done |
| 4 | Set up CI/CD pipeline | High | Alice | DevOps | Open |
| 5 | Create marketing campaign | Medium | David | Marketing | Open |
| 6 | Implement dark mode | Low | Carol | Frontend | Open |
| 7 | Fix mobile responsiveness | High | Carol | Frontend | Open |
| 8 | Database optimization | Medium | Alice | Backend | Open |
| 9 | User research interviews | Medium | Bob | Research | ✅ Done |
| 10 | Prepare product demo | High | Bob | Product | Open |

---

## 🔍 Verify the Data

### **View in Browser:**
Once the app is running, visit:
- **Swagger UI:** http://localhost:5000/swagger
- **All Users:** http://localhost:5000/api/users
- **All Tasks:** http://localhost:5000/api/tasks

### **Using curl:**
```bash
# Get all users
curl http://localhost:5000/api/users

# Get all tasks
curl http://localhost:5000/api/tasks

# Get tasks assigned to Alice (ID: 1)
curl http://localhost:5000/api/tasks

# Get a specific task
curl http://localhost:5000/api/tasks/1
```

---

## 🔄 Manual Seeding (Alternative Methods)

### **Option 1: Using Bash Script**
If the app is already running:
```bash
cd TaskManager.Api
./seed-data.sh
```

### **Option 2: Recreate Database**
If you need fresh data:
```bash
cd TaskManager.Api
rm taskmanager.db
dotnet run
```

---

## 🧪 Try the Comments Feature

After seeding, you can add comments to tasks:

```bash
# Add a comment to task #1
curl -X POST http://localhost:5000/api/comments \
  -H "Content-Type: application/json" \
  -d '{
    "content": "This is an important security feature!",
    "taskItemId": 1,
    "createdByUserId": 2
  }'

# Get all comments for task #1
curl http://localhost:5000/api/comments/task/1
```

---

## 🎯 Next Steps

### **1. Explore the API**
Visit Swagger UI at http://localhost:5000/swagger to:
- See all available endpoints
- Test API calls interactively
- View request/response schemas

### **2. Create Your Own Data**

**Create a User:**
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Your Name",
    "email": "your.email@company.com",
    "department": "Engineering"
  }'
```

**Create a Task:**
```bash
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "My First Task",
    "description": "Task description here",
    "priority": 1,
    "assigneeId": 1,
    "category": "Development",
    "isCompleted": false
  }'
```

### **3. Update the Frontend**
The frontend at `../taskmanager-frontend/` can now fetch and display:
- Users from `/api/users`
- Tasks from `/api/tasks`
- Comments from `/api/comments/task/{id}`

---

## 📝 Seeding Behavior

### **When Does Seeding Occur?**
- ✅ On first run (empty database)
- ✅ After deleting and recreating the database
- ❌ Never on subsequent runs if data exists

### **Checking Seed Status:**
When you start the app, look for these console messages:
```
🌱 Seeding database with sample data...
✅ Created 4 users
✅ Created 10 tasks
✨ Database seeding complete!
```

Or if data exists:
```
⚠️  Database already contains data. Skipping seed.
```

---

## 🛠️ Customizing Sample Data

To modify the sample data, edit:
```
TaskManager.Api/Extensions/DatabaseSeeder.cs
```

Then recreate the database:
```bash
rm taskmanager.db
dotnet run
```

---

## 🗄️ Database Location

The SQLite database is stored at:
```
TaskManager.Api/taskmanager.db
```

You can view it with any SQLite browser:
- [DB Browser for SQLite](https://sqlitebrowser.org/)
- [SQLite Viewer (VS Code Extension)](https://marketplace.visualstudio.com/items?itemName=alexcvzz.vscode-sqlite)

---

## ⚠️ Troubleshooting

### **"Database already contains data"**
If you want to reseed:
```bash
rm taskmanager.db
dotnet run
```

### **"Cannot add users/tasks"**
Make sure:
1. The database exists and has the latest schema
2. The app is running (`dotnet run`)
3. Check for errors in the console output

### **Need to keep existing data?**
If you don't want auto-seeding, comment out this line in `Program.cs`:
```csharp
// dbContext.SeedDatabase();
```

---

## 📦 Complete Fresh Start

To completely reset and start fresh:

```bash
cd TaskManager.Api

# Remove database and backups
rm taskmanager.db*

# Clean and rebuild
dotnet clean
dotnet build

# Run application (auto-seeds)
dotnet run
```

---

## 🎉 Summary

**Automatic Features:**
- ✅ Database creation
- ✅ Schema migration (Comments table)
- ✅ Sample data seeding
- ✅ 4 users ready to use
- ✅ 10 tasks ready to use

**What You Need to Do:**
1. Delete old database: `rm taskmanager.db`
2. Run the app: `dotnet run`
3. Start using the API!

---

**Ready to go!** Your TaskManager API is fully set up with sample data. 🚀

