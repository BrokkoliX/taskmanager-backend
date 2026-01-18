#!/bin/bash

echo "🔍 Verifying TaskManager API Build"
echo "===================================="
echo ""

cd "$(dirname "$0")"

echo "📁 Checking Comment-related files..."
echo ""

FILES=(
    "src/TaskManager.Core/Entities/Comment.cs"
    "src/TaskManager.Core/DTOs/CommentDto.cs"
    "src/TaskManager.Core/DTOs/CreateCommentDto.cs"
    "src/TaskManager.Core/DTOs/UpdateCommentDto.cs"
    "src/TaskManager.Core/Interfaces/Repositories/ICommentRepository.cs"
    "src/TaskManager.Core/Interfaces/Services/ICommentService.cs"
    "src/TaskManager.Infrastructure/Data/CommentRepository.cs"
    "src/TaskManager.Application/Services/CommentService.cs"
    "Controllers/CommentsController.cs"
    "Extensions/DatabaseSeeder.cs"
)

ALL_EXIST=true
for file in "${FILES[@]}"; do
    if [ -f "$file" ]; then
        echo "✅ $file"
    else
        echo "❌ $file (MISSING)"
        ALL_EXIST=false
    fi
done

echo ""

if [ "$ALL_EXIST" = false ]; then
    echo "⚠️  Some files are missing!"
    exit 1
fi

echo "✅ All Comment-related files exist"
echo ""
echo "🔧 Cleaning project..."
dotnet clean TaskManager.Api.csproj > /dev/null 2>&1

echo "🔨 Building project..."
BUILD_OUTPUT=$(dotnet build TaskManager.Api.csproj 2>&1)
BUILD_EXIT_CODE=$?

if [ $BUILD_EXIT_CODE -eq 0 ]; then
    echo "✅ Build succeeded!"
    echo ""
    echo "📊 Build Summary:"
    echo "$BUILD_OUTPUT" | grep "succeeded" | grep -v "^$"
    echo ""
    echo "✨ Project is ready to run!"
    echo ""
    echo "🚀 To start the application:"
    echo "   ./start-fresh.sh"
    echo ""
    echo "Or manually:"
    echo "   rm taskmanager.db"
    echo "   dotnet run"
else
    echo "❌ Build failed!"
    echo ""
    echo "Error details:"
    echo "$BUILD_OUTPUT" | grep -i "error"
    exit 1
fi
