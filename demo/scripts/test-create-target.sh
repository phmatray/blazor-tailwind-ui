#!/bin/bash
set -e

echo "=== Testing Create-Target Command ==="
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo

# Test with custom paths
rm -rf demo-custom-target
dotnet new blazor -n demo-custom-target -o demo-custom-target
cd demo-custom-target

mkdir -p CustomStyles
echo "@tailwind base; @tailwind components; @tailwind utilities;" > CustomStyles/main.css

tailwind-blazor create-target \
  --target-name CompileCustomCSS \
  --input-css CustomStyles/main.css \
  --output-css wwwroot/styles/output.css

test -f TailwindBuild.targets && echo "✓ .targets created"
grep -q "CompileCustomCSS" TailwindBuild.targets && echo "✓ Custom target name"
grep -q "CustomStyles/main.css" TailwindBuild.targets && echo "✓ Custom input path"

echo "✓ Create-target test PASSED"
