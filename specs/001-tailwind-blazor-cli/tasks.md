# Tasks: Tailwind Blazor CLI Setup Tool

**Input**: Design documents from `/specs/001-tailwind-blazor-cli/`
**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/

**Tests**: TDD is MANDATORY per constitution. All tests written FIRST, verified to FAIL, then implementation proceeds.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

## Path Conventions

Project structure (from plan.md):
- **Main project**: `TailwindToolbox/`
- **Test project**: `TailwindToolbox.Tests/`
- **Scripts**: `scripts/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create TailwindToolbox.csproj with .NET 10.0, enable nullable types, warnings as errors
- [ ] T002 [P] Create TailwindToolbox.Tests.csproj with xunit v3 and test project references
- [ ] T003 [P] Add Spectre.Console.Cli NuGet package to TailwindToolbox.csproj
- [ ] T004 [P] Create Templates/ directory and embed template files as resources in TailwindToolbox.csproj
- [ ] T005 Create scripts/install-tool.sh for macOS installation
- [ ] T006 [P] Add .gitignore entries for bin/, obj/, node_modules/

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [ ] T007 Create Models/BlazorProjectType.cs enum (Server, WebAssembly, Hybrid, Unknown)
- [ ] T008 Create Models/BlazorProject.cs with all properties from data-model.md
- [ ] T009 [P] Create Models/ValidationCategory.cs enum (Environment, Files, Configuration, Dependencies, Integration)
- [ ] T010 [P] Create Models/ValidationSeverity.cs enum (Error, Warning, Info)
- [ ] T011 Create Models/ValidationResult.cs record with all properties from data-model.md
- [ ] T012 [P] Create Models/ValidationRule.cs record with Func<BlazorProject, ValidationResult> check function
- [ ] T013 Create Services/ProjectDetector.cs interface IProjectDetector with DetectProject method
- [ ] T014 [P] Create Templates/tailwind.config.template.js with Blazor-specific content paths
- [ ] T015 [P] Create Templates/package.template.json with Tailwind CSS v4 dependencies
- [ ] T016 [P] Create Templates/app.template.css with @tailwind directives
- [ ] T017 [P] Create Templates/TailwindBuild.template.targets with MSBuild Exec tasks
- [ ] T018 Create Program.cs with Spectre.Console.Cli configuration and command registration
- [ ] T019 Setup dependency injection container in Program.cs for services

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Initial Tailwind Setup (Priority: P1) 🎯 MVP

**Goal**: Enable developers to run a single command that configures all necessary files, dependencies, and build processes for Tailwind CSS in their Blazor project

**Independent Test**: Run setup command on a fresh Blazor project and verify Tailwind classes render correctly in components

### Tests for User Story 1 (TDD - WRITE FIRST) ⚠️

> **CRITICAL**: Write these tests FIRST, ensure they FAIL, get approval, THEN implement

- [ ] T020 [P] [US1] Unit test for ProjectDetector.DetectProject in TailwindToolbox.Tests/Unit/ProjectDetectorTests.cs (test .csproj parsing logic)
- [ ] T021 [P] [US1] Unit test for FileGenerator service in TailwindToolbox.Tests/Unit/FileGeneratorTests.cs (test template substitution)
- [ ] T022 [P] [US1] Unit test for NpmService in TailwindToolbox.Tests/Unit/NpmServiceTests.cs (mock process execution)
- [ ] T023 [US1] Integration test for SetupCommand in TailwindToolbox.Tests/Integration/SetupCommandIntegrationTests.cs (test full setup flow with temp directory)
- [ ] T024 [US1] Contract test for npm package installation in TailwindToolbox.Tests/Contract/NpmServiceContractTests.cs (validate npm commands)
- [ ] T025 [US1] Contract test for MSBuild target XML structure in TailwindToolbox.Tests/Contract/MSBuildTargetContractTests.cs (validate .targets schema)

**⚠️ GATE**: Tests written, reviewed, approved, and FAILING. Proceed to implementation.

### Implementation for User Story 1

- [ ] T026 [P] [US1] Implement ProjectDetector.DetectProject in Services/ProjectDetector.cs (parse .csproj XML, detect SDK and package references)
- [ ] T027 [P] [US1] Create Models/TailwindConfig.cs record with FilePath, ContentPaths, Theme, Plugins, DarkMode properties
- [ ] T028 [P] [US1] Create Models/PackageConfig.cs record with FilePath, Name, Version, Dependencies, DevDependencies, Scripts
- [ ] T029 [P] [US1] Create Models/DependencyVersion.cs record with PackageName, InstalledVersion, RequiredVersion, LatestVersion, IsCompatible
- [ ] T030 [P] [US1] Create Models/BuildTarget.cs record with FilePath, TargetName, InputCssPath, OutputCssPath, RunBeforeTargets, IsMinified
- [ ] T031 [US1] Implement FileGenerator service in Services/FileGenerator.cs (load embedded templates, perform {{VARIABLE}} substitution, write files)
- [ ] T032 [US1] Implement NpmService in Services/NpmService.cs (execute npm commands via System.Diagnostics.Process, handle errors)
- [ ] T033 [US1] Implement TargetFileGenerator in Services/TargetFileGenerator.cs (generate MSBuild .targets XML from template)
- [ ] T034 [US1] Create SetupCommand.cs in Commands/ with Spectre.Console.Cli command infrastructure
- [ ] T035 [US1] Implement SetupCommand settings class with options (--project-dir, --tailwind-version, --force, --dry-run, --skip-npm-install, --css-output)
- [ ] T036 [US1] Implement SetupCommand.ExecuteAsync: detect Blazor project type
- [ ] T037 [US1] Implement SetupCommand.ExecuteAsync: validate Node.js and npm installation
- [ ] T038 [US1] Implement SetupCommand.ExecuteAsync: check for existing configuration, prompt if --force not set
- [ ] T039 [US1] Implement SetupCommand.ExecuteAsync: install npm packages (tailwindcss, autoprefixer) with progress indicator
- [ ] T040 [US1] Implement SetupCommand.ExecuteAsync: generate tailwind.config.js from template
- [ ] T041 [US1] Implement SetupCommand.ExecuteAsync: generate package.json from template
- [ ] T042 [US1] Implement SetupCommand.ExecuteAsync: create Styles/app.css from template
- [ ] T043 [US1] Implement SetupCommand.ExecuteAsync: generate TailwindBuild.targets from template
- [ ] T044 [US1] Implement SetupCommand.ExecuteAsync: update .csproj with <Import Project="TailwindBuild.targets" />
- [ ] T045 [US1] Implement SetupCommand.ExecuteAsync: update .gitignore to exclude node_modules
- [ ] T046 [US1] Implement SetupCommand.ExecuteAsync: display success summary with Spectre.Console tables
- [ ] T047 [US1] Implement SetupCommand.ExecuteAsync: dry-run mode (show changes without writing files)
- [ ] T048 [US1] Add error handling for all failure scenarios (project not found, npm install failed, permission errors)
- [ ] T049 [US1] Verify all unit tests pass with implemented code (Green phase)
- [ ] T050 [US1] Verify integration tests pass with full setup flow (Green phase)
- [ ] T051 [US1] Verify contract tests pass with real npm/MSBuild validation (Green phase)
- [ ] T052 [US1] Refactor: Extract common validation logic to helper methods, remove duplication (Refactor phase)

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently. Developers can run `tailwind-blazor setup` and get a working Tailwind configuration.

---

## Phase 4: User Story 2 - Configuration Validation (Priority: P2)

**Goal**: Enable developers to run a check command that validates all configuration files, dependencies, and build targets, providing immediate diagnostic feedback

**Independent Test**: Run check command on projects with various configuration states (correct, missing files, incorrect versions) and verify appropriate diagnostic messages

### Tests for User Story 2 (TDD - WRITE FIRST) ⚠️

> **CRITICAL**: Write these tests FIRST, ensure they FAIL, get approval, THEN implement

- [ ] T053 [P] [US2] Unit test for ValidationService.CreateValidationRules in TailwindToolbox.Tests/Unit/ValidationServiceTests.cs (test rule registry)
- [ ] T054 [P] [US2] Unit test for ValidationService.ExecuteValidationRules in TailwindToolbox.Tests/Unit/ValidationServiceTests.cs (test parallel execution)
- [ ] T055 [US2] Integration test for CheckCommand with all checks passing in TailwindToolbox.Tests/Integration/CheckCommandIntegrationTests.cs
- [ ] T056 [US2] Integration test for CheckCommand with errors in TailwindToolbox.Tests/Integration/CheckCommandIntegrationTests.cs
- [ ] T057 [US2] Integration test for CheckCommand JSON output format in TailwindToolbox.Tests/Integration/CheckCommandIntegrationTests.cs

**⚠️ GATE**: Tests written, reviewed, approved, and FAILING. Proceed to implementation.

### Implementation for User Story 2

- [ ] T058 [P] [US2] Create ValidationService interface IValidationService in Services/ValidationService.cs
- [ ] T059 [US2] Implement ValidationService.CreateValidationRules - register all 17 validation rules from data-model.md
- [ ] T060 [US2] Implement Environment validation rules (NODE_INSTALLED, NPM_INSTALLED, DOTNET_VERSION) in ValidationService
- [ ] T061 [US2] Implement Files validation rules (TAILWIND_CONFIG_EXISTS, PACKAGE_JSON_EXISTS, CSS_INPUT_EXISTS, BUILD_TARGETS_EXIST, GITIGNORE_CONFIGURED) in ValidationService
- [ ] T062 [US2] Implement Configuration validation rules (TAILWIND_CONFIG_VALID, PACKAGE_JSON_VALID, CONTENT_PATHS_CORRECT, BUILD_SCRIPTS_PRESENT) in ValidationService
- [ ] T063 [US2] Implement Dependencies validation rules (TAILWINDCSS_VERSION, AUTOPREFIXER_VERSION, NO_DEPRECATED_PACKAGES) in ValidationService
- [ ] T064 [US2] Implement Integration validation rules (MSBUILD_IMPORT_EXISTS, TARGET_XML_VALID) in ValidationService
- [ ] T065 [US2] Implement ValidationService.ExecuteValidationRules with PLINQ parallel execution
- [ ] T066 [US2] Implement ValidationService.CategorizeResults to group results by ValidationCategory
- [ ] T067 [US2] Create CheckCommand.cs in Commands/ with Spectre.Console.Cli command infrastructure
- [ ] T068 [US2] Implement CheckCommand settings class with options (--project-dir, --category, --format, --fail-on-warning)
- [ ] T069 [US2] Implement CheckCommand.ExecuteAsync: detect Blazor project
- [ ] T070 [US2] Implement CheckCommand.ExecuteAsync: execute validation rules via ValidationService
- [ ] T071 [US2] Implement CheckCommand.ExecuteAsync: table output format with Spectre.Console tables (categorized results)
- [ ] T072 [US2] Implement CheckCommand.ExecuteAsync: JSON output format (structured validation results)
- [ ] T073 [US2] Implement CheckCommand.ExecuteAsync: text output format (simple pass/fail list)
- [ ] T074 [US2] Implement CheckCommand.ExecuteAsync: display remediation guidance for failed checks
- [ ] T075 [US2] Implement CheckCommand.ExecuteAsync: category filtering (--category environment|files|config|dependencies|integration)
- [ ] T076 [US2] Implement CheckCommand.ExecuteAsync: exit code logic (0 if pass/warn, 1 if errors, 2 if project not found)
- [ ] T077 [US2] Implement CheckCommand.ExecuteAsync: --fail-on-warning flag (treat warnings as errors)
- [ ] T078 [US2] Verify all unit tests pass with implemented code (Green phase)
- [ ] T079 [US2] Verify integration tests pass with check command (Green phase)
- [ ] T080 [US2] Refactor: Extract output formatting to separate formatter classes, remove duplication (Refactor phase)

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently. Developers can validate their Tailwind setup without running the full project.

---

## Phase 5: User Story 3 - Dependency Updates (Priority: P3)

**Goal**: Enable developers to run an update command that safely upgrades Tailwind CSS and related packages while warning about breaking changes

**Independent Test**: Run update command on a project with old Tailwind versions and verify packages are updated to compatible latest versions

### Tests for User Story 3 (TDD - WRITE FIRST) ⚠️

> **CRITICAL**: Write these tests FIRST, ensure they FAIL, get approval, THEN implement

- [ ] T081 [P] [US3] Unit test for NpmService.CheckForUpdates in TailwindToolbox.Tests/Unit/NpmServiceTests.cs (mock npm outdated command)
- [ ] T082 [P] [US3] Unit test for NpmService.DetectBreakingChanges in TailwindToolbox.Tests/Unit/NpmServiceTests.cs (test semver major version detection)
- [ ] T083 [US3] Integration test for UpdateCommand with patch updates in TailwindToolbox.Tests/Integration/UpdateCommandIntegrationTests.cs
- [ ] T084 [US3] Integration test for UpdateCommand with breaking changes warning in TailwindToolbox.Tests/Integration/UpdateCommandIntegrationTests.cs
- [ ] T085 [US3] Integration test for UpdateCommand dry-run mode in TailwindToolbox.Tests/Integration/UpdateCommandIntegrationTests.cs

**⚠️ GATE**: Tests written, reviewed, approved, and FAILING. Proceed to implementation.

### Implementation for User Story 3

- [ ] T086 [P] [US3] Extend NpmService with CheckForUpdates method (execute npm outdated --json, parse results)
- [ ] T087 [P] [US3] Extend NpmService with DetectBreakingChanges method (compare semver major versions)
- [ ] T088 [P] [US3] Extend NpmService with UpdatePackage method (execute npm install package@version)
- [ ] T089 [US3] Create UpdateCommand.cs in Commands/ with Spectre.Console.Cli command infrastructure
- [ ] T090 [US3] Implement UpdateCommand settings class with options (--project-dir, --package, --target-version, --dry-run, --skip-breaking)
- [ ] T091 [US3] Implement UpdateCommand.ExecuteAsync: detect Blazor project
- [ ] T092 [US3] Implement UpdateCommand.ExecuteAsync: check for available updates via NpmService
- [ ] T093 [US3] Implement UpdateCommand.ExecuteAsync: display update table (current, latest, change type) with Spectre.Console
- [ ] T094 [US3] Implement UpdateCommand.ExecuteAsync: detect breaking changes and display warning with migration guide link
- [ ] T095 [US3] Implement UpdateCommand.ExecuteAsync: prompt user for confirmation if breaking changes detected
- [ ] T096 [US3] Implement UpdateCommand.ExecuteAsync: skip major version updates if --skip-breaking flag set
- [ ] T097 [US3] Implement UpdateCommand.ExecuteAsync: execute package updates via NpmService
- [ ] T098 [US3] Implement UpdateCommand.ExecuteAsync: update package.json with new versions
- [ ] T099 [US3] Implement UpdateCommand.ExecuteAsync: dry-run mode (show updates without executing)
- [ ] T100 [US3] Implement UpdateCommand.ExecuteAsync: exit code 3 if no updates available
- [ ] T101 [US3] Implement UpdateCommand.ExecuteAsync: exit code 2 if user cancels breaking change update
- [ ] T102 [US3] Verify all unit tests pass with implemented code (Green phase)
- [ ] T103 [US3] Verify integration tests pass with update scenarios (Green phase)
- [ ] T104 [US3] Refactor: Extract semver comparison logic to utility class (Refactor phase)

**Checkpoint**: At this point, User Stories 1, 2, AND 3 should all work independently. Developers can safely update Tailwind dependencies with breaking change awareness.

---

## Phase 6: User Story 4 - Build Target Management (Priority: P4)

**Goal**: Enable developers to generate or update MSBuild .targets files that integrate Tailwind compilation into the Blazor build process

**Independent Test**: Run create-target command and verify the generated MSBuild target file compiles Tailwind CSS correctly during project build

### Tests for User Story 4 (TDD - WRITE FIRST) ⚠️

> **CRITICAL**: Write these tests FIRST, ensure they FAIL, get approval, THEN implement

- [ ] T105 [P] [US4] Unit test for TargetFileGenerator.GenerateTargetFile in TailwindToolbox.Tests/Unit/TargetFileGeneratorTests.cs (test XML generation)
- [ ] T106 [P] [US4] Unit test for TargetFileGenerator.UpdateCsprojImport in TailwindToolbox.Tests/Unit/TargetFileGeneratorTests.cs (test .csproj modification)
- [ ] T107 [US4] Integration test for CreateTargetCommand in TailwindToolbox.Tests/Integration/CreateTargetCommandIntegrationTests.cs
- [ ] T108 [US4] Contract test for MSBuild target execution in TailwindToolbox.Tests/Contract/MSBuildTargetContractTests.cs (validate target runs during build)

**⚠️ GATE**: Tests written, reviewed, approved, and FAILING. Proceed to implementation.

### Implementation for User Story 4

- [ ] T109 [P] [US4] Extend TargetFileGenerator with GenerateTargetFile method (load template, substitute variables, generate XML)
- [ ] T110 [P] [US4] Extend TargetFileGenerator with UpdateCsprojImport method (add <Import> element to .csproj if not present)
- [ ] T111 [P] [US4] Extend TargetFileGenerator with ValidateTargetXml method (ensure well-formed XML, validate against MSBuild schema)
- [ ] T112 [US4] Create CreateTargetCommand.cs in Commands/ with Spectre.Console.Cli command infrastructure
- [ ] T113 [US4] Implement CreateTargetCommand settings class with options (--project-dir, --target-name, --input-css, --output-css, --minify, --force, --dry-run)
- [ ] T114 [US4] Implement CreateTargetCommand.ExecuteAsync: detect Blazor project
- [ ] T115 [US4] Implement CreateTargetCommand.ExecuteAsync: check for existing .targets file, prompt if --force not set
- [ ] T116 [US4] Implement CreateTargetCommand.ExecuteAsync: generate BuildTarget model from settings
- [ ] T117 [US4] Implement CreateTargetCommand.ExecuteAsync: generate .targets file via TargetFileGenerator
- [ ] T118 [US4] Implement CreateTargetCommand.ExecuteAsync: handle minify modes (always, never, release-only)
- [ ] T119 [US4] Implement CreateTargetCommand.ExecuteAsync: update .csproj with <Import> via TargetFileGenerator
- [ ] T120 [US4] Implement CreateTargetCommand.ExecuteAsync: dry-run mode (display generated XML without writing)
- [ ] T121 [US4] Implement CreateTargetCommand.ExecuteAsync: display success summary with configuration details
- [ ] T122 [US4] Verify all unit tests pass with implemented code (Green phase)
- [ ] T123 [US4] Verify integration tests pass with target generation (Green phase)
- [ ] T124 [US4] Verify contract tests pass with MSBuild target execution (Green phase)
- [ ] T125 [US4] Refactor: Extract XML manipulation to utility class (Refactor phase)

**Checkpoint**: All user stories should now be independently functional. Developers can manage all aspects of Tailwind setup, validation, updates, and build integration.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories or enhance overall quality

- [ ] T126 [P] Add global --help implementation with command descriptions and examples
- [ ] T127 [P] Add global --version implementation displaying tool version, .NET runtime, Spectre.Console version
- [ ] T128 [P] Add global --verbose implementation with detailed logging to all commands
- [ ] T129 [P] Add global --no-color implementation to disable ANSI color codes
- [ ] T130 [P] Implement consistent error handling across all commands (catch exceptions, display user-friendly messages, exit codes)
- [ ] T131 [P] Add progress indicators with Spectre.Console.Status for long-running operations (npm install, validation)
- [ ] T132 [P] Ensure all console output follows accessibility standards (color with text fallbacks, clear error messages)
- [ ] T133 Update scripts/install-tool.sh to build Release configuration and copy to /usr/local/bin
- [ ] T134 [P] Create README.md with installation instructions, quick start, command reference
- [ ] T135 [P] Create CONTRIBUTING.md with development setup, testing guide, PR process
- [ ] T136 [P] Add CI/CD workflow example (GitHub Actions) for validation in documentation
- [ ] T137 Run quickstart.md validation - follow guide end-to-end on sample Blazor project
- [ ] T138 [P] Final code review - ensure nullable reference types used throughout
- [ ] T139 [P] Final code review - ensure warnings as errors configuration in all .csproj files
- [ ] T140 [P] Final code review - verify no speculative abstractions or unused code
- [ ] T141 Run all tests (unit, integration, contract) and ensure 100% pass rate
- [ ] T142 Measure setup command performance - ensure <2 minute target met
- [ ] T143 Measure check command performance - ensure <5 second target met

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-6)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3 → P4)
- **Polish (Phase 7)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - Uses ProjectDetector from US1 but can run independently
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - Extends NpmService from US1 but independently testable
- **User Story 4 (P4)**: Can start after Foundational (Phase 2) - Uses TargetFileGenerator from US1 but independently testable

**Key Insight**: All user stories depend on Foundational phase but are otherwise INDEPENDENT. They can be implemented in parallel by different developers or sequentially in priority order.

### Within Each User Story

**CRITICAL TDD WORKFLOW** (per Constitution Principle I):
1. **Tests FIRST**: Write all test tasks ([P] tasks can run in parallel)
2. **Test Approval**: Get tests reviewed and approved
3. **Verify Failure**: Run tests, confirm they FAIL for expected reasons
4. **Implementation**: Write minimum code to pass tests
5. **Green Phase**: Verify all tests pass
6. **Refactor Phase**: Improve code quality while keeping tests green

**Implementation Dependencies**:
- Models before services
- Services before commands
- Commands before integration tests execute successfully
- Core implementation before integration with other stories

### Parallel Opportunities

- **Setup Phase**: All tasks marked [P] can run in parallel (T002, T003, T004, T006)
- **Foundational Phase**: All tasks marked [P] can run in parallel (T009-T017)
- **Once Foundational completes**: All four user stories can start in parallel (if team capacity allows)
- **Within Each Story**: All test tasks marked [P] can run in parallel, all model tasks marked [P] can run in parallel
- **Polish Phase**: Most tasks marked [P] can run in parallel (documentation, final reviews)

---

## Parallel Example: User Story 1

```bash
# After Foundational phase completes, launch these test tasks in parallel:
Task T020: Unit test for ProjectDetector
Task T021: Unit test for FileGenerator
Task T022: Unit test for NpmService

# After tests written and approved, launch these model tasks in parallel:
Task T027: Create TailwindConfig model
Task T028: Create PackageConfig model
Task T029: Create DependencyVersion model
Task T030: Create BuildTarget model

# Services depend on models, so run sequentially:
Task T031: FileGenerator (depends on models)
Task T032: NpmService (depends on models)
Task T033: TargetFileGenerator (depends on models)

# Command implementation can proceed linearly or with parallel subtasks
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T006)
2. Complete Phase 2: Foundational (T007-T019) **CRITICAL - blocks all stories**
3. Complete Phase 3: User Story 1 (T020-T052)
4. **STOP and VALIDATE**: Test User Story 1 independently
   - Run `tailwind-blazor setup` on test Blazor project
   - Verify Tailwind classes render correctly
   - Run all US1 tests (unit, integration, contract)
5. Deploy/demo MVP if ready

**MVP Deliverable**: Developers can run one command to get Tailwind fully configured in their Blazor project.

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 → Test independently → Deploy/Demo (MVP + Validation)
4. Add User Story 3 → Test independently → Deploy/Demo (MVP + Validation + Updates)
5. Add User Story 4 → Test independently → Deploy/Demo (Full Feature Set)
6. Complete Polish → Final release

Each story adds value without breaking previous stories.

### Parallel Team Strategy

With multiple developers:

1. **Team completes Setup + Foundational together** (required foundation)
2. **Once Foundational is done**, split team:
   - Developer A: User Story 1 (T020-T052)
   - Developer B: User Story 2 (T053-T080)
   - Developer C: User Story 3 (T081-T104)
   - Developer D: User Story 4 (T105-T125)
3. Stories complete and integrate independently
4. Team reconvenes for Polish phase

---

## Notes

- **[P] tasks**: Different files, no dependencies - can run in parallel
- **[Story] label**: Maps task to specific user story for traceability
- **TDD Mandatory**: Constitution Principle I enforced - tests FIRST, fail, implement, pass, refactor
- **Each user story independently completable and testable**
- **Verify tests fail before implementing** (Red-Green-Refactor cycle)
- **Commit after each task or logical group**
- **Stop at any checkpoint to validate story independently**
- **Exit codes**: 0 (success), 1 (error), 2 (user cancelled), 3 (no action needed)
- **Avoid**: Vague tasks, same file conflicts, cross-story dependencies that break independence

---

## Task Count Summary

- **Setup Phase**: 6 tasks
- **Foundational Phase**: 13 tasks
- **User Story 1 (P1)**: 33 tasks (6 tests + 27 implementation)
- **User Story 2 (P2)**: 28 tasks (5 tests + 23 implementation)
- **User Story 3 (P3)**: 24 tasks (5 tests + 19 implementation)
- **User Story 4 (P4)**: 21 tasks (4 tests + 17 implementation)
- **Polish Phase**: 18 tasks

**Total**: 143 tasks

**Tests**: 20 test tasks (xunit v3, TDD workflow enforced)
**Parallel Tasks**: 45 tasks marked [P] (31.5% parallelizable)

---

## Constitution Compliance Verification

✅ **Principle I - TDD**: All user stories have test tasks written FIRST with explicit gates
✅ **Principle II - Code Quality**: Nullable types, warnings as errors verified in Setup (T001) and Polish (T138-T140)
✅ **Principle III - UX Consistency**: Spectre.Console used throughout for consistent CLI UX (T003)
✅ **Principle IV - Integration Testing**: Contract and integration tests for all user stories (20 test tasks)
✅ **Principle V - Simplicity**: No speculative features, direct implementations, verified in Polish (T140)
