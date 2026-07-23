<!--
  SYNC IMPACT REPORT
  ==================
  Version Change: N/A (Initial) → 1.0.0
  Rationale: Initial constitution creation for TailwindToolbox project

  Modified Principles: N/A (initial creation)

  Added Sections:
  - Core Principles (5 principles)
  - User Experience Standards
  - Development Workflow
  - Governance

  Removed Sections: N/A

  Templates Status:
  ✅ plan-template.md - Constitution Check section aligned with new principles
  ✅ spec-template.md - Aligns with UX consistency and testability requirements
  ✅ tasks-template.md - Reflects TDD workflow and test-first approach

  Follow-up TODOs: None

  Last Updated: 2026-01-03
-->

# TailwindToolbox Constitution

## Core Principles

### I. Test-Driven Development (NON-NEGOTIABLE)

TDD is mandatory for all feature development. Tests MUST be written first, reviewed and approved, verified to fail, and only then may implementation begin. The Red-Green-Refactor cycle is strictly enforced:

1. **Red**: Write failing tests that define expected behavior
2. **Green**: Implement minimum code to make tests pass
3. **Refactor**: Clean up code while keeping tests green

**Rationale**: TDD ensures code correctness from the start, provides living documentation, enables confident refactoring, and catches regressions early. Tests written after code often miss edge cases and become mere validation theater.

### II. Code Quality First

All code MUST meet the following quality standards before merge:

- **Maintainability**: Code is self-documenting with clear intent; complexity is justified
- **Simplicity**: Prefer simple solutions over clever ones; YAGNI (You Aren't Gonna Need It) principle enforced
- **Consistency**: Follow established patterns within the codebase; deviations require explicit justification
- **Type Safety**: Leverage C# nullable reference types; all warnings treated as errors
- **Performance**: No premature optimization; performance concerns addressed with profiling data

**Rationale**: Quality code reduces cognitive load, accelerates development velocity over time, and minimizes technical debt accumulation.

### III. User Experience Consistency

All user-facing components MUST adhere to consistent UX patterns:

- **Accessibility**: WCAG 2.1 Level AA compliance mandatory; all interactive elements have proper ARIA labels and keyboard navigation
- **Responsive Design**: Tailwind responsive utilities used throughout; mobile-first approach enforced
- **Visual Consistency**: Unified design system with consistent spacing (Tailwind spacing scale), typography (predefined text styles), and color palette
- **Semantic HTML**: Proper HTML5 semantic elements for structure and accessibility
- **Component Reusability**: Shared Blazor components for common UI patterns; no duplicate implementations

**Rationale**: Consistent UX builds user trust, reduces learning curve, ensures accessibility for all users, and accelerates UI development through reusable components.

### IV. Integration & Contract Testing

Focus integration testing on critical boundaries and contracts:

- **New Components**: All new Blazor components require component integration tests
- **API Contracts**: Any backend API endpoints require contract tests validating request/response schemas
- **State Management**: Blazor state management flows require integration tests for critical paths
- **External Dependencies**: Third-party integrations require contract tests to catch breaking changes

**Rationale**: Unit tests validate isolated behavior; integration tests validate that components work together correctly. Contract tests protect against breaking changes in APIs and dependencies.

### V. Simplicity & Clarity

Start simple and add complexity only when justified by concrete needs:

- **No Speculative Generality**: Build for current requirements; refactor when patterns emerge
- **Clear Dependencies**: Explicit dependency injection; avoid hidden global state
- **Minimal Abstractions**: No repository/service patterns unless multiple implementations exist
- **Direct Over Indirect**: Prefer direct method calls over event buses, mediators, or message queues unless distribution required

**Rationale**: Simple code is easier to understand, modify, and debug. Premature abstraction creates maintenance burden without benefit. Refactor toward patterns when actual need emerges.

## User Experience Standards

### Component Development

- Build custom UI components using native Blazor components and Tailwind CSS utility classes
- Components MUST be independently testable with clear, documented public APIs
- All interactive components require keyboard navigation support and screen reader compatibility
- Form components MUST include proper validation with accessible error messages

### Design System Adherence

- **Spacing**: Use Tailwind spacing scale exclusively (no arbitrary values like `p-[17px]`)
- **Colors**: Define semantic color palette in Tailwind config; reference by semantic names
- **Typography**: Predefined text styles (heading-1, heading-2, body, caption, etc.)
- **Breakpoints**: Standard Tailwind breakpoints (sm, md, lg, xl, 2xl); custom breakpoints require justification

### Accessibility Requirements

- All interactive elements MUST have focus indicators (visible focus ring)
- Color MUST NOT be the only means of conveying information
- Text MUST meet WCAG AA contrast ratios (4.5:1 for normal text, 3:1 for large text)
- Forms MUST have properly associated labels and error messages
- Dynamic content MUST use ARIA live regions for screen reader announcements

## Development Workflow

### Feature Development Process

1. **Specification**: Feature requirements documented with user scenarios and acceptance criteria
2. **Test Design**: Write comprehensive tests covering happy paths, edge cases, and error conditions
3. **Test Approval**: Tests reviewed and approved before implementation begins
4. **Test Failure Verification**: Confirm tests fail for expected reasons
5. **Implementation**: Write minimum code to pass tests
6. **Refactoring**: Improve code quality while maintaining passing tests
7. **Integration**: Ensure new code integrates with existing codebase without breaking changes

### Code Review Requirements

All pull requests MUST:

- Include tests that verify the feature/fix (written first per TDD)
- Pass all existing tests (no regressions)
- Meet code quality standards (automated linting, no warnings)
- Include accessibility verification for UI changes
- Document any deviations from established patterns with justification

### Testing Gates

Before merge, verify:

- ✅ All unit tests pass
- ✅ Integration tests pass for affected components
- ✅ No test coverage regressions
- ✅ Accessibility tests pass (if UI changes)
- ✅ Performance tests pass (if performance-critical changes)

## Governance

### Constitution Authority

This constitution supersedes all other development practices and guidelines. When conflicts arise between this constitution and other documentation, the constitution takes precedence.

### Amendment Process

Amendments require:

1. **Documented Proposal**: Clear rationale for change with examples
2. **Impact Analysis**: Assessment of affected code and templates
3. **Team Approval**: Consensus among project stakeholders
4. **Migration Plan**: Strategy for updating existing code (if applicable)
5. **Version Bump**: Semantic versioning applied (MAJOR for breaking changes, MINOR for additions, PATCH for clarifications)

### Compliance Verification

- All pull requests MUST verify constitution compliance
- Complexity violations (deviating from simplicity principles) MUST be explicitly justified in PR description
- Constitution violations require approval exception or PR rejection

### Version Control

Constitution changes tracked via semantic versioning:

- **MAJOR**: Backward-incompatible principle removals or redefinitions
- **MINOR**: New principles added or materially expanded guidance
- **PATCH**: Clarifications, wording improvements, non-semantic refinements

**Version**: 1.0.0 | **Ratified**: 2026-01-03 | **Last Amended**: 2026-01-03
