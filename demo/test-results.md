# TailwindToolbox Test Results

**Date:** Sun Jan  4 01:54:29 CET 2026
**Tool Version:** 1.0.0
**Platform:** Darwin / .NET 10.0.100

=== Testing Setup Command ===
The template "Blazor Web App" was created successfully.
This template contains technologies from parties other than Microsoft, see https://aka.ms/aspnetcore/10.0-third-party-notices for details.

Processing post-creation actions...
Restoring /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/demo-blazor-server.csproj:
  Determining projects to restore...
  Restored /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/demo-blazor-server.csproj (in 104 ms).
Restore succeeded.


[38;5;12m──────────────────────────── [0m[1;38;5;12mTailwind Blazor Setup[0m[38;5;12m ─────────────────────────────[0m

[38;5;2m✓[0m Blazor project detected: [1mdemo-blazor-server[0m ([2mServer, net10.0[0m)
[38;5;2m✓[0m Node.js v25.2.1 detected
[38;5;2m✓[0m npm 11.6.2 detected

Installing Tailwind CSS packages...
[38;5;2m✓[0m tailwindcss installed
[38;5;2m✓[0m @tailwindcss/cli installed
[38;5;2m✓[0m autoprefixer installed

[1mCreating configuration files...[0m
[38;5;2m✓[0m tailwind.config.js created
[38;5;2m✓[0m package.json created
[38;5;2m✓[0m Styles/app.css created
[38;5;2m✓[0m .gitignore updated

[1mSetting up build integration...[0m
[38;5;2m✓[0m TailwindBuild.targets created
[38;5;2m✓[0m demo-blazor-server.csproj updated

[38;5;12m────────────────────────────────────────────────────────────────────────────────[0m
[1;38;5;2mSetup complete![0m Run 'dotnet build' to compile Tailwind CSS.
✓ tailwind.config.js
✓ package.json
✓ Styles/app.css
✓ TailwindBuild.targets
✓ npm packages installed
✓ MSBuild import added
  Determining projects to restore...
  All projects are up-to-date for restore.
  ≈ tailwindcss v4.1.18
  
  Done in 48ms
  demo-blazor-server -> /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/bin/Debug/net10.0/demo-blazor-server.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:09.09
✓ CSS compiled
✓ Setup test PASSED
## Setup Command: ✓ PASSED

=== Testing Check Command ===
[38;5;12m────────────────────── [0m[1;38;5;12mTailwind Configuration Validation[0m[38;5;12m ───────────────────────[0m

[2mProject:[0m demo-blazor-server ([2mServer, net10.0[0m)

[1mEnvironment[0m

  [38;5;2m✓[0m Node.js Installation
    [2mNode.js v25.2.1 detected[0m

  [38;5;2m✓[0m npm Installation
    [2mnpm 11.6.2 detected[0m

  [38;5;2m✓[0m .NET Version
    [2m.NET net10.0 is supported[0m

[1mFiles[0m

  [38;5;2m✓[0m Tailwind Configuration File
    [2mtailwind.config.js found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mtailwind.config.js[0m

  [38;5;2m✓[0m Package.json File
    [2mpackage.json found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mpackage.json[0m

  [38;5;2m✓[0m CSS Input File
    [2mStyles/app.css found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mStyles/app.css[0m

  [38;5;2m✓[0m Build Targets File
    [2mBuild targets file found: TailwindBuild.targets[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mTailwindBuild.targets[0m

  [38;5;2m✓[0m Gitignore Configuration
    [2mnode_modules in .gitignore[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2m.gitignore[0m

[1mConfiguration[0m

  [38;5;2m✓[0m Tailwind Config Valid
    [2mtailwind.config.js structure is valid[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mtailwind.config.js[0m

  [38;5;2m✓[0m Package.json Valid
    [2mpackage.json structure is valid[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mpackage.json[0m

  [38;5;2m✓[0m Content Paths Correct
    [2mContent paths include Blazor file types[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mtailwind.config.js[0m

  [38;5;2m✓[0m Build Scripts Present
    [2mTailwind build scripts present[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mpackage.json[0m

[1mDependencies[0m

  [38;5;11m⚠[0m TailwindCSS Version
    [2mtailwindcss not installed[0m
    [38;5;11m→[0m Run 'npm install tailwindcss' or 'tailwind-blazor setup'

  [38;5;12mℹ[0m Autoprefixer Version
    [2mautoprefixer not installed (optional)[0m
    [38;5;11m→[0m Run 'npm install autoprefixer' (optional enhancement)

  [38;5;2m✓[0m No Deprecated Packages
    [2mNo deprecated packages found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mpackage.json[0m

[1mIntegration[0m

  [38;5;2m✓[0m MSBuild Import Exists
    [2mMSBuild targets imported in .csproj[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mdemo-blazor-server.csproj[0m

  [38;5;2m✓[0m Target XML Valid
    [2mTailwindBuild.targets is valid XML[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mTailwindBuild.targets[0m

[38;5;12m────────────────────────────────────────────────────────────────────────────────[0m
[1;38;5;9mSummary:[0m 15/17 checks passed
[38;5;11m  • 1 warning(s)[0m
✓ Valid JSON output
[38;5;12m────────────────────── [0m[1;38;5;12mTailwind Configuration Validation[0m[38;5;12m ───────────────────────[0m

[2mProject:[0m demo-blazor-server ([2mServer, net10.0[0m)

[1mDependencies[0m

  [38;5;11m⚠[0m TailwindCSS Version
    [2mtailwindcss not installed[0m
    [38;5;11m→[0m Run 'npm install tailwindcss' or 'tailwind-blazor setup'

  [38;5;12mℹ[0m Autoprefixer Version
    [2mautoprefixer not installed (optional)[0m
    [38;5;11m→[0m Run 'npm install autoprefixer' (optional enhancement)

  [38;5;2m✓[0m No Deprecated Packages
    [2mNo deprecated packages found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-blazor-server/[0m
[2mpackage.json[0m

[38;5;12m────────────────────────────────────────────────────────────────────────────────[0m
[1;38;5;9mSummary:[0m 1/3 checks passed
[38;5;11m  • 1 warning(s)[0m
✓ Category filter works
[38;5;12m────────────────────── [0m[1;38;5;12mTailwind Configuration Validation[0m[38;5;12m ───────────────────────[0m
[1mEnvironment[0m
[1mFiles[0m
  [38;5;2m✓[0m Tailwind Configuration File
  [38;5;2m✓[0m Gitignore Configuration
[1mConfiguration[0m
[1mDependencies[0m
[1mIntegration[0m
✓ All categories checked
✓ Check test PASSED
## Check Command: ✓ PASSED

=== Testing Update Command ===
[38;5;14mChecking for updates in:[0m demo-blazor-server

[38;5;2m✓[0m All packages are up to date!
✓ Dry-run works
[38;5;14mChecking for updates in:[0m demo-blazor-server

[38;5;2m✓[0m All packages are up to date!
[38;5;14mChecking for updates in:[0m demo-blazor-server

[38;5;2m✓[0m All packages are up to date!
✓ Update test PASSED
## Update Command: ✓ PASSED

=== Testing Create-Target Command ===
The template "Blazor Web App" was created successfully.
This template contains technologies from parties other than Microsoft, see https://aka.ms/aspnetcore/10.0-third-party-notices for details.

Processing post-creation actions...
Restoring /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-custom-target/demo-custom-target.csproj:
  Determining projects to restore...
  Restored /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-custom-target/demo-custom-target.csproj (in 106 ms).
Restore succeeded.


[38;5;14mCreating MSBuild target for:[0m demo-custom-target

[2mTarget Name:[0m  CompileCustomCSS         
[2mInput CSS:[0m    CustomStyles/main.css    
[2mOutput CSS:[0m   wwwroot/styles/output.css
[2mMinify:[0m       release-only             
[2mTargets File:[0m TailwindBuild.targets    

[38;5;2m✓[0m Created TailwindBuild.targets
[38;5;2m✓[0m Validated .targets XML structure
[38;5;2m✓[0m Updated demo-custom-target.csproj with Import reference

╭───────────────┬───────────────────────────╮
│ [1mConfiguration[0m │ [1mValue[0m                     │
├───────────────┼───────────────────────────┤
│ Target Name   │ CompileCustomCSS          │
│ Input CSS     │ CustomStyles/main.css     │
│ Output CSS    │ wwwroot/styles/output.css │
│ Minify Mode   │ release-only              │
│ Targets File  │ TailwindBuild.targets     │
╰───────────────┴───────────────────────────╯

[38;5;2m✓ MSBuild target created successfully![0m
[2mTailwind CSS will now compile automatically during builds.[0m
✓ .targets created
✓ Custom target name
✓ Custom input path
✓ Create-target test PASSED
## Create-Target Command: ✓ PASSED

=== Testing End-to-End Workflow ===
The template "Blazor Web App" was created successfully.
This template contains technologies from parties other than Microsoft, see https://aka.ms/aspnetcore/10.0-third-party-notices for details.

Processing post-creation actions...
Restoring /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/demo-e2e.csproj:
  Determining projects to restore...
  Restored /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/demo-e2e.csproj (in 106 ms).
Restore succeeded.


[38;5;12m──────────────────────────── [0m[1;38;5;12mTailwind Blazor Setup[0m[38;5;12m ─────────────────────────────[0m

[38;5;2m✓[0m Blazor project detected: [1mdemo-e2e[0m ([2mServer, net10.0[0m)
[38;5;2m✓[0m Node.js v25.2.1 detected
[38;5;2m✓[0m npm 11.6.2 detected

Installing Tailwind CSS packages...
[38;5;2m✓[0m tailwindcss installed
[38;5;2m✓[0m @tailwindcss/cli installed
[38;5;2m✓[0m autoprefixer installed

[1mCreating configuration files...[0m
[38;5;2m✓[0m tailwind.config.js created
[38;5;2m✓[0m package.json created
[38;5;2m✓[0m Styles/app.css created
[38;5;2m✓[0m .gitignore updated

[1mSetting up build integration...[0m
[38;5;2m✓[0m TailwindBuild.targets created
[38;5;2m✓[0m demo-e2e.csproj updated

[38;5;12m────────────────────────────────────────────────────────────────────────────────[0m
[1;38;5;2mSetup complete![0m Run 'dotnet build' to compile Tailwind CSS.
[38;5;12m────────────────────── [0m[1;38;5;12mTailwind Configuration Validation[0m[38;5;12m ───────────────────────[0m

[2mProject:[0m demo-e2e ([2mServer, net10.0[0m)

[1mEnvironment[0m

  [38;5;2m✓[0m Node.js Installation
    [2mNode.js v25.2.1 detected[0m

  [38;5;2m✓[0m npm Installation
    [2mnpm 11.6.2 detected[0m

  [38;5;2m✓[0m .NET Version
    [2m.NET net10.0 is supported[0m

[1mFiles[0m

  [38;5;2m✓[0m Tailwind Configuration File
    [2mtailwind.config.js found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/tailwind.c[0m
[2monfig.js[0m

  [38;5;2m✓[0m Package.json File
    [2mpackage.json found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/package.js[0m
[2mon[0m

  [38;5;2m✓[0m CSS Input File
    [2mStyles/app.css found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/Styles/app[0m
[2m.css[0m

  [38;5;2m✓[0m Build Targets File
    [2mBuild targets file found: TailwindBuild.targets[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/TailwindBu[0m
[2mild.targets[0m

  [38;5;2m✓[0m Gitignore Configuration
    [2mnode_modules in .gitignore[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/.gitignore[0m

[1mConfiguration[0m

  [38;5;2m✓[0m Tailwind Config Valid
    [2mtailwind.config.js structure is valid[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/tailwind.c[0m
[2monfig.js[0m

  [38;5;2m✓[0m Package.json Valid
    [2mpackage.json structure is valid[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/package.js[0m
[2mon[0m

  [38;5;2m✓[0m Content Paths Correct
    [2mContent paths include Blazor file types[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/tailwind.c[0m
[2monfig.js[0m

  [38;5;2m✓[0m Build Scripts Present
    [2mTailwind build scripts present[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/package.js[0m
[2mon[0m

[1mDependencies[0m

  [38;5;11m⚠[0m TailwindCSS Version
    [2mtailwindcss not installed[0m
    [38;5;11m→[0m Run 'npm install tailwindcss' or 'tailwind-blazor setup'

  [38;5;12mℹ[0m Autoprefixer Version
    [2mautoprefixer not installed (optional)[0m
    [38;5;11m→[0m Run 'npm install autoprefixer' (optional enhancement)

  [38;5;2m✓[0m No Deprecated Packages
    [2mNo deprecated packages found[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/package.js[0m
[2mon[0m

[1mIntegration[0m

  [38;5;2m✓[0m MSBuild Import Exists
    [2mMSBuild targets imported in .csproj[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/demo-e2e.c[0m
[2msproj[0m

  [38;5;2m✓[0m Target XML Valid
    [2mTailwindBuild.targets is valid XML[0m
    [2mFile: [0m
[2m/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/TailwindBu[0m
[2mild.targets[0m

[38;5;12m────────────────────────────────────────────────────────────────────────────────[0m
[1;38;5;9mSummary:[0m 15/17 checks passed
[38;5;11m  • 1 warning(s)[0m
  Determining projects to restore...
  All projects are up-to-date for restore.
  ≈ tailwindcss v4.1.18
  
  Done in 51ms
  demo-e2e -> /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.45
✓ Container utility compiled
Using launch settings from /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/Properties/launchSettings.json...
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5226
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e
✓ Application started
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
Build started 04/01/2026 01:55:09.
     1>Project "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/demo-e2e.csproj" on node 1 (Clean target(s)).
     1>CoreClean:
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/appsettings.Development.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/appsettings.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/package-lock.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/package.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.staticwebassets.runtime.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.staticwebassets.endpoints.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.deps.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.runtimeconfig.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.dll".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.pdb".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/EmbeddedAttribute.cs".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/ValidatableTypeAttribute.cs".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/rpswa.dswa.cache.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.GeneratedMSBuildEditorConfig.editorconfig".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.AssemblyInfoInputs.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.AssemblyInfo.cs".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.csproj.CoreCompileInputs.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.MvcApplicationPartsAssemblyInfo.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/rjimswa.dswa.cache.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/rjsmrazor.dswa.cache.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/rjsmcshtml.dswa.cache.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/scopedcss/Components/Layout/MainLayout.razor.rz.scp.css".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/scopedcss/Components/Layout/NavMenu.razor.rz.scp.css".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/scopedcss/Components/Layout/ReconnectModal.razor.rz.scp.css".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/scopedcss/bundle/demo-e2e.styles.css".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/scopedcss/projectbundle/demo-e2e.bundle.scp.css".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/n26kqa8g41-{0}-w7mf952350-w7mf952350.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/aewijk8kfr-{0}-bqjiyaj88i-bqjiyaj88i.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/qnkr2m6sgk-{0}-c2jlpeoesf-c2jlpeoesf.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/203hy2fsdu-{0}-erw9l3u2r3-erw9l3u2r3.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/v9l25se1yy-{0}-aexeepp0ev-aexeepp0ev.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/brd1w1m7l6-{0}-d7shbmvgxk-d7shbmvgxk.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/vtiv6qo23t-{0}-ausgxo2sd3-ausgxo2sd3.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/mxl0ftgmi2-{0}-k8d9w2qqmf-k8d9w2qqmf.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/5i6894pxb3-{0}-cosvhxvwiu-cosvhxvwiu.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/hj5xtvljdt-{0}-ub07r2b239-ub07r2b239.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/72f5zkbamq-{0}-fvhpjtyr6v-fvhpjtyr6v.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/yozyajx925-{0}-b7pk76d08c-b7pk76d08c.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/d4n35sv3z2-{0}-fsbi9cje9m-fsbi9cje9m.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/mtmyh8bgbm-{0}-rzd6atqjts-rzd6atqjts.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/v98hpenbt9-{0}-ee0r1s7dh0-ee0r1s7dh0.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/of7nbkvk26-{0}-dxx9fxp4il-dxx9fxp4il.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/4v55ll3mgp-{0}-jd9uben2k1-jd9uben2k1.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/ddlmlp5kq5-{0}-khv3u5hwcm-khv3u5hwcm.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/hcrffdglri-{0}-r4e9w2rdcm-r4e9w2rdcm.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/o6cwu6y8cd-{0}-lcd1t2u6c8-lcd1t2u6c8.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/sfm4vae656-{0}-c2oey78nd0-c2oey78nd0.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/pk3990mie9-{0}-tdbxkamptv-tdbxkamptv.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/kg6t5xoopd-{0}-j5mq2jizvt-j5mq2jizvt.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/g659237l0n-{0}-06098lyss8-06098lyss8.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/6c7z2yydnf-{0}-nvvlpmu67g-nvvlpmu67g.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/b9hvey5uaa-{0}-s35ty4nyc5-s35ty4nyc5.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/wht0l3pn6h-{0}-pj5nd1wqec-pj5nd1wqec.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/46k0ii15m3-{0}-46ein0sx1k-46ein0sx1k.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/6ruh28tfzz-{0}-v0zj4ognzu-v0zj4ognzu.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/ffki1931pc-{0}-37tfw0ft22-37tfw0ft22.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/sl2bl10lqd-{0}-hrwsygsryq-hrwsygsryq.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/5r5cw0cylm-{0}-pk9g2wxc8p-pk9g2wxc8p.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/xlv7i1l6lh-{0}-ft3s53vfgj-ft3s53vfgj.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/ozyb5hp8s9-{0}-6cfz1n2cew-6cfz1n2cew.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/e4cmblp4hn-{0}-6pdc2jztkx-6pdc2jztkx.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/lngrb7lc9e-{0}-493y06b0oq-493y06b0oq.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/l68k9tw6zb-{0}-iovd86k7lj-iovd86k7lj.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/wd6l8nr3se-{0}-vr1egmr9el-vr1egmr9el.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/ne2wf4mng1-{0}-kbrnm935zg-kbrnm935zg.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/nytzg3k3a8-{0}-jj8uyg4cgr-jj8uyg4cgr.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/dqaanufvt4-{0}-y7v9cxd14o-y7v9cxd14o.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/jo9qurv7vi-{0}-notf2xhcfb-notf2xhcfb.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/oc4et5w31b-{0}-h1s4sie4z3-h1s4sie4z3.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/zb1zchtgsw-{0}-63fj8s7r0e-63fj8s7r0e.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/paql3wd073-{0}-0j3bgjxly4-0j3bgjxly4.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/tkrlobyxv5-{0}-w97mql0d9w-w97mql0d9w.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/emidzsrat8-{0}-j8lzlu28q6-j8lzlu28q6.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/vwjd8gwsv1-{0}-u1n4jc5v46-u1n4jc5v46.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/5u0a5y9ybx-{0}-97dvsmv3pn-97dvsmv3pn.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/r6ti2jca5z-{0}-97dvsmv3pn-97dvsmv3pn.gz".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/staticwebassets.build.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/staticwebassets.build.json.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/staticwebassets.development.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/staticwebassets.build.endpoints.json".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/swae.build.ex.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.dll".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/refint/demo-e2e.dll".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.pdb".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/demo-e2e.genruntimeconfig.cache".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/ref/demo-e2e.dll".
         Deleting file "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/obj/Debug/net10.0/compressed/6pwt82hc66-{0}-kltzb8uwg7-kltzb8uwg7.gz".
     1>Done Building Project "/Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/demo-e2e.csproj" (Clean target(s)).

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.23
  Determining projects to restore...
  All projects are up-to-date for restore.
  ≈ tailwindcss v4.1.18
  
  Done in 40ms
  demo-e2e -> /Users/phmatray/Repositories/github-phm/TailwindToolbox/demo/demo-e2e/bin/Debug/net10.0/demo-e2e.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.31
✓ MSBuild integration works
✓ End-to-end test PASSED
## End-to-End Workflow: ✓ PASSED

