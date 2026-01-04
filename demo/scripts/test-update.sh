#!/bin/bash
set -e

echo "=== Testing Update Command ==="
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server

# Test dry-run
tailwind-blazor update --dry-run | tee /tmp/update-dryrun.log
echo "✓ Dry-run works"

# Test package filter
tailwind-blazor update --package tailwindcss --dry-run && echo "✓ Package filter works"

# Test skip breaking
tailwind-blazor update --skip-breaking --dry-run && echo "✓ Skip breaking works"

echo "✓ Update test PASSED"
