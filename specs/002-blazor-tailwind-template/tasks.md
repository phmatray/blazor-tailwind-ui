# Tasks: Blazor Tailwind Template

**Input**: Design documents from `/specs/002-blazor-tailwind-template/`
**Prerequisites**: plan.md (required), spec.md (required), research.md, data-model.md, contracts/

**Tests**: Tests are explicitly required by Constitution Principle I (TDD). All tests MUST be written FIRST before implementation.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

## Path Conventions

- Template files: `templates/blazor-tailwind/`
- Template configuration: `templates/blazor-tailwind/.template.config/`
- Test project: `templates/blazor-tailwind.Tests/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and template folder structure creation

- [ ] T001 Create templates/blazor-tailwind/ directory structure per plan.md lines 79-113
- [ ] T002 Create templates/blazor-tailwind.Tests/ directory for test project
- [ ] T003 [P] Create templates/blazor-tailwind/.template.config/ folder for template manifest
- [ ] T004 [P] Create templates/blazor-tailwind/Components/ folder structure (Layout/, Pages/)
- [ ] T005 [P] Create templates/blazor-tailwind/Styles/ folder for Tailwind input CSS
- [ ] T006 [P] Create templates/blazor-tailwind/wwwroot/ folder structure (css/)
- [ ] T007 [P] Create templates/blazor-tailwind/Properties/ folder for launch settings

**Checkpoint**: Template folder structure complete and ready for content files

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core template configuration and test infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

### Test Infrastructure Setup (TDD - Constitution Principle I)

- [ ] T008 Create templates/blazor-tailwind.Tests/blazor-tailwind.Tests.csproj test project per research.md lines 503-529
- [ ] T009 [P] Configure xUnit v3 dependencies in blazor-tailwind.Tests.csproj
- [ ] T010 [P] Add reference to .NET template test helpers in test project

### Template Manifest Configuration

- [ ] T011 Create templates/blazor-tailwind/.template.config/template.json per contracts/template.json
- [ ] T012 [P] Create templates/blazor-tailwind/.template.config/dotnetcli.host.json per data-model.md lines 75-88
- [ ] T013 [P] Create templates/blazor-tailwind/.template.config/ide.host.json per data-model.md lines 90-112

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 & 4 - Create Blazor Project with Tailwind & Maintain Familiar Structure (Priority: P1) 🎯 MVP

**Goal**: Generate a working Blazor project with Tailwind CSS that maintains Microsoft's exact template structure

**Independent Test**: Run `dotnet new blazor-tailwind -n TestApp`, then `dotnet build` and `dotnet run` - project should build successfully and display Tailwind-styled pages

**Note**: US1 and US4 are implemented together as they are inseparable - US1 requires US4's structure to work

### Tests for US1/US4 (Write FIRST - TDD) ⚠️

> **CRITICAL: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T014 [P] [US1] Template generation test in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs per research.md lines 507-512
- [ ] T015 [P] [US1] Build integration test in templates/blazor-tailwind.Tests/TailwindIntegrationTests.cs per research.md lines 514-518
- [ ] T016 [P] [US4] Component structure verification test in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs
- [ ] T017 [P] [US1] Tailwind CSS compilation test in templates/blazor-tailwind.Tests/TailwindIntegrationTests.cs

**Checkpoint - Test Gate**: All tests written and FAILING. User must approve tests before proceeding to implementation.

### Core Project Files (US1/US4)

- [ ] T018 [P] [US1] Create templates/blazor-tailwind/BlazorTailwind.csproj per data-model.md lines 158-198 + research.md lines 95-111
- [ ] T019 [P] [US1] Create templates/blazor-tailwind/Program.cs per data-model.md lines 216-233 + research.md lines 95-111
- [ ] T020 [P] [US4] Create templates/blazor-tailwind/Components/_Imports.razor
- [ ] T021 [P] [US4] Create templates/blazor-tailwind/Components/Routes.razor per Microsoft template structure
- [ ] T022 [P] [US4] Create templates/blazor-tailwind/Properties/launchSettings.json

### Root Component (US1/US4)

- [ ] T023 [US1] Create templates/blazor-tailwind/Components/App.razor per research.md lines 297-319 with @Assets[] syntax

### Layout Components - Convert Bootstrap to Tailwind (US1/US4)

**Reference**: research.md lines 417-442 for Bootstrap → Tailwind class mapping

- [ ] T024 [P] [US1] Create templates/blazor-tailwind/Components/Layout/MainLayout.razor converting Bootstrap grid to Tailwind flexbox
- [ ] T025 [P] [US1] Create templates/blazor-tailwind/Components/Layout/MainLayout.razor.css (minimal scoped CSS)
- [ ] T026 [P] [US1] Create templates/blazor-tailwind/Components/Layout/NavMenu.razor converting Bootstrap navbar to Tailwind per research.md line 428
- [ ] T027 [P] [US1] Create templates/blazor-tailwind/Components/Layout/NavMenu.razor.css (minimal scoped CSS)
- [ ] T028 [P] [US4] Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor (NEW in .NET 10 - research.md lines 69-72)
- [ ] T029 [P] [US4] Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor.css
- [ ] T030 [P] [US4] Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor.js

### Page Components - Convert Bootstrap to Tailwind (US1/US4)

- [ ] T031 [P] [US1] Create templates/blazor-tailwind/Components/Pages/Home.razor replacing Bootstrap card classes per research.md line 429
- [ ] T032 [P] [US1] Create templates/blazor-tailwind/Components/Pages/Counter.razor replacing button classes per research.md line 427
- [ ] T033 [P] [US1] Create templates/blazor-tailwind/Components/Pages/Weather.razor converting table classes to Tailwind
- [ ] T034 [P] [US4] Create templates/blazor-tailwind/Components/Pages/Error.razor with Tailwind error styling
- [ ] T035 [P] [US4] Create templates/blazor-tailwind/Components/Pages/NotFound.razor (NEW in .NET 10 - research.md line 74)

### Tailwind CSS Configuration (US1)

**Reference**: research.md lines 220-295 for Tailwind v4.x file structure

- [ ] T036 [P] [US1] Create templates/blazor-tailwind/Styles/app.css with @import "tailwindcss" per research.md lines 236-254
- [ ] T037 [P] [US1] Create templates/blazor-tailwind/tailwind.config.js per data-model.md lines 77-114 + research.md lines 169-183
- [ ] T038 [P] [US1] Create templates/blazor-tailwind/package.json per contracts/package.json (BOTH tailwindcss AND @tailwindcss/cli)
- [ ] T039 [US1] Create templates/blazor-tailwind/TailwindBuild.targets per research.md lines 279-295 (npx @tailwindcss/cli command)

### Static Assets & Configuration (US1/US4)

- [ ] T040 [P] [US1] Create templates/blazor-tailwind/wwwroot/favicon.png
- [ ] T041 [P] [US4] Create templates/blazor-tailwind/appsettings.json
- [ ] T042 [P] [US4] Create templates/blazor-tailwind/appsettings.Development.json
- [ ] T043 [P] [US1] Create templates/blazor-tailwind/.gitignore per data-model.md lines 250-263

### MSBuild Integration (US1)

- [ ] T044 [US1] Add <Import> for TailwindBuild.targets to BlazorTailwind.csproj per data-model.md line 198

**Checkpoint**: Run Template Generation Tests (T014-T017) - ALL tests should now PASS. US1 and US4 are complete and independently testable.

---

## Phase 4: User Story 3 - Parameter Substitution (Priority: P3)

**Goal**: Support custom project names with parameter substitution (namespace, assembly name, package.json)

**Independent Test**: Create projects with different names (`dotnet new blazor-tailwind -n MyCompany.MyApp`) and verify all namespaces, assembly names updated correctly

### Tests for US3 (Write FIRST - TDD) ⚠️

- [ ] T045 [P] [US3] Parameter substitution test for .csproj in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs per research.md lines 526-529
- [ ] T046 [P] [US3] Parameter substitution test for namespaces in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs
- [ ] T047 [P] [US3] Parameter substitution test for package.json name field in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs

**Checkpoint - Test Gate**: Tests written and FAILING. User must approve before implementation.

### Implementation for US3

- [ ] T048 [P] [US3] Configure sourceName symbol in .template.config/template.json per data-model.md lines 72-78
- [ ] T049 [P] [US3] Add preferNameDirectory: true to template.json per research.md line 78
- [ ] T050 [P] [US3] Configure ProjectNameLowerCase derived symbol for package.json per research.md lines 390-403
- [ ] T051 [P] [US3] Add fileRename configuration for BlazorTailwind.csproj in template.json per data-model.md lines 158-198
- [ ] T052 [US3] Update all .razor files with namespace parameter substitution syntax
- [ ] T053 [US3] Update Program.cs with using directive parameter substitution
- [ ] T054 [US3] Update package.json name field with lowercase parameter substitution

**Checkpoint**: Run Parameter Substitution Tests (T045-T047) - ALL tests should now PASS. US3 complete.

---

## Phase 5: User Story 2 - NuGet Template Package (Priority: P2)

**Goal**: Package template for NuGet distribution and installation via `dotnet new install`

**Independent Test**: Package template, install it via `dotnet new install [package]`, verify it appears in `dotnet new list`, then uninstall

### Tests for US2 (Write FIRST - TDD) ⚠️

- [ ] T055 [P] [US2] Template installation test in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs
- [ ] T056 [P] [US2] Template list verification test in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs
- [ ] T057 [P] [US2] Template uninstall test in templates/blazor-tailwind.Tests/TemplateGenerationTests.cs

**Checkpoint - Test Gate**: Tests written and FAILING. User must approve before implementation.

### Implementation for US2

- [ ] T058 [US2] Create templates/TailwindToolbox.Blazor.Template.csproj (template package project) per research.md lines 102-125
- [ ] T059 [P] [US2] Configure PackageType=Template in template package project
- [ ] T060 [P] [US2] Set IncludeContentInPack=true and ContentTargetFolders=content
- [ ] T061 [P] [US2] Configure package metadata (PackageId, Title, Description, Tags) per plan.md lines 108-110
- [ ] T062 [P] [US2] Add Content Include for templates/**/* with bin/obj exclusions
- [ ] T063 [US2] Test local packaging with `dotnet pack -c Release`
- [ ] T064 [US2] Test local installation with `dotnet new install --add-source ./`

**Checkpoint**: Run Template Package Tests (T055-T057) - ALL tests should now PASS. US2 complete.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final improvements, documentation, and comprehensive testing

### Documentation

- [ ] T065 [P] Create templates/blazor-tailwind/README.md explaining Tailwind v4.x usage per research.md lines 577-599
- [ ] T066 [P] Add comments to tailwind.config.js explaining content paths per research.md lines 585-587
- [ ] T067 [P] Add comments to TailwindBuild.targets explaining MSBuild integration per research.md lines 589-591
- [ ] T068 [P] Document post-installation steps in template.json manualInstructions per research.md lines 323-369

### Platform-Specific npm Post-Actions

**Reference**: research.md lines 323-369 for platform-specific npm.cmd handling

- [ ] T069 [P] Add Windows npm post-action (OS == "Windows", npm.cmd) to template.json
- [ ] T070 [P] Add Unix npm post-action (OS != "Windows", npm) to template.json
- [ ] T071 [P] Add dotnet restore post-action with skipRestore condition to template.json
- [ ] T072 [P] Set continueOnError: true for all post-actions
- [ ] T073 [P] Add manualInstructions fallback for npm install per research.md lines 350-352, 364-366

### Final Validation

- [ ] T074 Run ALL template generation tests - verify 100% pass rate
- [ ] T075 Run quickstart.md validation - create project, build, run per quickstart.md lines 33-53
- [ ] T076 Verify Tailwind CSS classes compile correctly in generated wwwroot/css/app.css
- [ ] T077 Verify .NET 10 specific components present (ReconnectModal, NotFound) per research.md lines 69-74
- [ ] T078 Verify @Assets[] syntax works in App.razor per research.md lines 80-84
- [ ] T079 Verify MapStaticAssets() in Program.cs per research.md lines 91-93
- [ ] T080 Cross-platform test: Verify template works on Windows, macOS, Linux
- [ ] T081 Verify Bootstrap completely removed (no Bootstrap classes or files)
- [ ] T082 Performance test: Template instantiation completes in <30 seconds per spec.md SC-004
- [ ] T083 Performance test: Generated project builds in <10 seconds per plan.md line 20

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup (Phase 1) completion - BLOCKS all user stories
- **User Story 1 & 4 (Phase 3 - P1 MVP)**: Depends on Foundational (Phase 2) completion
- **User Story 3 (Phase 4 - P3)**: Depends on Foundational (Phase 2) completion - Can run in parallel with Phase 3
- **User Story 2 (Phase 5 - P2)**: Depends on Phase 3 completion (needs working template to package)
- **Polish (Phase 6)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 & 4 (P1)**: BLOCKS User Story 2 (US2 packages the template created by US1/4)
- **User Story 3 (P3)**: Independent - can start after Foundational - integrates into US1/4 template files
- **User Story 2 (P2)**: DEPENDS ON User Story 1 & 4 completion (must have template to package)

### Within Each User Story

- **Tests MUST be written FIRST** and FAIL before implementation (TDD - Constitution Principle I)
- **User must approve tests** at each Test Gate checkpoint before proceeding
- Template manifest before content files
- Core project files before components
- Layout components before page components
- Tailwind configuration files in parallel
- MSBuild integration after TailwindBuild.targets created

### Parallel Opportunities

- **Setup (Phase 1)**: T003-T007 can all run in parallel (different folders)
- **Foundational (Phase 2)**: T009-T010 parallel, T012-T013 parallel
- **Tests**: All tests within a user story can be written in parallel (different test files)
- **Components**: Layout components (T024-T030) can run in parallel, Page components (T031-T035) can run in parallel
- **Tailwind Config**: T036-T038 can run in parallel (different files)
- **Static Assets**: T040-T043 can run in parallel
- **US3 Implementation**: T048-T051 can run in parallel (different sections of template.json)
- **US2 Package Config**: T059-T062 can run in parallel (different properties)
- **Documentation**: T065-T068 can run in parallel (different files)
- **Post-Actions**: T069-T073 can run in parallel (different post-action entries)

---

## Parallel Example: User Story 1 & 4 Components

```bash
# Launch all layout component tasks together:
Task T024: "Create templates/blazor-tailwind/Components/Layout/MainLayout.razor"
Task T025: "Create templates/blazor-tailwind/Components/Layout/MainLayout.razor.css"
Task T026: "Create templates/blazor-tailwind/Components/Layout/NavMenu.razor"
Task T027: "Create templates/blazor-tailwind/Components/Layout/NavMenu.razor.css"
Task T028: "Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor"
Task T029: "Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor.css"
Task T030: "Create templates/blazor-tailwind/Components/Layout/ReconnectModal.razor.js"

# Launch all page component tasks together:
Task T031: "Create templates/blazor-tailwind/Components/Pages/Home.razor"
Task T032: "Create templates/blazor-tailwind/Components/Pages/Counter.razor"
Task T033: "Create templates/blazor-tailwind/Components/Pages/Weather.razor"
Task T034: "Create templates/blazor-tailwind/Components/Pages/Error.razor"
Task T035: "Create templates/blazor-tailwind/Components/Pages/NotFound.razor"

# Launch all Tailwind config files together:
Task T036: "Create templates/blazor-tailwind/Styles/app.css"
Task T037: "Create templates/blazor-tailwind/tailwind.config.js"
Task T038: "Create templates/blazor-tailwind/package.json"
```

---

## Implementation Strategy

### MVP First (User Stories 1 & 4 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (includes test infrastructure)
3. **Write Tests (T014-T017) and get user approval**
4. Complete Phase 3: User Story 1 & 4 (combined P1 MVP)
5. **STOP and VALIDATE**: Run all tests - they should now PASS
6. **Manual Verification**: Follow quickstart.md to verify template works
7. Celebrate MVP! 🎉

### Incremental Delivery

1. **Foundation**: Setup + Foundational → Test infrastructure ready
2. **MVP (US1 & US4)**: Working template with Tailwind CSS → Test independently → Local usage ready
3. **Parameter Substitution (US3)**: Custom project names → Test independently → Developer experience improved
4. **NuGet Package (US2)**: Template distribution → Test independently → Public distribution ready
5. **Polish**: Documentation and cross-platform testing → Production ready

### Parallel Team Strategy

With multiple developers:

1. **Phase 1-2**: Team completes Setup + Foundational together
2. **Once Foundational done**:
   - **Developer A**: Write tests for US1/US4 (T014-T017)
   - **Developer B**: Write tests for US3 (T045-T047)
3. **After test approval**:
   - **Developer A**: Implement US1/US4 core files (T018-T023)
   - **Developer B**: Implement US1/US4 layout components (T024-T030)
   - **Developer C**: Implement US1/US4 page components (T031-T035)
   - **Developer D**: Implement US1/US4 Tailwind config (T036-T039)
4. **Integration**: Combine work, verify tests pass
5. **US3 and US2**: Can be done by separate developers after US1/US4 complete

---

## Critical Path Analysis

**Critical Path** (longest dependency chain - cannot be parallelized):

1. T001 (Setup) →
2. T008-T011 (Test Infrastructure) →
3. T014-T017 (Write Tests) →
4. **User Approval Gate** →
5. T018 (BlazorTailwind.csproj) →
6. T044 (Add MSBuild Import) →
7. T048-T054 (US3 Parameter Substitution) →
8. T058-T064 (US2 NuGet Package) →
9. T074-T083 (Final Validation)

**Estimated Critical Path**: ~15 tasks (many tasks can run in parallel off this path)

**Total Tasks**: 83 tasks
**Parallelizable Tasks**: 47 tasks marked with [P]
**Parallel Efficiency**: ~57% of tasks can run concurrently

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label (US1, US2, US3, US4) maps task to specific user story for traceability
- US1 and US4 are combined because they are inseparable (structure IS the functionality)
- **TDD Required**: All tests MUST be written FIRST (Constitution Principle I)
- **Test Gates**: User must approve tests before implementation proceeds
- Verify tests fail before implementing
- Verify tests pass after implementing
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Follow Bootstrap → Tailwind mapping in research.md lines 417-442
- Critical .NET 10 changes documented in research.md lines 67-94
- Critical Tailwind v4.x changes documented in research.md lines 127-218
