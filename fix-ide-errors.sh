#!/bin/bash

echo "🔧 Fixing IDE Errors (Cache Clear & Rebuild)"
echo "============================================="
echo ""

cd "$(dirname "$0")"

echo "Step 1: Cleaning all build artifacts..."
dotnet clean TaskManager.Api.sln > /dev/null 2>&1
rm -rf bin obj
rm -rf src/*/bin src/*/obj
echo "✅ Cleaned"

echo ""
echo "Step 2: Restoring NuGet packages..."
dotnet restore TaskManager.Api.sln > /dev/null 2>&1
echo "✅ Restored"

echo ""
echo "Step 3: Building solution..."
BUILD_OUTPUT=$(dotnet build TaskManager.Api.sln 2>&1)
BUILD_EXIT_CODE=$?

if [ $BUILD_EXIT_CODE -eq 0 ]; then
    echo "✅ Build succeeded!"
    echo ""
    echo "📊 Build Summary:"
    echo "$BUILD_OUTPUT" | grep "Build succeeded" | head -1
    echo "$BUILD_OUTPUT" | grep "Warning(s)" | head -1
    echo "$BUILD_OUTPUT" | grep "Error(s)" | head -1
    echo ""
    echo "✨ All components built successfully:"
    echo "$BUILD_OUTPUT" | grep "-> " | sed 's|/Users/robbie/Tab/TabnineTaskDemo/TaskManager.Api/|   - |g'
    echo ""
    echo "🎯 Next Steps:"
    echo ""
    echo "1. **Reload your IDE:**"
    echo "   VS Code: Cmd+Shift+P → 'Developer: Reload Window'"
    echo "   Visual Studio: Close and reopen"
    echo "   Rider: File → Invalidate Caches → Invalidate and Restart"
    echo ""
    echo "2. **Or just run the app:**"
    echo "   ./start-fresh.sh"
    echo ""
    echo "   The project builds successfully - IDE just needs to refresh!"
    echo ""
else
    echo "❌ Build failed!"
    echo ""
    echo "Error details:"
    echo "$BUILD_OUTPUT" | grep -i "error"
    echo ""
    echo "Full output:"
    echo "$BUILD_OUTPUT"
    exit 1
fi
