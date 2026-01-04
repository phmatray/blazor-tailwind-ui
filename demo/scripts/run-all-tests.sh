#!/bin/bash

echo "========================================"
echo "TailwindToolbox Test Suite"
echo "========================================"

RESULTS_FILE="/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/test-results.md"

cat > "$RESULTS_FILE" <<EOF
# TailwindToolbox Test Results

**Date:** $(date)
**Tool Version:** $(tailwind-blazor --version)
**Platform:** $(uname -s) / .NET $(dotnet --version)

EOF

run_test() {
    echo "Running: $1"
    if bash "$2" >> "$RESULTS_FILE" 2>&1; then
        echo "✓ $1 PASSED"
        echo "## $1: ✓ PASSED" >> "$RESULTS_FILE"
    else
        echo "✗ $1 FAILED"
        echo "## $1: ✗ FAILED" >> "$RESULTS_FILE"
    fi
    echo "" >> "$RESULTS_FILE"
}

cd /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo

run_test "Setup Command" "scripts/test-setup.sh"
run_test "Check Command" "scripts/test-check.sh"
run_test "Update Command" "scripts/test-update.sh"
run_test "Create-Target Command" "scripts/test-create-target.sh"
run_test "End-to-End Workflow" "scripts/test-e2e.sh"

echo ""
echo "Test results saved to: $RESULTS_FILE"
