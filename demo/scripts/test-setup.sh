#!/bin/bash
set -e

echo "=== Testing Setup Command ==="
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo

# Create fresh Blazor app
rm -rf demo-blazor-server
dotnet new blazor -n demo-blazor-server -o demo-blazor-server
cd demo-blazor-server

# Run setup
tailwind-blazor setup

# Verify files created
test -f tailwind.config.js && echo "✓ tailwind.config.js"
test -f package.json && echo "✓ package.json"
test -f Styles/app.css && echo "✓ Styles/app.css"
test -f TailwindBuild.targets && echo "✓ TailwindBuild.targets"
test -d node_modules && echo "✓ npm packages installed"

# Verify .csproj modified
grep -q "TailwindBuild.targets" demo-blazor-server.csproj && echo "✓ MSBuild import added"

# Build project (triggers Tailwind compilation)
dotnet build
test -f wwwroot/css/app.css && echo "✓ CSS compiled"

echo "✓ Setup test PASSED"
