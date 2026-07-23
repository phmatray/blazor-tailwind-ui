#!/bin/bash
set -e

echo "=== Testing End-to-End Workflow ==="
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo

# Create fresh app
rm -rf demo-e2e
dotnet new blazor -n demo-e2e -o demo-e2e
cd demo-e2e

# Setup Tailwind
tailwind-blazor setup

# Add Tailwind classes to Home.razor
cat > Components/Pages/Home.razor <<'EOF'
@page "/"
<PageTitle>Home</PageTitle>

<div class="container mx-auto p-8">
    <h1 class="text-4xl font-bold text-blue-600 mb-4">
        Welcome to Tailwind + Blazor!
    </h1>
    <div class="bg-gray-100 rounded-lg p-6 shadow-md">
        <p class="text-lg text-gray-700">Tailwind CSS is working!</p>
        <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            Click Me
        </button>
    </div>
</div>
EOF

# Validate
tailwind-blazor check

# Build
dotnet build

# Verify Tailwind classes in compiled CSS
grep -q "text-4xl" wwwroot/css/app.css && echo "✓ Tailwind classes compiled"
grep -q "container" wwwroot/css/app.css && echo "✓ Container utility compiled"
grep -q "bg-blue-500" wwwroot/css/app.css && echo "✓ Color utilities compiled"

# Test run (background for 10 seconds)
dotnet run &
APP_PID=$!
sleep 10

if ps -p $APP_PID > /dev/null; then
    echo "✓ Application started"
    kill $APP_PID
else
    echo "✗ App failed to start"
    exit 1
fi

# Test clean build (verify MSBuild integration)
dotnet clean
rm -f wwwroot/css/app.css
dotnet build
test -f wwwroot/css/app.css && echo "✓ MSBuild integration works"

echo "✓ End-to-end test PASSED"
