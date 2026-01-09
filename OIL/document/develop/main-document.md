# BerryAIGen.Toolkit - Main Development Document

## 1. Project Overview

### 1.1 Background

With the rapid growth of AI-generated content, particularly images, there is an increasing need for robust tools to manage and organize these assets. AI-generated images often contain valuable metadata (PNGInfo) that provides insights into their creation, including models used, parameters, and prompts. However, traditional file managers and image viewers lack specialized support for this metadata, making it difficult for users to effectively search, organize, and understand their AI-generated image collections.

BerryAIGen.Toolkit addresses this gap by providing a comprehensive solution specifically designed for AI-generated images, offering advanced metadata extraction, powerful search capabilities, and intuitive organization features.

### 1.2 Project Goals

The primary goals of BerryAIGen.Toolkit are:

- **Comprehensive Metadata Support**: Extract and index detailed metadata from various AI image generation platforms
- **Powerful Search Functionality**: Enable advanced searching across all extracted metadata fields
- **Intuitive Organization**: Provide user-friendly tools for organizing images using ratings, tags, and albums
- **Efficient Performance**: Ensure smooth operation even with large image collections
- **Extensible Architecture**: Design a modular system that can easily support new AI platforms and metadata formats
- **Cross-platform Compatibility**: Support multiple languages and operating systems

### 1.3 Main Features

| Feature Category | Key Features |
|------------------|--------------|
| **Metadata Extraction** | Extracts PNGInfo from AUTOMATIC1111, InvokeAI, NovelAI, Stable Diffusion, EasyDiffusion, Fooocus family, and Stable Swarm |
| **Search & Filtering** | Advanced search capabilities with SQLite Full-Text Search, dynamic query generation, and debouncing for performance |
| **Image Organization** | Ratings, tags, albums, and folder management with archive functionality |
| **Thumbnail Generation** | Efficient, asynchronous thumbnail generation with caching and lazy loading |
| **Batch Processing** | Support for batch operations on images |
| **Multi-language Support** | Internationalization support for multiple languages |
| **External Application Integration** | Support for opening images in external applications |
| **Update Mechanism** | Automatic update checking from GitHub |

## 2. Technical Architecture

### 2.1 Technology Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Core framework |
| WPF | .NET 8.0 | UI framework |
| SQLite | 3.x | Database |
| C# | 12.0 | Programming language |
| XAML | - | UI markup |

### 2.2 Key Dependencies

| Library | Version | Purpose |
|---------|---------|---------|
| Dapper | 2.0.123 | Database access |
| FontAwesome.WPF | 4.7.0.9 | Icon library |
| WPFLocalizeExtension | - | Internationalization |

### 2.3 Architecture Design

BerryAIGen.Toolkit follows a modular, layered architecture with clear separation of concerns:

```
+-----------------------------------------------------------------------+
|                         Presentation Layer                            |
|  (WPF UI, XAML, Controls, Pages)                                      |
+-----------------------------------------------------------------------+
                            |
                            v
+-----------------------------------------------------------------------+
|                         Business Layer                                |
|  (ViewModels, Services, Converters, Commands)                         |
+-----------------------------------------------------------------------+
                            |
                            v
+-----------------------------------------------------------------------+
|                         Data Layer                                    |
|  (Database, Models, Repositories, Migrations)                         |
+-----------------------------------------------------------------------+
                            |
                            v
+-----------------------------------------------------------------------+
|                         External Layer                                |
|  (File System, External Libraries, APIs)                              |
+-----------------------------------------------------------------------+
```

### 2.4 Component Relationships

#### 2.4.1 Core Components

1. **Data Store**: Database implementation and data access layer
2. **Metadata Scanner**: Extracts and processes metadata from AI-generated images
3. **Scanning Service**: Manages folder scanning and monitoring
4. **Search Service**: Implements advanced search functionality
5. **Thumbnail Service**: Generates and manages thumbnails
6. **Service Locator**: Handles dependency management
7. **UI Controls**: Custom WPF components for the user interface

#### 2.4.2 Module Structure

The project is organized into several modules, each with specific responsibilities:

- **BerryAIGen.Civitai**: Civitai API integration
- **BerryAIGen.Common**: Common utilities and shared code
- **BerryAIGen.Database**: Database implementation
- **BerryAIGen.Github**: GitHub API integration for updates
- **BerryAIGen.IO**: File I/O operations
- **BerryAIGen.Scripting**: Scripting support
- **BerryAIGen.Toolkit**: Main WPF application
- **BerryAIGen.Updater**: Application updater component
- **BerryAIGen.Video**: Video support (future feature)

## 3. Key Technical Points

### 3.1 Metadata Extraction

**Challenge**: AI-generated images from different platforms use varying metadata formats, making extraction complex.

**Solution**: Implemented a modular metadata extraction system that can handle multiple formats:

- **AUTOMATIC1111**: Extracts metadata from the standard PNGInfo format
- **InvokeAI**: Supports Dream/sd-metadata/invokeai_metadata formats
- **NovelAI**: Special handling for NovelAI-specific metadata
- **Fooocus family**: Support for RuinedFooocus, Fooocus, FooocusMRE
- **Stable Swarm**: Integration with Stable Swarm metadata

**Implementation**: The `MetadataScanner` class in `BerryAIGen.Common` uses a factory pattern to create appropriate extractors based on the detected metadata format.

### 3.2 Thread Safety in UI Operations

**Challenge**: WPF UI elements can only be accessed from the UI thread, but many operations (thumbnail generation, metadata extraction) are performed in background threads.

**Solution**: Implemented robust thread safety mechanisms:

- Use `Dispatcher.Invoke` to marshal UI access from background threads
- Implement proper thread checking in UI-related methods
- Use async/await pattern for asynchronous operations
- Example:
  ```csharp
  public void ReloadThumbnailsView()
  {
      if (!Dispatcher.CheckAccess())
      {
          Dispatcher.Invoke(() => ReloadThumbnailsView());
          return;
      }
      // UI operations here
  }
  ```

### 3.3 Search Performance Optimization

**Challenge**: Providing fast search across large collections of images with extensive metadata.

**Solution**: Implemented a multi-layered search optimization approach:

- **SQLite Full-Text Search**: For fast text searching across prompts and metadata
- **Dynamic Query Generation**: Builds optimized SQL queries based on user input
- **Debouncing**: Delays search execution during user input to improve responsiveness
- **Index Optimization**: Properly indexed database columns for common search operations

### 3.4 Thumbnail Generation and Caching

**Challenge**: Generating thumbnails for large image collections while maintaining UI responsiveness.

**Solution**: Implemented an efficient thumbnail system:

- **Background Processing**: Thumbnails generated in background threads
- **Caching**: Generated thumbnails cached for quick access
- **Lazy Loading**: Thumbnails loaded on demand as they come into view
- **Cache Management**: Automatic cache size management based on available resources

### 3.5 Update Checking Mechanism

**Challenge**: Ensuring users have access to the latest updates while minimizing performance impact.

**Solution**: Implemented a robust update checking system:

- **GitHub API Integration**: Uses GitHub API to fetch latest releases
- **Configurable**: User can enable/disable auto-update checks
- **Timeout**: Update checks have a 3-second timeout to prevent UI hang
- **Non-intrusive**: Failed checks silently continue to main application
- **User Notification**: Clear notification when updates are available

## 4. Complete Directory Structure

The project follows a well-organized directory structure that separates different types of content, making it easy to locate and access specific files.

### 4.1 Document Structure

```
document/develop/
├── api-reference/            # API documentation
│   ├── database-api.md       # Database API reference
│   ├── public-interfaces.md  # Public interface documentation
│   └── service-api.md        # Service API reference
├── architecture/             # Architecture documentation
│   ├── data-flow.md          # Data flow diagrams and descriptions
│   ├── dependency-injection.md # Dependency injection implementation
│   ├── modular-structure.md  # Modular architecture details
│   └── system-design.md      # Overall system design
├── challenges/               # Technical challenges and solutions
│   ├── metadata-extraction.md # Metadata extraction challenges
│   ├── performance-optimization.md # Performance tuning
│   ├── scalability.md        # Scalability considerations
│   └── thread-safety.md      # Thread safety implementation
├── components/               # Component-specific documentation
│   ├── data-store.md         # Data store implementation
│   ├── main-window.md        # Main window design
│   ├── metadata-scanner.md   # Metadata scanner details
│   ├── scanning-service.md   # Scanning service implementation
│   ├── search-service.md     # Search service details
│   ├── service-locator.md    # Service locator pattern
│   ├── thumbnail-service.md  # Thumbnail generation
│   └── ui-controls.md        # Custom UI controls
├── database/                 # Database documentation
│   ├── migrations.md         # Database migration strategies
│   ├── performance.md        # Database performance optimization
│   ├── queries.md            # Common database queries
│   └── schema.md             # Database schema design
├── Dev-Log/                  # Development logs
├── development/              # Development standards and processes
│   ├── build-process.md      # Build configuration and process
│   ├── coding-standards.md   # Coding conventions and standards
│   ├── development-specification.md # Comprehensive development spec
│   ├── testing.md            # Testing strategies and practices
│   └── version-control.md    # Version control guidelines
├── initialization/           # Initialization documentation
│   └── initial-startup-checks.md # Startup check procedures
├── introduction/             # Project introduction and overview
│   ├── develop.md            # Development guide
│   ├── main-document.md      # This main document
│   ├── overview.md           # Project overview
│   └── index.md              # Documentation index
├── logs/                     # Log directory for future use
├── modules/                  # Module-specific documentation
│   ├── berryaigen-civitai.md # Civitai module documentation
│   ├── berryaigen-common.md  # Common module documentation
│   ├── berryaigen-database.md # Database module documentation
│   ├── berryaigen-github.md  # GitHub module documentation
│   ├── berryaigen-io.md      # IO module documentation
│   ├── berryaigen-scripting.md # Scripting module documentation
│   └── berryaigen-toolkit.md # Main toolkit module documentation
└── ui-ux/                    # UI/UX documentation
    ├── localization.md       # Internationalization support
    ├── mvvm-pattern.md       # MVVM implementation
    ├── theming.md            # UI theming and styling
    └── user-interactions.md  # User interaction patterns
```

### 4.2 Source Code Structure

```
BerryAIGen.Toolkit/
├── BerryAIGen.Civitai/       # Civitai API integration
├── BerryAIGen.Common/        # Common utilities and interfaces
├── BerryAIGen.Data/          # Data models and interfaces
├── BerryAIGen.Database/      # Database implementation (SQLite)
├── BerryAIGen.Github/        # GitHub API integration
├── BerryAIGen.IO/            # File I/O operations
├── BerryAIGen.Scripting/     # Scripting support
├── BerryAIGen.Toolkit/       # Main WPF application
│   ├── Controls/            # Custom WPF controls
│   ├── Converters/          # Value converters
│   ├── Configuration/       # Configuration management
│   ├── Localization/        # Internationalization support
│   ├── Models/              # View models
│   ├── Pages/               # Application pages
│   ├── Services/            # Business services
│   ├── Thumbnails/          # Thumbnail generation
│   └── Themes/              # UI themes and resources
├── BerryAIGen.Updater/       # Application updater
├── BerryAIGen.Video/         # Video support
├── TestBed/                  # Test application
├── TestHarness/              # Unit tests
└── BerryAIGen.sln           # Main solution file
```

## 5. Key Documentation References

| Document Category | Key Files | Purpose |
|-------------------|-----------|---------|
| **Development Standards** | development/development-specification.md | Comprehensive development guidelines |
| **Coding Standards** | development/coding-standards.md | Code style and naming conventions |
| **Version Control** | development/version-control.md | Git workflow and commit guidelines |
| **Architecture** | architecture/system-design.md | Overall system architecture |
| **Database** | database/schema.md | Database schema design |
| **Components** | components/data-store.md | Data store implementation |
| **Initialization** | initialization/initial-startup-checks.md | Startup check procedures |
| **API Reference** | api-reference/service-api.md | Service API documentation |

## 6. Getting Started with Development

### 6.1 Prerequisites

- Visual Studio 2022 or later
- .NET 8.0 SDK
- Git for version control

### 6.2 Setup Process

1. Clone the repository: `git clone https://github.com/Berry-Wahlberg/AIGenManager.git`
2. Open `BerryAIGen.sln` in Visual Studio
3. Restore NuGet packages
4. Build the solution
5. Run the `BerryAIGen.Toolkit` project

### 6.3 Development Workflow

1. Follow the coding standards in `development/coding-standards.md`
2. Create feature branches for new functionality
3. Write unit tests for new features
4. Follow the commit message guidelines in `development/version-control.md`
5. Create pull requests for review

## 7. Conclusion

BerryAIGen.Toolkit provides a comprehensive solution for managing AI-generated images, addressing the unique challenges of this rapidly growing content type. Its modular architecture, advanced metadata extraction, and powerful search capabilities make it an invaluable tool for anyone working with AI-generated images.

The project is designed to be extensible, allowing for easy addition of support for new AI platforms and metadata formats. With its well-documented codebase and clear development processes, BerryAIGen.Toolkit is well-positioned for continued growth and improvement.

For more detailed information on specific aspects of the project, please refer to the individual documentation files indexed in this document.