#!/bin/bash

echo "🚀 Setting up Comments Feature for TaskManager API"
echo "=================================================="
echo ""

# Check if database exists
if [ -f "taskmanager.db" ]; then
    echo "📦 Old database found: taskmanager.db"
    echo "⚠️  The database needs to be recreated to add the Comments table."
    echo ""
    read -p "Do you want to delete the old database? (y/n) " -n 1 -r
    echo ""
    
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        # Backup old database
        BACKUP_NAME="taskmanager.db.backup.$(date +%Y%m%d_%H%M%S)"
        cp taskmanager.db "$BACKUP_NAME"
        echo "✅ Backup created: $BACKUP_NAME"
        
        # Remove old database
        rm taskmanager.db
        echo "✅ Old database removed"
    else
        echo "❌ Setup cancelled. Database not modified."
        exit 1
    fi
else
    echo "✅ No existing database found. Will create new one."
fi

echo ""
echo "🔧 Building the project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "❌ Build failed. Please fix errors and try again."
    exit 1
fi

echo "✅ Build successful"
echo ""
echo "🎯 Starting the application..."
echo "   The database will be created automatically with the Comments table."
echo ""
echo "📝 Once started:"
echo "   - API: http://localhost:5000"
echo "   - Swagger: http://localhost:5000/swagger"
echo ""
echo "💬 Comment Endpoints:"
echo "   GET    /api/comments"
echo "   GET    /api/comments/task/{taskId}"
echo "   GET    /api/comments/{id}"
echo "   POST   /api/comments"
echo "   PUT    /api/comments/{id}"
echo "   DELETE /api/comments/{id}"
echo ""
echo "Press Ctrl+C to stop the server"
echo ""

dotnet run
