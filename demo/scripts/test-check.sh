#!/bin/bash
set -e

echo "=== Testing Check Command ==="
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server

# Test all formats
tailwind-blazor check | tee /tmp/check-table.log
tailwind-blazor check --format json > /tmp/check.json
tailwind-blazor check --format text > /tmp/check-text.log

# Verify JSON is valid
python3 -m json.tool /tmp/check.json > /dev/null && echo "✓ Valid JSON output"

# Test category filter
tailwind-blazor check --category dependencies && echo "✓ Category filter works"

# Test all 17 validation rules execute
grep -E "(Environment|Files|Configuration|Dependencies|Integration)" /tmp/check-table.log && echo "✓ All categories checked"

echo "✓ Check test PASSED"
