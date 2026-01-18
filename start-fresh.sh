#!/bin/bash

echo "🚀 TaskManager API - Fresh Start with Sample Data"
echo "=================================================="
echo ""

# Navigate to API directory
cd "$(dirname "$0")"

# Check if database exists
if [ -f "taskmanager.db" ]; then
    echo "📦 Existing database found"
    echo ""
    read -p "Do you want to recreate the database? This will delete all existing data (y/n): " -n 1 -r
    echo ""
    
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        # Create backup
        BACKUP_NAME="taskmanager.db.backup.$(date +%Y%m%d_%H%M%S)"
        cp taskmanager.db "$BACKUP_NAME"
        echo "✅ Backup created: $BACKUP_NAME"
        
        # Remove old database
        rm taskmanager.db
        echo "✅ Old database removed"
    else
        echo "❌ Operation cancelled. Keeping existing database."
        echo ""
        read -p "Do you want to start the API anyway? (y/n): " -n 1 -r
        echo ""
        
        if [[ ! $REPLY =~ ^[Yy]$ ]]; then
            echo "Exiting..."
            exit 0
        fi
    fi
else
    echo "✅ No existing database found. Will create new one."
fi

echo ""
echo "🔧 Building the project..."
dotnet build TaskManager.Api.sln

if [ $? -ne 0 ]; then
    echo "❌ Build failed. Please fix errors and try again."
    exit 1
fi

echo "✅ Build successful"
echo ""
echo "=================================================="
echo "🎯 Starting TaskManager API..."
echo "=================================================="
echo ""
echo "📝 The application will:"
echo "   1. Create the database with Comments table"
echo "   2. Automatically seed 4 sample users"
echo "   3. Automatically seed 10 sample tasks"
echo ""
echo "🌐 API will be available at:"
echo "   - API: http://localhost:5000"
echo "   - Swagger: http://localhost:5000/swagger"
echo ""
echo "📚 API Endpoints:"
echo "   - GET/POST    /api/users"
echo "   - GET/POST    /api/tasks"
echo "   - GET/POST    /api/comments"
echo ""
echo "Press Ctrl+C to stop the server"
echo "=================================================="
echo ""

# Run the application
dotnet run --project TaskManager.Api.csproj --no-build
