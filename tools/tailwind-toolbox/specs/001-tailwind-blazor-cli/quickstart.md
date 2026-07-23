# Quickstart Guide: Tailwind Blazor CLI

**Feature**: 001-tailwind-blazor-cli
**Audience**: Developers using Blazor who want to add Tailwind CSS
**Time to Complete**: 2-5 minutes

## What is Tailwind Blazor CLI?

A command-line tool that automates Tailwind CSS setup, configuration, and management for Blazor projects. Eliminates the manual, error-prone process of configuring Tailwind with MSBuild.

**Benefits**:
- ✅ One-command setup (no manual configuration files)
- ✅ Automatic validation (catches configuration issues instantly)
- ✅ Safe dependency updates (warns about breaking changes)
- ✅ Works with Blazor Server, WebAssembly, and Hybrid

---

## Prerequisites

Before using Tailwind Blazor CLI, ensure you have:

1. **.NET 6.0 or higher** installed
   ```bash
   dotnet --version
   # Should show: 6.0.x, 7.0.x, 8.0.x, or 10.0.x
   ```

2. **Node.js 16+ and npm** installed
   ```bash
   node --version  # Should show: v16.x or higher
   npm --version   # Should show: 8.x or higher
   ```

   If not installed, download from [nodejs.org](https://nodejs.org/)

3. **A Blazor project** (existing or new)
   ```bash
   # Create a new Blazor project if needed:
   dotnet new blazor -n MyBlazorApp
   cd MyBlazorApp
   ```

---

## Installation

### Option 1: macOS Installation Script (Recommended)

If you have the TailwindToolbox repository:

```bash
# Clone the repository
git clone https://github.com/yourusername/TailwindToolbox.git
cd TailwindToolbox

# Run installation script
./scripts/install-tool.sh

# Verify installation
tailwind-blazor --version
```

### Option 2: Manual Build and Install

```bash
# Build the project
dotnet build -c Release TailwindToolbox/TailwindToolbox.csproj

# Copy to your PATH
sudo cp TailwindToolbox/bin/Release/net10.0/TailwindToolbox /usr/local/bin/tailwind-blazor

# Make executable
chmod +x /usr/local/bin/tailwind-blazor

# Verify installation
tailwind-blazor --version
```

### Option 3: Run from Source

```bash
# No installation needed, run directly:
dotnet run --project TailwindToolbox/TailwindToolbox.csproj -- setup
```

---

## Quick Start: Setup Tailwind in 3 Steps

### Step 1: Navigate to Your Blazor Project

```bash
cd /path/to/your/BlazorProject
```

### Step 2: Run Setup Command

```bash
tailwind-blazor setup
```

**What happens**:
- ✅ Detects your Blazor project type (Server/WebAssembly/Hybrid)
- ✅ Installs Tailwind CSS npm packages
- ✅ Creates `tailwind.config.js` with correct content paths for `.razor` files
- ✅ Creates `package.json` with Tailwind build scripts
- ✅ Creates `Styles/app.css` with Tailwind directives
- ✅ Generates `TailwindBuild.targets` for MSBuild integration
- ✅ Updates `.gitignore` to exclude `node_modules`

**Output**:
```
Tailwind Blazor Setup
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Blazor project detected: MyBlazorApp (Blazor WebAssembly, net10.0)
✓ Node.js v20.10.0 detected
✓ npm v10.2.0 detected

Installing Tailwind CSS packages...
✓ tailwindcss@4.0.0 installed
✓ autoprefixer@10.4.16 installed

Creating configuration files...
✓ tailwind.config.js created
✓ package.json created
✓ Styles/app.css created

Setting up build integration...
✓ TailwindBuild.targets created
✓ MyBlazorApp.csproj updated

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Setup complete! Run 'dotnet build' to compile Tailwind CSS.
```

### Step 3: Build and Test

```bash
# Build your project (Tailwind CSS compiles automatically)
dotnet build

# Run your project
dotnet run
```

**Add Tailwind classes to your components**:

```razor
<!-- Pages/Index.razor -->
<div class="container mx-auto p-4">
    <h1 class="text-3xl font-bold text-blue-600">
        Hello, Tailwind!
    </h1>
    <p class="mt-4 text-gray-700">
        Tailwind CSS is now working in your Blazor project!
    </p>
</div>
```

Navigate to your app in the browser - Tailwind styles should be applied! 🎉

---

## Common Tasks

### Validate Your Configuration

Check that everything is set up correctly:

```bash
tailwind-blazor check
```

**Example output**:
```
Tailwind Configuration Validation
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Node.js installed (v20.10.0)
✓ npm installed (v10.2.0)
✓ tailwind.config.js exists
✓ package.json exists
✓ Tailwind CSS 4.0.0 installed
✓ MSBuild targets configured

Summary: All checks passed!
```

If issues are found, you'll see specific errors with remediation steps.

---

### Update Tailwind CSS

Keep your Tailwind packages up to date:

```bash
# Check for available updates
tailwind-blazor update --dry-run

# Apply updates
tailwind-blazor update
```

**Example output**:
```
Available updates:
┌─────────────────┬─────────┬─────────┬──────────────┐
│ Package         │ Current │ Latest  │ Change Type  │
├─────────────────┼─────────┼─────────┼──────────────┤
│ tailwindcss     │ 4.0.0   │ 4.0.5   │ Patch        │
└─────────────────┴─────────┴─────────┴──────────────┘

Updating...
✓ Update complete!
```

The tool warns you about breaking changes and lets you review migration guides before updating.

---

### Regenerate Build Targets

If you need to customize or regenerate your MSBuild targets:

```bash
# Create with custom paths
tailwind-blazor create-target \
  --input-css Styles/main.css \
  --output-css wwwroot/css/main.css

# Preview without writing
tailwind-blazor create-target --dry-run

# Force overwrite existing file
tailwind-blazor create-target --force
```

---

## Project Structure After Setup

```
MyBlazorApp/
├── MyBlazorApp.csproj              # Updated with MSBuild import
├── tailwind.config.js              # Tailwind configuration
├── package.json                    # npm dependencies
├── package-lock.json               # npm lock file (generated)
├── node_modules/                   # npm packages (gitignored)
├── TailwindBuild.targets           # MSBuild integration
├── Styles/
│   └── app.css                     # Tailwind input CSS
└── wwwroot/
    └── css/
        └── app.css                 # Compiled Tailwind CSS (generated)
```

---

## Customizing Your Setup

### Tailwind Configuration

Edit `tailwind.config.js` to customize your Tailwind setup:

```javascript
module.exports = {
  content: [
    './**/*.razor',
    './**/*.html',
    './**/*.cshtml'
  ],
  theme: {
    extend: {
      colors: {
        'brand-blue': '#1E40AF',
        'brand-purple': '#7C3AED'
      }
    }
  },
  plugins: []
}
```

After making changes, rebuild your project:
```bash
dotnet build
```

### Input CSS File

Add custom CSS to `Styles/app.css`:

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

/* Your custom styles */
@layer components {
  .btn-primary {
    @apply bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700;
  }
}
```

### MSBuild Integration

The `TailwindBuild.targets` file runs before every build:

```xml
<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
    <Exec
      Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css --minify"
      Condition="'$(Configuration)' == 'Release'"
      WorkingDirectory="$(ProjectDir)" />
    <Exec
      Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css"
      Condition="'$(Configuration)' == 'Debug'"
      WorkingDirectory="$(ProjectDir)" />
  </Target>
</Project>
```

**Note**: CSS is minified only in Release builds for better development experience.

---

## Troubleshooting

### Problem: "Node.js not found"

**Cause**: Node.js is not installed or not in your PATH.

**Solution**:
1. Install Node.js from [nodejs.org](https://nodejs.org/)
2. Or use nvm (Node Version Manager):
   ```bash
   curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash
   nvm install --lts
   ```
3. Restart your terminal and run `tailwind-blazor setup` again

---

### Problem: "Not a Blazor project"

**Cause**: You're not in a directory containing a Blazor .csproj file.

**Solution**:
1. Navigate to your Blazor project directory:
   ```bash
   cd /path/to/BlazorProject
   ```
2. Or specify the project directory:
   ```bash
   tailwind-blazor setup --project-dir /path/to/BlazorProject
   ```

---

### Problem: npm install fails with network errors

**Cause**: Network connectivity issues or npm registry problems.

**Solution**:
1. Check your internet connection
2. Retry the setup command (it's idempotent):
   ```bash
   tailwind-blazor setup
   ```
3. Or manually install packages:
   ```bash
   npm install tailwindcss autoprefixer
   ```

---

### Problem: Tailwind styles not applied

**Cause**: Generated CSS file not referenced in layout or build failed.

**Solution**:
1. Ensure CSS is referenced in your layout file (`App.razor` or `MainLayout.razor`):
   ```razor
   <link href="css/app.css" rel="stylesheet" />
   ```
2. Rebuild your project:
   ```bash
   dotnet clean
   dotnet build
   ```
3. Check build output for Tailwind compilation errors
4. Run validation:
   ```bash
   tailwind-blazor check
   ```

---

### Problem: Permission denied when writing files

**Cause**: Insufficient file system permissions.

**Solution**:
- **macOS/Linux**: Ensure you have write permissions in project directory
  ```bash
  chmod -R u+w /path/to/project
  ```
- **Windows**: Run terminal as Administrator or check folder permissions

---

### Problem: Existing configuration detected

**Cause**: You already have Tailwind files from a previous setup.

**Solution**:

**Option 1**: Merge configurations (recommended)
```bash
# Tool will prompt and merge safely
tailwind-blazor setup
```

**Option 2**: Force overwrite (⚠ loses customizations)
```bash
tailwind-blazor setup --force
```

**Option 3**: Backup and recreate
```bash
# Backup existing files
cp tailwind.config.js tailwind.config.js.backup

# Recreate with force
tailwind-blazor setup --force

# Manually merge customizations from backup
```

---

## Next Steps

### Learn Tailwind CSS

- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
- [Tailwind CSS Playground](https://play.tailwindcss.com/)
- [Tailwind CSS Cheat Sheet](https://nerdcave.com/tailwind-cheat-sheet)

### Blazor + Tailwind Resources

- Use Tailwind classes in `.razor` components
- Combine with Blazor's component model for reusable UI
- Leverage Tailwind's JIT (Just-In-Time) mode for fast development

### Continuous Integration

Add validation to your CI pipeline:

```yaml
# .github/workflows/build.yml (GitHub Actions)
- name: Validate Tailwind Configuration
  run: tailwind-blazor check --fail-on-warning

- name: Build Project
  run: dotnet build
```

```yaml
# azure-pipelines.yml (Azure DevOps)
- script: tailwind-blazor check --fail-on-warning
  displayName: 'Validate Tailwind'
```

---

## Advanced Usage

### Dry Run Mode

Preview changes without applying them:

```bash
tailwind-blazor setup --dry-run
tailwind-blazor update --dry-run
tailwind-blazor create-target --dry-run
```

Useful for:
- CI/CD pipelines (validate changes)
- Understanding what the tool will do
- Testing different configurations

---

### JSON Output for Automation

Output check results as JSON for parsing:

```bash
tailwind-blazor check --format json > validation-results.json
```

Example JSON structure:
```json
{
  "summary": {
    "total": 17,
    "passed": 16,
    "warnings": 1,
    "errors": 0
  },
  "results": [...]
}
```

Parse with `jq` or in scripts:
```bash
# Check if any errors exist
ERRORS=$(tailwind-blazor check --format json | jq '.summary.errors')
if [ $ERRORS -gt 0 ]; then
  echo "Tailwind validation failed!"
  exit 1
fi
```

---

### Verbose Logging

Enable detailed logging for debugging:

```bash
tailwind-blazor setup --verbose
```

Shows:
- File system operations
- npm command execution
- Configuration parsing steps
- Validation rule evaluations

---

## Support

### Get Help

```bash
# General help
tailwind-blazor --help

# Command-specific help
tailwind-blazor setup --help
tailwind-blazor check --help
tailwind-blazor update --help
tailwind-blazor create-target --help
```

### Report Issues

If you encounter bugs or have feature requests:
1. Run with `--verbose` to capture detailed logs
2. Run `tailwind-blazor check --format json` for configuration snapshot
3. Report issue with logs and project setup details

---

## Summary

**That's it!** You've successfully set up Tailwind CSS in your Blazor project.

**Quick recap**:
1. ✅ Installed Tailwind Blazor CLI
2. ✅ Ran `tailwind-blazor setup` in your project
3. ✅ Built and ran your project with Tailwind styles

**Next**: Start using Tailwind utility classes in your Blazor components and enjoy rapid UI development!

**Common commands**:
```bash
tailwind-blazor setup         # Initial setup
tailwind-blazor check         # Validate configuration
tailwind-blazor update        # Update dependencies
tailwind-blazor create-target # Regenerate build targets
```

Happy coding! 🎨
