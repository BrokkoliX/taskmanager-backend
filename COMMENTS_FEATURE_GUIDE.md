# 💬 Comments Feature Guide

**Feature Added:** January 18, 2025  
**Status:** ✅ **READY TO USE**

---

## 🎯 Overview

The Comments feature allows users to add, view, update, and delete comments on tasks. This enhances collaboration and tracking of task discussions.

### **Key Features:**
✅ Add comments to any task  
✅ View all comments for a specific task  
✅ Edit existing comments  
✅ Delete comments  
✅ Track comment authors and timestamps  
✅ Automatic cascade delete (comments deleted when task is deleted)

---

## 📐 Architecture

### **Entities Created:**

**Comment Entity** (`TaskManager.Core/Entities/Comment.cs`)
```csharp
public class Comment
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public int TaskItemId { get; set; }
    public TaskItem? TaskItem { get; set; }
    public int? CreatedByUserId { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

### **Relationships:**
- **Comment → TaskItem:** Many-to-One (cascade delete)
- **Comment → User:** Many-to-One (set null on delete)
- **TaskItem → Comments:** One-to-Many
- **User → Comments:** One-to-Many

---

## 🔌 API Endpoints

### **Base URL:** `/api/comments`

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| `GET` | `/api/comments` | Get all comments | - |
| `GET` | `/api/comments/task/{taskId}` | Get comments for a task | - |
| `GET` | `/api/comments/{id}` | Get comment by ID | - |
| `POST` | `/api/comments` | Create new comment | `CreateCommentDto` |
| `PUT` | `/api/comments/{id}` | Update comment | `UpdateCommentDto` |
| `DELETE` | `/api/comments/{id}` | Delete comment | - |

---

## 📝 DTOs

### **CreateCommentDto**
```json
{
  "content": "This is a comment on the task",
  "taskItemId": 1,
  "createdByUserId": 2
}
```

### **UpdateCommentDto**
```json
{
  "content": "Updated comment content"
}
```

### **CommentDto** (Response)
```json
{
  "id": 1,
  "content": "This is a comment on the task",
  "taskItemId": 1,
  "createdByUserId": 2,
  "createdByUserName": "John Doe",
  "createdAt": "2025-01-18T10:30:00Z",
  "updatedAt": null
}
```

---

## 🚀 Quick Start

### **1. Reset Database**

Since we added a new entity, you need to recreate the database:

```bash
cd TaskManager.Api

# Remove old database
rm taskmanager.db

# Run the application (database will be created automatically)
dotnet run
```

### **2. Test the API**

#### **Create a Comment**
```bash
curl -X POST http://localhost:5000/api/comments \
  -H "Content-Type: application/json" \
  -d '{
    "content": "Great work on this task!",
    "taskItemId": 1,
    "createdByUserId": 1
  }'
```

#### **Get Comments for a Task**
```bash
curl http://localhost:5000/api/comments/task/1
```

#### **Update a Comment**
```bash
curl -X PUT http://localhost:5000/api/comments/1 \
  -H "Content-Type: application/json" \
  -d '{
    "content": "Updated comment text"
  }'
```

#### **Delete a Comment**
```bash
curl -X DELETE http://localhost:5000/api/comments/1
```

---

## 🧪 Testing Examples

### **Using PowerShell (Windows)**

```powershell
# Create a comment
Invoke-RestMethod -Uri "http://localhost:5000/api/comments" -Method Post -ContentType "application/json" -Body '{
  "content": "This task needs review",
  "taskItemId": 1,
  "createdByUserId": 1
}'

# Get comments for task
Invoke-RestMethod -Uri "http://localhost:5000/api/comments/task/1" -Method Get
```

### **Using JavaScript (Frontend)**

```javascript
// Create a comment
async function addComment(taskId, content, userId) {
  const response = await fetch('http://localhost:5000/api/comments', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      content: content,
      taskItemId: taskId,
      createdByUserId: userId
    })
  });
  return await response.json();
}

// Get comments for a task
async function getTaskComments(taskId) {
  const response = await fetch(`http://localhost:5000/api/comments/task/${taskId}`);
  return await response.json();
}

// Update a comment
async function updateComment(commentId, newContent) {
  const response = await fetch(`http://localhost:5000/api/comments/${commentId}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ content: newContent })
  });
  return await response.json();
}

// Delete a comment
async function deleteComment(commentId) {
  await fetch(`http://localhost:5000/api/comments/${commentId}`, {
    method: 'DELETE'
  });
}
```

---

## 🗄️ Database Schema

### **Comments Table**

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | INTEGER | PRIMARY KEY | Auto-increment ID |
| `Content` | TEXT | REQUIRED, MAX 1000 | Comment text |
| `TaskItemId` | INTEGER | FOREIGN KEY, REQUIRED | Reference to Tasks |
| `CreatedByUserId` | INTEGER | FOREIGN KEY, NULLABLE | Reference to Users |
| `CreatedAt` | DATETIME | REQUIRED | Creation timestamp |
| `UpdatedAt` | DATETIME | NULLABLE | Last update timestamp |

### **Relationships:**
- `TaskItemId` → `Tasks.Id` (CASCADE DELETE)
- `CreatedByUserId` → `Users.Id` (SET NULL)

---

## 🔍 Validation Rules

### **Content Field:**
- ✅ Required
- ✅ Maximum 1000 characters
- ❌ Cannot be empty or whitespace

### **TaskItemId:**
- ✅ Required
- ✅ Must reference existing task
- ❌ Validation error if task doesn't exist

### **CreatedByUserId:**
- ⚠️ Optional (can be null for anonymous comments)
- ✅ Must reference existing user if provided

---

## 🎨 Frontend Integration

### **Add Comments Section to Task Detail View**

```html
<!-- Comments Section -->
<div class="comments-section">
  <h3>Comments</h3>
  
  <!-- Comment List -->
  <div id="comments-list"></div>
  
  <!-- Add Comment Form -->
  <form id="add-comment-form">
    <textarea 
      id="comment-content" 
      placeholder="Add a comment..." 
      maxlength="1000"
      required>
    </textarea>
    <button type="submit">Add Comment</button>
  </form>
</div>

<script>
// Load comments for task
async function loadComments(taskId) {
  const comments = await fetch(`/api/comments/task/${taskId}`).then(r => r.json());
  const commentsList = document.getElementById('comments-list');
  
  commentsList.innerHTML = comments.map(comment => `
    <div class="comment" data-id="${comment.id}">
      <div class="comment-header">
        <strong>${comment.createdByUserName || 'Anonymous'}</strong>
        <span>${new Date(comment.createdAt).toLocaleString()}</span>
      </div>
      <div class="comment-content">${comment.content}</div>
      <div class="comment-actions">
        <button onclick="editComment(${comment.id})">Edit</button>
        <button onclick="deleteComment(${comment.id})">Delete</button>
      </div>
    </div>
  `).join('');
}

// Handle comment submission
document.getElementById('add-comment-form').addEventListener('submit', async (e) => {
  e.preventDefault();
  const content = document.getElementById('comment-content').value;
  const taskId = getCurrentTaskId(); // Your function to get current task ID
  const userId = getCurrentUserId(); // Your function to get current user ID
  
  await fetch('/api/comments', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ content, taskItemId: taskId, createdByUserId: userId })
  });
  
  document.getElementById('comment-content').value = '';
  loadComments(taskId);
});
</script>
```

---

## 📦 Files Created

### **Core Layer:**
- `src/TaskManager.Core/Entities/Comment.cs`
- `src/TaskManager.Core/DTOs/CommentDto.cs`
- `src/TaskManager.Core/DTOs/CreateCommentDto.cs`
- `src/TaskManager.Core/DTOs/UpdateCommentDto.cs`
- `src/TaskManager.Core/Interfaces/Repositories/ICommentRepository.cs`
- `src/TaskManager.Core/Interfaces/Services/ICommentService.cs`

### **Infrastructure Layer:**
- `src/TaskManager.Infrastructure/Data/CommentRepository.cs`

### **Application Layer:**
- `src/TaskManager.Application/Services/CommentService.cs`

### **API Layer:**
- `Controllers/CommentsController.cs`

### **Modified Files:**
- `src/TaskManager.Core/Entities/TaskItem.cs` - Added Comments collection
- `src/TaskManager.Core/Entities/User.cs` - Added Comments collection
- `src/TaskManager.Infrastructure/Data/TaskDbContext.cs` - Added Comment DbSet and configuration
- `Program.cs` - Registered Comment services

---

## 🚨 Important Notes

### **Database Migration:**
Since Entity Framework is using `EnsureCreated()`, you need to:
1. **Delete** the existing `taskmanager.db` file
2. **Restart** the application
3. The database will be recreated with the Comments table

**⚠️ Warning:** This will delete all existing data. In production, use proper EF migrations.

### **Cascade Deletion:**
- When a **Task** is deleted, all its **Comments** are automatically deleted
- When a **User** is deleted, their comments remain but `CreatedByUserId` is set to `null`

---

## 🔄 Swagger Documentation

After starting the application, visit:
**http://localhost:5000/swagger**

You'll see the new `/api/comments` endpoints with interactive documentation.

---

## 🧪 Unit Testing

### **Example Test Cases:**

```csharp
[Fact]
public async Task CreateComment_ShouldReturnComment()
{
    // Arrange
    var createDto = new CreateCommentDto 
    { 
        Content = "Test comment",
        TaskItemId = 1,
        CreatedByUserId = 1
    };
    
    // Act
    var result = await _commentService.CreateAsync(createDto);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal("Test comment", result.Content);
}

[Fact]
public async Task GetByTaskId_ShouldReturnCommentsForTask()
{
    // Arrange
    var taskId = 1;
    
    // Act
    var result = await _commentService.GetByTaskIdAsync(taskId);
    
    // Assert
    Assert.NotNull(result);
    Assert.All(result, c => Assert.Equal(taskId, c.TaskItemId));
}
```

---

## 🎯 Next Steps

### **Recommended Enhancements:**

1. **Add User Authentication** - Ensure users can only edit/delete their own comments
2. **Add Pagination** - For tasks with many comments
3. **Add Rich Text** - Support markdown or HTML in comments
4. **Add Reactions** - Like/emoji reactions to comments
5. **Add Mentions** - @mention other users
6. **Add Attachments** - Allow file uploads with comments
7. **Add Real-time Updates** - Using SignalR for live comment updates
8. **Add Comment Threading** - Reply to comments

---

## 📞 Support

For issues or questions:
- Check Swagger UI for API documentation
- Review the code in the created files
- Test endpoints using curl or Postman

---

## ✅ Checklist

- [x] Comment entity created
- [x] DTOs created
- [x] Repository interface and implementation created
- [x] Service interface and implementation created
- [x] Controller created
- [x] DbContext updated
- [x] Services registered in Program.cs
- [x] Relationships configured
- [ ] Database recreated
- [ ] API tested
- [ ] Frontend integrated

---

**Status:** Comments feature is ready to use! 🎉  
**Next:** Delete `taskmanager.db` and run `dotnet run` to recreate the database.

