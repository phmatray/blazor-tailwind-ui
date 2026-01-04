# TailwindToolbox Demo

Comprehensive demos and tests for TailwindToolbox CLI.

## Prerequisites

- .NET 10.0 SDK
- Node.js 16+ and npm
- TailwindToolbox installed globally

## Quick Start

### Install Tool

```bash
cd /Users/phmatray/Repositories/github-phm/TailwindToolbox
dotnet pack -c Release TailwindToolbox/TailwindToolbox.csproj -o ./nupkg
dotnet tool install -g TailwindToolbox --add-source ./nupkg --version 1.0.0
tailwind-blazor --version
```

### Run All Tests

```bash
cd demo
chmod +x scripts/*.sh
./scripts/run-all-tests.sh
```

### Manual Testing

```bash
cd demo/demo-blazor-server
tailwind-blazor setup
dotnet build
dotnet run
# Visit http://localhost:5000
```

## Test Scripts

| Script | Purpose |
|--------|---------|
| `test-setup.sh` | Test setup command |
| `test-check.sh` | Test validation |
| `test-update.sh` | Test updates |
| `test-create-target.sh` | Test target generation |
| `test-e2e.sh` | End-to-end workflow |
| `run-all-tests.sh` | Run all tests |

## Expected Results

All tests should pass:
- ✓ Setup creates all files
- ✓ Check validates 17 rules
- ✓ Update detects changes
- ✓ Create-target generates XML
- ✓ App builds and runs
- ✓ Tailwind CSS compiles

## Demo Projects

After running tests, the following demo projects will be created:

- `demo-blazor-server/` - Blazor Server app with Tailwind CSS
- `demo-custom-target/` - Custom MSBuild target configuration
- `demo-e2e/` - End-to-end workflow demonstration

## Troubleshooting

### Permission Denied

If you get permission errors when running scripts:

```bash
chmod +x scripts/*.sh
```

### Tool Not Found

If `tailwind-blazor` command is not found:

```bash
dotnet tool list -g
# If not listed, reinstall:
dotnet tool install -g TailwindToolbox --add-source ../nupkg --version 1.0.0
```

### Build Errors

If you encounter build errors in demo projects:

```bash
# Clean and rebuild
cd demo-blazor-server
dotnet clean
dotnet build --verbosity detailed
```
