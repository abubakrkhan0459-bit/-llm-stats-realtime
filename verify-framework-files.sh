#!/bin/bash
# Pre-deployment verification script for LLM Stats Realtime
# This script ensures all framework files exist before deployment
# Run this before: firebase deploy

set -e  # Exit on error

echo "🔍 Pre-deployment Framework File Verification"
echo "============================================"

FRAMEWORK_DIR="publish/wwwroot/_framework"
REQUIRED_FILES=(
    "blazor.webassembly.66stpp682q.js"
    "dotnet.runtime.2tx45g8lli.js"
)

# Check if publish directory exists
if [ ! -d "$FRAMEWORK_DIR" ]; then
    echo "❌ ERROR: Framework directory not found at $FRAMEWORK_DIR"
    echo "Please run: dotnet publish first"
    exit 1
fi

echo "✓ Framework directory found"

# Check each required file
ALL_GOOD=true
for file in "${REQUIRED_FILES[@]}"; do
    if [ -f "$FRAMEWORK_DIR/$file" ]; then
        size=$(stat -f%z "$FRAMEWORK_DIR/$file" 2>/dev/null || stat -c%s "$FRAMEWORK_DIR/$file" 2>/dev/null)
        echo "✓ $file exists ($size bytes)"
        
        # Verify it's not an HTML file (404 page) by checking first character
        first_char=$(head -c 1 "$FRAMEWORK_DIR/$file")
        if [ "$first_char" = "<" ]; then
            echo "❌ ERROR: $file appears to be HTML (404 page) instead of JavaScript!"
            ALL_GOOD=false
        fi
    else
        echo "❌ ERROR: $file not found!"
        ALL_GOOD=false
    fi
done

# Check for non-versioned files (legacy support)
echo ""
echo "🔧 Checking non-versioned file copies..."
NON_VERSIONED=(
    "blazor.webassembly.js"
    "dotnet.js"
    "dotnet.runtime.js"
)

for file in "${NON_VERSIONED[@]}"; do
    if [ -f "$FRAMEWORK_DIR/$file" ]; then
        echo "✓ $file exists (for legacy support)"
    else
        echo "⚠ $file missing (this is OK if using versioned files)"
    fi
done

# Create non-versioned copies as backup (optional)
echo ""
echo "📦 Creating backup file copies..."
cd "$FRAMEWORK_DIR"

# Create blazor.webassembly.js if it doesn't exist
if [ ! -f "blazor.webassembly.js" ] && [ -f "blazor.webassembly.66stpp682q.js" ]; then
    cp "blazor.webassembly.66stpp682q.js" "blazor.webassembly.js"
    echo "✓ Created blazor.webassembly.js backup"
fi

# Create dotnet.runtime.js if it doesn't exist
if [ ! -f "dotnet.runtime.js" ] && [ -f "dotnet.runtime.2tx45g8lli.js" ]; then
    cp "dotnet.runtime.2tx45g8lli.js" "dotnet.runtime.js"
    echo "✓ Created dotnet.runtime.js backup"
fi

# Create dotnet.js if it doesn't exist (use latest dotnet.*.js)
if [ ! -f "dotnet.js" ]; then
    # Find the newest dotnet.*.js file (excluding runtime and native)
    latest_dotnet=$(ls -t dotnet.*.js 2>/dev/null | grep -v runtime | grep -v native | head -1)
    if [ -n "$latest_dotnet" ]; then
        cp "$latest_dotnet" "dotnet.js"
        echo "✓ Created dotnet.js from $latest_dotnet"
    fi
fi

cd - > /dev/null

echo ""
if [ "$ALL_GOOD" = true ]; then
    echo "✅ All checks passed! Ready for deployment."
    echo "Run: firebase deploy"
    exit 0
else
    echo "❌ Some checks failed. Please fix the issues above."
    exit 1
fi
