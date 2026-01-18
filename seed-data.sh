#!/bin/bash

echo "🌱 Seeding TaskManager Database"
echo "================================"
echo ""

API_URL="http://localhost:5000"

# Check if server is running
echo "🔍 Checking if API is running..."
if ! curl -s "$API_URL/api/tasks" > /dev/null 2>&1; then
    echo "❌ API is not running at $API_URL"
    echo "Please start the API first with: dotnet run"
    exit 1
fi

echo "✅ API is running"
echo ""

# Create Users
echo "👥 Creating 4 users..."
echo ""

USER1=$(curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Alice Johnson",
    "email": "alice.johnson@company.com",
    "department": "Engineering",
    "isActive": true
  }')
echo "✅ Created: Alice Johnson"

USER2=$(curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Bob Smith",
    "email": "bob.smith@company.com",
    "department": "Product",
    "isActive": true
  }')
echo "✅ Created: Bob Smith"

USER3=$(curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Carol Davis",
    "email": "carol.davis@company.com",
    "department": "Design",
    "isActive": true
  }')
echo "✅ Created: Carol Davis"

USER4=$(curl -s -X POST "$API_URL/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "David Wilson",
    "email": "david.wilson@company.com",
    "department": "Marketing",
    "isActive": true
  }')
echo "✅ Created: David Wilson"

echo ""
echo "📋 Creating 10 tasks..."
echo ""

# Task 1
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Implement user authentication",
    "description": "Add JWT-based authentication to the API",
    "priority": 2,
    "assigneeId": 1,
    "dueDate": "2025-02-15T17:00:00Z",
    "category": "Backend",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 1: Implement user authentication"

# Task 2
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Design landing page mockup",
    "description": "Create high-fidelity mockups for the new landing page",
    "priority": 1,
    "assigneeId": 3,
    "dueDate": "2025-01-25T17:00:00Z",
    "category": "Design",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 2: Design landing page mockup"

# Task 3
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Write API documentation",
    "description": "Document all endpoints with examples and request/response schemas",
    "priority": 1,
    "assigneeId": 1,
    "dueDate": "2025-01-30T17:00:00Z",
    "category": "Documentation",
    "isCompleted": true
  }' > /dev/null
echo "✅ Task 3: Write API documentation"

# Task 4
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Set up CI/CD pipeline",
    "description": "Configure GitHub Actions for automated testing and deployment",
    "priority": 2,
    "assigneeId": 1,
    "dueDate": "2025-02-01T17:00:00Z",
    "category": "DevOps",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 4: Set up CI/CD pipeline"

# Task 5
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Create marketing campaign",
    "description": "Plan Q1 2025 marketing campaign for product launch",
    "priority": 1,
    "assigneeId": 4,
    "dueDate": "2025-02-10T17:00:00Z",
    "category": "Marketing",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 5: Create marketing campaign"

# Task 6
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Implement dark mode",
    "description": "Add dark mode theme support to the application",
    "priority": 0,
    "assigneeId": 3,
    "dueDate": "2025-03-01T17:00:00Z",
    "category": "Frontend",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 6: Implement dark mode"

# Task 7
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Fix mobile responsiveness",
    "description": "Resolve layout issues on mobile devices",
    "priority": 2,
    "assigneeId": 3,
    "dueDate": "2025-01-28T17:00:00Z",
    "category": "Frontend",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 7: Fix mobile responsiveness"

# Task 8
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Database optimization",
    "description": "Add indexes and optimize slow queries",
    "priority": 1,
    "assigneeId": 1,
    "dueDate": "2025-02-05T17:00:00Z",
    "category": "Backend",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 8: Database optimization"

# Task 9
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "User research interviews",
    "description": "Conduct 10 user interviews for feature validation",
    "priority": 1,
    "assigneeId": 2,
    "dueDate": "2025-01-27T17:00:00Z",
    "category": "Research",
    "isCompleted": true
  }' > /dev/null
echo "✅ Task 9: User research interviews"

# Task 10
curl -s -X POST "$API_URL/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Prepare product demo",
    "description": "Create demo script and slides for stakeholder presentation",
    "priority": 2,
    "assigneeId": 2,
    "dueDate": "2025-01-26T17:00:00Z",
    "category": "Product",
    "isCompleted": false
  }' > /dev/null
echo "✅ Task 10: Prepare product demo"

echo ""
echo "✨ Seeding complete!"
echo ""
echo "📊 Summary:"
echo "   - 4 users created"
echo "   - 10 tasks created"
echo ""
echo "🔍 View the data:"
echo "   Users:  curl http://localhost:5000/api/users"
echo "   Tasks:  curl http://localhost:5000/api/tasks"
echo "   Swagger: http://localhost:5000/swagger"
echo ""
