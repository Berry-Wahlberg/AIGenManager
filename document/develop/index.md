# Developer Documentation Wiki

Welcome to the BerryAIGen.Toolkit Developer Documentation Wiki. This directory contains comprehensive documentation for developers working on the project.

## Documentation Structure

### Core Documentation

- **[Development Specification](development/development-specification.md)** - Comprehensive development standards including coding guidelines, commit messages, and issue reporting
- **[Coding Standards](development/coding-standards.md)** - Detailed coding standards for C# and XAML
- **[Version Control](development/version-control.md)** - Git workflow, branching strategy, and commit conventions
- **[Build Process](development/build-process.md)** - Build configuration and CI/CD pipeline
- **[Testing](development/testing.md)** - Testing strategies and best practices

### Architecture

- **[System Design](architecture/system-design.md)** - Overall system architecture and design principles
- **[Modular Structure](architecture/modular-structure.md)** - Project modularization and component relationships
- **[Dependency Injection](architecture/dependency-injection.md)** - Dependency management and service locator pattern
- **[Data Flow](architecture/data-flow.md)** - Data movement and processing throughout the application

### Components

- **[Data Store](components/data-store.md)** - Database implementation and data access layer
- **[Metadata Scanner](components/metadata-scanner.md)** - Metadata extraction and processing
- **[Scanning Service](components/scanning-service.md)** - Folder scanning and monitoring
- **[Search Service](components/search-service.md)** - Search functionality implementation
- **[Service Locator](components/service-locator.md)** - Service management and dependency resolution
- **[Thumbnail Service](components/thumbnail-service.md)** - Thumbnail generation and management
- **[UI Controls](components/ui-controls.md)** - Custom UI components and controls

### Database

- **[Schema](database/schema.md)** - Database schema design and table relationships
- **[Queries](database/queries.md)** - Common database queries and optimization
- **[Performance](database/performance.md)** - Database performance considerations and optimizations
- **[Migrations](database/migrations.md)** - Database migration strategies and practices

### Challenges

- **[Metadata Extraction](challenges/metadata-extraction.md)** - Challenges in extracting metadata from various AI image formats
- **[Performance Optimization](challenges/performance-optimization.md)** - Performance tuning and optimization techniques
- **[Scalability](challenges/scalability.md)** - Scalability considerations for large image collections
- **[Thread Safety](challenges/thread-safety.md)** - Handling thread safety in UI operations

### Modules

- **[BerryAIGen.Civitai](modules/berryaigen-civitai.md)** - Civitai API integration
- **[BerryAIGen.Common](modules/berryaigen-common.md)** - Common utilities and shared code
- **[BerryAIGen.Database](modules/berryaigen-database.md)** - Database implementation
- **[BerryAIGen.Github](modules/berryaigen-github.md)** - GitHub API integration
- **[BerryAIGen.IO](modules/berryaigen-io.md)** - File I/O operations
- **[BerryAIGen.Scripting](modules/berryaigen-scripting.md)** - Scripting support
- **[BerryAIGen.Toolkit](modules/berryaigen-toolkit.md)** - Main WPF application
- **[BerryAIGen.Updater](modules/berryaigen-updater.md)** - Application updater
- **[BerryAIGen.Video](modules/berryaigen-video.md)** - Video support

### UI/UX

- **[MVVM Pattern](ui-ux/mvvm-pattern.md)** - MVVM implementation guidelines
- **[Localization](ui-ux/localization.md)** - Internationalization and localization practices
- **[Theming](ui-ux/theming.md)** - UI theming and styling
- **[User Interactions](ui-ux/user-interactions.md)** - User interaction patterns and guidelines

### Introduction

- **[Project Overview](overview.md)** - Comprehensive project overview and introduction
- **[Main Development Document](main-document.md)** - Complete development documentation with technical details
- **[Development Guide](develop.md)** - Detailed development documentation

### Initialization

- **[Initial Startup Checks](initialization/initial-startup-checks.md)** - Automatic checks performed during the first application startup

### Logs

- **[Development Logs](Dev-Log/)** - Development progress and activity logs

## Getting Started

1. Review the **[Development Specification](development/development-specification.md)** for overall development standards
2. Check the **[Coding Standards](development/coding-standards.md)** before writing code
3. Understand the **[Version Control](development/version-control.md)** workflow for committing changes
4. Refer to the **[Architecture](architecture/)** documents to understand the system design
5. Explore component-specific documentation for detailed implementation details

## Document Conventions

- All documentation is written in English
- Use Markdown format for all documentation files
- Follow the existing structure and naming conventions
- Include code examples where appropriate
- Update documentation when making changes to the codebase

## Updating Documentation

1. Make changes to the relevant documentation files
2. Follow the same version control process as code changes
3. Ensure all documentation is accurate and up-to-date
4. Add new documentation files for new features or components

## Contact

For questions or suggestions about the documentation, please create an issue in the GitHub repository.