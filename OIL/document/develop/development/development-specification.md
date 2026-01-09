# Development Specification Document

## 1. Overview

This document outlines the comprehensive development standards for the BerryAIGen.Toolkit project. It provides guidelines for coding, documentation, version control, and issue management to ensure consistency, quality, and maintainability across the entire codebase.

### 1.1 Purpose

The purpose of this document is to:
- Establish clear standards for all aspects of development
- Ensure consistency across the codebase
- Facilitate collaboration among team members
- Improve code quality and maintainability
- Provide guidance for new team members

### 1.2 Scope

This document applies to all developers working on the BerryAIGen.Toolkit project, including:
- Core team members
- Contributors
- Maintainers

It covers all aspects of development, including:
- Code writing and formatting
- Documentation practices
- Version control and commit messages
- Issue reporting and management

## 2. Development Guidelines

### 2.1 Code Style

#### 2.1.1 C# Coding Standards

##### Naming Conventions

- **Classes and Interfaces**: Use PascalCase, descriptive noun-based names. Interfaces should start with "I".
  ```csharp
  // Good
  public class ImageProcessor { }
  public interface IImageService { }
  
  // Bad
  public class imgProc { }
  public interface ImageService { } // Missing "I" prefix
  ```

- **Methods and Functions**: Use PascalCase, verb-based names that describe the action. Include descriptive parameters.
  ```csharp
  // Good
  public void ProcessImage(string filePath) { }
  public async Task<ImageMetadata> ExtractMetadataAsync(string filePath) { }
  
  // Bad
  public void DoStuff(string f) { }
  ```

- **Variables and Fields**: Use camelCase for local variables and private fields (with underscore prefix). Use PascalCase for public fields and properties. Constants in SCREAMING_SNAKE_CASE.
  ```csharp
  // Good
  private string _filePath;
  public string FilePath { get; set; }
  private const int MAX_RETRY_COUNT = 3;
  
  // Bad
  private string filePath; // Missing underscore
  public string filepath { get; set; } // Incorrect casing
  ```

##### Formatting

- **Indentation**: 4 spaces per indentation level, no tabs
- **Line Length**: Maximum 120 characters
- **Braces**: Allman style (new line for opening and closing braces)
- **Spaces**: Single space after keywords, around operators, and after commas

#### 2.1.2 XAML Coding Standards

##### Naming Conventions

- **Controls**: PascalCase, meaningful descriptive names
  ```xaml
  <!-- Good -->
  <Button x:Name="SearchButton" Content="Search" />
  
  <!-- Bad -->
  <Button x:Name="btn1" Content="Search" />
  ```

##### Formatting

- **Indentation**: 4 spaces per indentation level
- **Element Formatting**: Close tags on same line for simple elements, separate lines for complex elements with children
- **Attribute Alignment**: Align attributes for readability

### 2.2 File Organization

- Follow the project structure as defined in `develop.md`
- Keep related code together in appropriate directories
- Limit file size preferably under 500 lines
- Use descriptive file names that reflect their content

### 2.3 Modular Requirements

- Follow the modular architecture as described in the system design
- Use appropriate design patterns (MVVM, Service Locator, Repository, Command)
- Minimize dependencies between modules
- Use interfaces to decouple components

## 3. Comment Standards

### 3.1 Documentation Comments

- **XML Documentation Comments**: Use for all public members
  - Include summary, parameters, returns, and exceptions
  - Use `<see>` and `<seealso>` tags for cross-references
  ```csharp
  /// <summary>
  /// Extracts metadata from an image file.
  /// </summary>
  /// <param name="filePath">The path to the image file.</param>
  /// <returns>The extracted metadata.</returns>
  /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
  public async Task<ImageMetadata> ExtractMetadataAsync(string filePath)
  {
      // Implementation
  }
  ```

### 3.2 Inline Comments

- Use sparingly and only when necessary
- Explain why, not what
- Focus on complex logic or non-obvious code
  ```csharp
  // Good: Explains the purpose of the delay
  await Task.Delay(1000); // Wait for file system to stabilize
  
  // Bad: States the obvious
  int x = 5; // Assign 5 to x
  ```

### 3.3 Comment Location

- Place comments on a separate line above the code they describe
- Use consistent indentation with the code
- Avoid end-of-line comments except for short explanations

## 4. Commit Message Guidelines

### 4.1 Commit Message Structure

```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

### 4.2 Type

The commit type must be one of the following:

| Type | Description |
|------|-------------|
| **feat** | New feature |
| **fix** | Bug fix |
| **docs** | Documentation changes |
| **style** | Code style changes (formatting, etc.) |
| **refactor** | Code refactoring (no functional changes) |
| **perf** | Performance improvements |
| **test** | Testing changes |
| **build** | Build system or dependency changes |
| **ci** | CI/CD configuration changes |
| **chore** | Other changes (e.g., maintenance tasks) |

### 4.3 Scope

The scope should describe the affected component or module:

```
feat(thumbnail-service): add batch thumbnail generation
fix(search-service): correct model search logic
docs(ui-ux): update theming documentation
```

### 4.4 Description

- Short summary of the change (max 50 characters)
- Written in imperative mood (e.g., "Add feature" not "Added feature")
- First letter lowercase
- No trailing period

### 4.5 Body (Optional)

- More detailed explanation of the change
- Wrap at 72 characters
- Explain what and why, not how

### 4.6 Footer (Optional)

- Used for breaking changes and issue references
- Breaking changes: `BREAKING CHANGE: description`
- Issue references: `Closes #123`, `Fixes #456`, `Resolves #789`

### 4.7 Examples

```
# Simple commit
feat(metadata-scanner): add continuous scanning mode

# Commit with body and footer
fix(database): resolve deadlock in query execution

The fix ensures proper transaction handling when multiple queries are 
executed simultaneously, preventing deadlocks that were occurring 
during high-load scenarios.

Closes #246
```

## 5. Issue Reporting Standards

### 5.1 Issue Types

| Type | Description |
|------|-------------|
| **Bug** | Something isn't working as expected |
| **Feature Request** | New feature or enhancement |
| **Documentation** | Documentation issue or improvement |
| **Performance** | Performance-related issue |
| **Security** | Security vulnerability |
| **Question** | General question or clarification |

### 5.2 Issue Template

#### Bug Report

```markdown
## Bug Report

### Description
A clear and concise description of what the bug is.

### Steps to Reproduce
1. Go to '...'
2. Click on '...'
3. Scroll down to '...'
4. See error

### Expected Behavior
A clear and concise description of what you expected to happen.

### Actual Behavior
What actually happened.

### Environment
- OS: [e.g., Windows 11]
- Version: [e.g., v1.2.0]
- .NET Version: [e.g., .NET 8.0]

### Screenshots
If applicable, add screenshots to help explain your problem.

### Additional Context
Add any other context about the problem here.
```

#### Feature Request

```markdown
## Feature Request

### Description
A clear and concise description of the feature you'd like to see.

### Motivation
Why is this feature important? What problem does it solve?

### Proposed Solution
A clear and concise description of how you think the feature should work.

### Alternatives Considered
Any alternative solutions or features you've considered.

### Additional Context
Add any other context or screenshots about the feature request here.
```

### 5.3 Label Usage

#### Priority Labels

| Label | Description |
|-------|-------------|
| `priority: critical` | Critical issue affecting core functionality |
| `priority: high` | High priority, needs attention soon |
| `priority: medium` | Medium priority, can be scheduled |
| `priority: low` | Low priority, backlog item |

#### Component Labels

- `component: ui` - User interface
- `component: search` - Search functionality
- `component: metadata` - Metadata extraction
- `component: database` - Database-related
- `component: thumbnail` - Thumbnail generation
- `component: documentation` - Documentation

#### Status Labels

- `status: in-progress` - Currently being worked on
- `status: review` - Awaiting review
- `status: blocked` - Blocked by another issue
- `status: backlog` - In the backlog

### 5.4 Issue Resolution Process

1. **Issue Creation**: Submit an issue using the appropriate template
2. **Triage**: Issue is reviewed and labeled by maintainers
3. **Assignment**: Issue is assigned to a team member
4. **Implementation**: Work is done to resolve the issue
5. **Verification**: Issue is tested and verified
6. **Closure**: Issue is closed with appropriate resolution

## 6. Document Version Control and Update Mechanism

### 6.1 Versioning

This document follows semantic versioning:
- **Major Version**: Significant changes to the standards
- **Minor Version**: New sections or substantial updates to existing sections
- **Patch Version**: Minor corrections or clarifications

Current Version: 1.0.0

### 6.2 Update Process

1. **Request for Change**: Any team member can request changes to this document
2. **Review**: Changes are reviewed by the team lead or architecture team
3. **Approval**: Changes are approved by at least two senior team members
4. **Update**: Document is updated with the changes
5. **Notification**: Team members are notified of the changes

### 6.3 Version History

| Version | Date | Changes | Author |
|---------|------|---------|--------|
| 1.0.0 | YYYY-MM-DD | Initial version | BerryAIGen Team |

## 7. Inspection Methods and Tool Recommendations

### 7.1 Code Quality Tools

| Tool | Purpose | Configuration |
|------|---------|---------------|
| **Roslyn Analyzers** | Static code analysis | Enabled in project settings |
| **StyleCop** | Code style enforcement | Custom ruleset configured |
| **SonarQube** | Comprehensive code quality | CI/CD integration |
| **ReSharper** | Code analysis and refactoring | Recommended IDE extension |
| **Visual Studio Code Analysis** | Built-in code analysis | Enabled in project settings |

### 7.2 Code Review Process

1. **Pull Request Creation**: Create a PR with clear description of changes
2. **Review Assignment**: Assign 1-2 reviewers
3. **Review Checklist**: Reviewers use the following checklist:
   - [ ] Follows coding standards
   - [ ] Proper documentation
   - [ ] Tests added/updated
   - [ ] No breaking changes without proper handling
   - [ ] Code compiles and passes build
   - [ ] All tests pass
4. **Feedback**: Reviewers provide constructive feedback
5. **Revisions**: Author addresses all feedback
6. **Approval**: PR is approved by reviewers
7. **Merge**: PR is merged into target branch

### 7.3 Automated Checks

- **CI/CD Pipeline**: Runs on every PR
  - Build verification
  - Unit tests
  - Integration tests
  - Code quality analysis
  - Commit message validation

- **Pre-commit Hooks**: Optional local hooks
  - Code formatting
  - Basic linting
  - Commit message validation

## 8. FAQ

### Q: What should I do if I'm unsure about a particular standard?
A: When in doubt, follow the existing codebase style. If still unsure, ask the team lead or create a discussion in the project's communication channel.

### Q: Can I propose changes to the development standards?
A: Yes, any team member can propose changes. Follow the update process outlined in Section 6.

### Q: Are there any exceptions to the coding standards?
A: Exceptions may be made for legacy code or specific performance-critical sections, but they should be documented and approved by the team lead.

### Q: How often is this document updated?
A: The document is updated as needed, typically when there are significant changes to the project's development processes or when new best practices emerge.

### Q: What tools should I use for development?
A: Recommended tools include Visual Studio 2022, GitHub Desktop or Git CLI, and any of the code quality tools mentioned in Section 7.

## 9. References

- [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [WPF XAML Coding Conventions](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-coding-conventions)
- [Semantic Versioning](https://semver.org/)
- [Keep a Changelog](https://keepachangelog.com/)

## 10. Appendix

### 10.1 Glossary

| Term | Definition |
|------|------------|
| **MVVM** | Model-View-ViewModel, a design pattern for UI applications |
| **CI/CD** | Continuous Integration/Continuous Deployment |
| **SCREAMING_SNAKE_CASE** | All uppercase with underscores between words |
| **PascalCase** | First letter of each word capitalized, no spaces |
| **camelCase** | First letter lowercase, subsequent words capitalized |

### 10.2 Related Documents

- [coding-standards.md](coding-standards.md)
- [version-control.md](version-control.md)
- [build-process.md](build-process.md)
- [testing.md](testing.md)