# BerryAIGen.Toolkit - Developer Documentation

## Project Overview

BerryAIGen.Toolkit is a comprehensive image metadata indexer and viewer designed specifically for AI-generated images. It provides powerful tools to help users organize, search, and manage their growing collection of AI-generated media. This documentation provides detailed technical information for developers working on the project.

## Vision

The vision of BerryAIGen.Toolkit is to create a user-friendly, performant, and extensible tool for managing AI-generated images, with a focus on:
- Comprehensive metadata extraction and indexing
- Powerful search and filtering capabilities
- Intuitive user interface
- Extensibility for future features and metadata formats

## Key Features

- **Metadata Extraction**: Extracts detailed metadata (PNGInfo) from various AI image generation platforms
- **Search and Filtering**: Advanced search capabilities across all extracted metadata
- **Image Organization**: Ratings, tags, and albums for organizing images
- **Thumbnail Generation**: Efficient thumbnail generation for quick preview
- **Batch Processing**: Support for batch operations on images
- **Multi-language Support**: Internationalization support for different languages

## Architecture Overview

BerryAIGen.Toolkit follows a modular architecture with clear separation of concerns:

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?                         Presentation Layer                         鈹?
鈹?  (WPF UI, XAML, Controls, Pages)                                   鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
                            鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈻尖攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?                        Business Layer                              鈹?
鈹?  (ViewModels, Services, Converters, Commands)                      鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
                            鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈻尖攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?                        Data Layer                                  鈹?
鈹?  (Database, Models, Repositories, Migrations)                     鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
                            鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈻尖攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?                        External Layer                              鈹?
鈹?  (File System, External Libraries, APIs)                          鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

## Project Structure

```
BerryAIGen.Toolkit/
鈹溾攢鈹€ BerryAIGen.Civitai/          # Civitai integration
鈹溾攢鈹€ BerryAIGen.Common/           # Common utilities and interfaces
鈹溾攢鈹€ BerryAIGen.Data/             # Data models and interfaces
鈹溾攢鈹€ BerryAIGen.Database/         # Database implementation (SQLite)
鈹溾攢鈹€ BerryAIGen.Github/           # GitHub integration
鈹溾攢鈹€ BerryAIGen.IO/               # File I/O operations
鈹溾攢鈹€ BerryAIGen.Scripting/        # Scripting support
鈹溾攢鈹€ BerryAIGen.Toolkit/          # Main application (WPF)
鈹?  鈹溾攢鈹€ Controls/               # Custom WPF controls
鈹?  鈹溾攢鈹€ Converters/             # Value converters
鈹?  鈹溾攢鈹€ Configuration/          # Configuration management
鈹?  鈹溾攢鈹€ Localization/           # Internationalization support
鈹?  鈹溾攢鈹€ Models/                 # View models
鈹?  鈹溾攢鈹€ Pages/                  # Application pages
鈹?  鈹溾攢鈹€ Services/               # Business services
鈹?  鈹溾攢鈹€ Thumbnails/             # Thumbnail generation
鈹?  鈹斺攢鈹€ Themes/                 # UI themes and resources
鈹溾攢鈹€ BerryAIGen.Updater/          # Application updater
鈹溾攢鈹€ BerryAIGen.Video/            # Video support
鈹溾攢鈹€ TestBed/                     # Test application
鈹斺攢鈹€ TestHarness/                 # Unit tests
```

## Technical Stack

### Core Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Core framework |
| WPF | .NET 8.0 | UI framework |
| SQLite | 3.x | Database |
| C# | 12.0 | Programming language |
| XAML | - | UI markup |

### Key Dependencies

| Library | Version | Purpose |
|---------|---------|---------|
| Dapper | 2.0.123 | Database access |
| FontAwesome.WPF | 4.7.0.9 | Icon library |
| WPFLocalizeExtension | - | Internationalization |

## Navigation Guide

This documentation is organized into the following sections:

### 1. [Architecture](./architecture/)
- **[System Design](./architecture/system-design.md)**: Overall system architecture and design patterns
- **[Modular Structure](./architecture/modular-structure.md)**: Detailed explanation of the project's modular organization
- **[Data Flow](./architecture/data-flow.md)**: Data flow diagrams and explanations
- **[Dependency Injection](./architecture/dependency-injection.md)**: ServiceLocator pattern and service management

### 2. [Project Modules](./modules/)
- **[BerryAIGen.Civitai](./modules/berryaigen-civitai.md)**: Civitai integration module
- **[BerryAIGen.Common](./modules/berryaigen-common.md)**: Common utilities and interfaces
- **[BerryAIGen.Database](./modules/berryaigen-database.md)**: Database implementation
- **[BerryAIGen.Github](./modules/berryaigen-github.md)**: GitHub integration
- **[BerryAIGen.IO](./modules/berryaigen-io.md)**: File I/O operations
- **[BerryAIGen.Scripting](./modules/berryaigen-scripting.md)**: Scripting support
- **[BerryAIGen.Toolkit](./modules/berryaigen-toolkit.md)**: Main application (WPF)
- **[BerryAIGen.Updater](./modules/berryaigen-updater.md)**: Application updater
- **[BerryAIGen.Video](./modules/berryaigen-video.md)**: Video support

### 3. [Key Components](./components/)
- **[Main Window](./components/main-window.md)**: Main application window and its functionality
- **[Service Locator](./components/service-locator.md)**: Service registry and dependency management
- **[Data Store](./components/data-store.md)**: Database management and operations
- **[Scanning Service](./components/scanning-service.md)**: File system scanning and image indexing
- **[Search Service](./components/search-service.md)**: Search functionality and implementation
- **[Thumbnail Service](./components/thumbnail-service.md)**: Thumbnail generation and caching
- **[Metadata Scanner](./components/metadata-scanner.md)**: Metadata extraction from images
- **[UI Controls](./components/ui-controls.md)**: Custom UI controls and components

### 4. [Database Design](./database/)
- **[Schema](./database/schema.md)**: Database schema with table descriptions
- **[Migrations](./database/migrations.md)**: Database migration system and best practices
- **[Queries](./database/queries.md)**: Common database queries and patterns
- **[Performance](./database/performance.md)**: Database performance optimization

### 5. [UI/UX Design](./ui-ux/)
- **[MVVM Pattern](./ui-ux/mvvm-pattern.md)**: MVVM implementation details
- **[Theming](./ui-ux/theming.md)**: Theme management and customization
- **[Localization](./ui-ux/localization.md)**: Multi-language support implementation
- **[User Interactions](./ui-ux/user-interactions.md)**: User interaction patterns and best practices

### 6. [Development Practices](./development/)
- **[Coding Standards](./development/coding-standards.md)**: Coding conventions and standards
- **[Testing](./development/testing.md)**: Testing strategies and frameworks
- **[Build Process](./development/build-process.md)**: Build and deployment process
- **[Version Control](./development/version-control.md)**: Git workflow and best practices

### 7. [API Reference](./api-reference/)
- **[Public Interfaces](./api-reference/public-interfaces.md)**: Public APIs and interfaces
- **[Service API](./api-reference/service-api.md)**: Service layer API documentation
- **[Database API](./api-reference/database-api.md)**: Database access API

### 8. [Technical Challenges](./challenges/)
- **[Thread Safety](./challenges/thread-safety.md)**: Thread safety in UI operations
- **[Performance Optimization](./challenges/performance-optimization.md)**: Performance optimization techniques
- **[Metadata Extraction](./challenges/metadata-extraction.md)**: Challenges in metadata extraction
- **[Scalability](./challenges/scalability.md)**: Scalability considerations

## Quick Start Guide for Developers

### Prerequisites
- **IDE**: Visual Studio 2022 or later
- **SDK**: [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (includes the runtime)

### Build Instructions
1. Clone this repository
2. Open the solution file `BerryAIGen.sln` in Visual Studio
3. Build the solution using the **Build** menu or by pressing `Ctrl+Shift+B`
4. Alternatively, run `dotnet build` from the command line in the project root

### Running the Application
1. Set `BerryAIGen.Toolkit` as the startup project
2. Press `F5` to run the application in debug mode
3. Or press `Ctrl+F5` to run without debugging

### Key Development Files
- **Main Application Entry**: `BerryAIGen.Toolkit/App.xaml.cs`
- **Main Window**: `BerryAIGen.Toolkit/MainWindow.xaml.cs`
- **Service Registry**: `BerryAIGen.Toolkit/Services/ServiceLocator.cs`
- **Database Management**: `BerryAIGen.Database/DataStore.cs`

## Contributing Guidelines

When contributing to BerryAIGen.Toolkit, please follow these guidelines:

1. **Branch Naming**: Use descriptive branch names like `feature/metadata-extraction` or `bugfix/search-issue`
2. **Commit Messages**: Write clear, concise commit messages that explain the change
3. **Code Style**: Follow the existing code style and conventions
4. **Testing**: Add appropriate tests for new functionality
5. **Documentation**: Update documentation to reflect changes
6. **Pull Requests**: Create pull requests with clear descriptions of the changes

## License

BerryAIGen.Toolkit is licensed under the MIT License. See the [LICENSE](../LICENSE) file for details.

## Support

For support or questions about the codebase, please refer to the [GitHub Issues](https://github.com/Berry-Wahlberg/AIGenManager/issues) page.

