# Project File Organization Specification

## 1. Overview
This document defines the comprehensive file organization standard for the AIGenManager project. It establishes guidelines for directory structure, naming conventions, documentation requirements, and code organization to ensure a consistent, maintainable, and scalable codebase.

## 2. Directory Structure

### Core Directory Layout
```
├── src/                    # Main source code directory
│   ├── Presentation/       # Presentation layer (UI components and interfaces)
│   │   ├── Electron/       # Electron.NET web-based UI
│   │   └── Wpf/            # WPF desktop UI
│   ├── Business/           # Business logic layer
│   ├── Data/               # Data access layer
│   │   ├── IO/             # File I/O operations
│   │   └── Database/       # Database operations
│   ├── Infrastructure/     # External services and integrations
│   │   ├── Civitai/        # Civitai API integration
│   │   ├── Github/         # GitHub API integration
│   │   └── Video/          # Video processing
│   └── Common/             # Shared code, utilities, and models
├── tests/                  # Test projects
│   ├── Unit/               # Unit tests
│   ├── Integration/        # Integration tests
│   └── E2E/                # End-to-end tests
├── tools/                  # Build and development tools
├── docs/                   # Documentation
├── samples/                # Sample code and examples
└── lib/                    # Third-party libraries
```

### Directory Naming Rules
- Use **PascalCase** for all directory names except for special cases like `src/`, `tests/`, `tools/`, etc.
- Be **descriptive** and **specific** about the directory's purpose
- Avoid **abbreviations** unless they are widely recognized (e.g., `IO` for Input/Output)
- Keep directory structure **shallow** where possible (max 3-4 levels deep)

## 3. Naming Conventions

### Project Names
- **Format**: `BerryAIGen.{Layer}.{Component}.csproj`
- **Examples**:
  - `BerryAIGen.Presentation.Electron.csproj`
  - `BerryAIGen.Data.Database.csproj`
  - `BerryAIGen.Infrastructure.Civitai.csproj`

### File Names
- **Classes**: `PascalCase.cs` (e.g., `UserService.cs`, `DatabaseContext.cs`)
- **Interfaces**: `I{PascalCase}.cs` (e.g., `IUserRepository.cs`, `ILogger.cs`)
- **Enums**: `PascalCase.cs` (e.g., `UserRole.cs`, `LogLevel.cs`)
- **Tests**: `{ClassName}Tests.cs` (e.g., `UserServiceTests.cs`)
- **Configuration**: `appsettings.{Environment}.json` (e.g., `appsettings.Development.json`)
- **CSS/JS**: `kebab-case.(css|js)` (e.g., `site.css`, `navigation.js`)

### Folder Names
- **Use PascalCase** for folders containing C# code
- **Use kebab-case** for web-related folders (HTML, CSS, JS, etc.)
- **Avoid spaces** in all folder and file names

## 4. Documentation Requirements

### Project Documentation
- Each project must have a `README.md` file at its root
- `README.md` should include:
  - Project purpose and description
  - Key features and functionality
  - Dependencies
  - Usage examples
  - Contributing guidelines

### Code Documentation
- **Classes and Interfaces**: Must have XML documentation with `<summary>` tags
- **Public Methods**: Must have XML documentation with `<summary>`, `<param>`, and `<returns>` tags as appropriate
- **Internal Methods**: Should have XML documentation for complex functionality
- **Private Methods**: May have XML documentation for complex logic

### Solution Documentation
- `PROJECT_FILE_SPECIFICATION.md` (this file): Defines file organization standards
- `ARCHITECTURE.md`: Describes the overall architecture and design patterns
- `CONTRIBUTING.md`: Guidelines for contributing to the project
- `CHANGELOG.md`: Record of changes, updates, and version history

## 5. Code Organization Rules

### Layer Dependencies
- **Presentation** → **Business** → **Data** → **Infrastructure** → **Common**
- **No circular dependencies** allowed between layers
- **Common** can be referenced by any layer
- **Infrastructure** components are accessed through interfaces defined in higher layers

### Class Organization
```csharp
// 1. Using directives (sorted alphabetically)
using System;
using System.Collections.Generic;

// 2. Namespace
namespace BerryAIGen.Business.Services
{
    // 3. Public enum (if applicable)
    public enum UserStatus
    {
        Active,
        Inactive
    }

    // 4. Public interface (if applicable)
    public interface IUserService
    {
        User GetUser(int id);
        void SaveUser(User user);
    }

    // 5. Public class
    public class UserService : IUserService
    {
        // 6. Private fields
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        // 7. Constructor
        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // 8. Public methods (sorted alphabetically)
        public User GetUser(int id)
        {
            // Implementation
        }

        public void SaveUser(User user)
        {
            // Implementation
        }

        // 9. Private methods (sorted alphabetically)
        private void ValidateUser(User user)
        {
            // Implementation
        }
    }
}
```

### Code Style
- Use **C# 12** features where appropriate
- Follow **Microsoft's C# Coding Conventions**
- Use **implicit typing** (`var`) where the type is obvious
- Use **expression-bodied members** for simple methods and properties
- Use **nullable reference types** to improve type safety
- Keep methods **short** and **focused** (max 50 lines per method)
- Avoid **magic numbers** and **hard-coded values**

## 6. Build Configuration

### Project Structure
- Each project must have a unique `AssemblyName` matching the project name
- Use **SDK-style** project files
- Define **target frameworks** in the project file
- Use **PackageReference** for all NuGet dependencies

### Build Scripts
- Build scripts must be placed in the `tools/` directory
- Use **PowerShell** for Windows-specific scripts (`*.ps1`)
- Use **Bash** for cross-platform scripts (`*.sh`)
- Include scripts for:
  - Building the solution
  - Running tests
  - Creating release packages
  - Cleaning the build

## 7. Obsolete and Redundant Content

### Removal Guidelines
- **Obsolete code**: Mark with `[Obsolete]` attribute before removal
- **Redundant files**: Remove immediately if they serve no purpose
- **Dead code**: Remove if it's not called by any other code
- **Unused dependencies**: Remove from project files
- **Old test projects**: Remove if they're no longer maintained

### Removal Process
1. Identify obsolete/redundant content
2. Verify with team members if unsure
3. Create a backup branch if necessary
4. Remove the content
5. Run all tests to ensure functionality is maintained
6. Update documentation if needed

## 8. Version Control Guidelines

### Gitignore
- Include a comprehensive `.gitignore` file at the root
- Ignore:
  - Build outputs (`bin/`, `obj/`)
  - IDE-specific files (`*.suo`, `*.user`, `.vs/`)
  - Package manager files (`packages/`, `node_modules/`)
  - Log files (`*.log`)
  - Environment-specific files (`appsettings.local.json`)

### Branching Strategy
- `main`: Production-ready code
- `develop`: Integration branch for new features
- `feature/`: Feature branches
- `bugfix/`: Bug fix branches
- `release/`: Release preparation branches

## 9. Application of This Specification

### Restructuring Process
1. **Analyze** current project structure
2. **Create** new directory structure
3. **Move** files to their new locations
4. **Update** project references and dependencies
5. **Remove** obsolete or redundant content
6. **Rename** files/folders to adhere to naming conventions
7. **Update** build configuration and scripts
8. **Verify** all files adhere to the new standard
9. **Test** the restructured project

### Enforcement
- All new files must adhere to this specification
- Existing files will be migrated gradually
- Code reviews will include checks for adherence to this standard
- Build processes may include validation steps

## 10. Exceptions

- **Third-party libraries**: May follow their own naming conventions
- **Legacy code**: May be exempt temporarily during migration
- **Specialized tools**: May have unique directory structures based on their requirements

## 11. Version History

| Version | Date       | Author | Description                          |
|---------|------------|--------|--------------------------------------|
| 1.0     | 2026-01-08 | System | Initial specification                |

## 12. Approval

| Role       | Name | Date       | Signature |
|------------|------|------------|-----------|
| Technical Lead |      |            |           |
| Project Manager |      |            |           |
| Developer Representative |      |            |           |
