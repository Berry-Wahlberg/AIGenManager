# Modular Structure

## Overview
This document describes the modular structure of BerryAIGen.Toolkit, detailing each project module, its purpose, and key components. The codebase is organized into multiple independent modules that work together to provide the complete functionality of the application.

## Module Structure

The project consists of several distinct modules, each with its own specific responsibility. This modular design allows for:
- Better separation of concerns
- Easier maintenance and debugging
- Independent development and testing
- Reusability of components across projects

## Core Modules

### 1. BerryAIGen.Civitai

**Purpose**: Integration with the Civitai API for accessing AI models and their metadata.

**Key Components**:
- **CivitaiClient**: Main client for interacting with the Civitai API
- **Models**: Data models representing Civitai entities like Model, ModelVersion, and ModelFile
- **CivitaiRequestException**: Exception handling for API requests

**Key Features**:
- Fetching models from Civitai
- Searching for models by various criteria
- Retrieving model metadata and versions
- Handling API rate limits and errors

### 2. BerryAIGen.Common

**Purpose**: Common utilities, interfaces, and shared components used across multiple modules.

**Key Components**:
- **Query**: Classes for building and executing queries (Filter, NodeFilter, QueryOptions)
- **AppInfo**: Application information like version and settings path
- **Configuration**: Generic configuration management
- **FileUtility**: File system utilities
- **Hashing**: Hash generation utilities
- **Logger**: Logging functionality
- **Model**: Common model representation
- **ModelInfo**: Model information structure
- **SemanticVersion**: Version parsing and comparison
- **UpdateChecker**: Application update checking
- **Utility**: General utility methods

**Key Features**:
- Cross-module shared functionality
- Standardized interfaces for common operations
- Utility functions for file handling, logging, and configuration

### 3. BerryAIGen.Data

**Purpose**: Data models and interfaces for the application.

**Key Components**:
- **Class1**: Placeholder for future data model implementations

**Key Features**:
- Shared data model definitions
- Interfaces for data access

### 4. BerryAIGen.Database

**Purpose**: Database implementation using SQLite, including schema definition, migrations, and data access.

**Key Components**:
- **DataStore**: Main database access class
- **Models**: Database entity models (Album, AlbumImage, Folder, Image, ImageTag, etc.)
- **Migrations**: Database schema migration scripts
- **QueryBuilder**: Classes for constructing database queries
- **SQLite**: SQLite-specific functionality

**Key Features**:
- Database connection management
- Table creation and indexing
- Data retrieval and modification
- Database schema migrations
- Query building and execution

### 5. BerryAIGen.Github

**Purpose**: Integration with GitHub API for accessing releases and other repository information.

**Key Components**:
- **GithubClient**: Main client for interacting with the GitHub API
- **Models**: Data models representing GitHub entities like Release, Asset, and Author

**Key Features**:
- Fetching releases from GitHub
- Checking for application updates
- Downloading release assets

### 6. BerryAIGen.IO

**Purpose**: File I/O operations, including metadata extraction, file scanning, and image processing.

**Key Components**:
- **ComfyUI**: ComfyUI-specific functionality
- **FileParameters**: File parameter definitions
- **HashFunctions**: Hash generation for files
- **Metadata**: Metadata representation
- **MetadataScanner**: Metadata extraction from images
- **ModelScanner**: AI model scanning and identification
- **StealthPng**: Stealth PNG functionality

**Key Features**:
- Extracting metadata from various image formats
- Scanning directories for files
- Generating hashes for files
- Processing AI model files

### 7. BerryAIGen.Scripting

**Purpose**: Scripting support for extending application functionality.

**Key Components**:
- **python/jinja.py**: Jinja2 template engine integration
- **Class1**: Placeholder for future scripting functionality

**Key Features**:
- Python script execution
- Template generation using Jinja2

### 8. BerryAIGen.Toolkit

**Purpose**: Main WPF application providing the user interface and core functionality.

**Key Submodules**:

#### 8.1 Behaviors
- Attached behaviors for UI elements
- Drag-and-drop functionality
- Focus management
- Scroll behavior

#### 8.2 Common
- Async command implementations
- Navigation services
- Relay commands
- Utility classes

#### 8.3 Configuration
- Application settings management
- Accordion settings
- External application configuration

#### 8.4 Controls
- Custom UI controls like ThumbnailView, MetadataPanel, and FilterControl
- WPF control implementations

#### 8.5 Converters
- Value converters for data binding
- Various UI-related converters

#### 8.6 Icons
- Application icons for different themes

#### 8.7 Images
- Application images and resources

#### 8.8 Localization
- Multi-language support
- JSON-based localization files

#### 8.9 MdStyles
- Markdown styling for the application

#### 8.10 Models
- View models for the application
- Data models for UI binding

#### 8.11 Pages
- Application pages like Search, Models, Prompts, and Settings

#### 8.12 Services
- Core application services
- ServiceLocator for dependency management

#### 8.13 Themes
- Application theming support
- Light and dark themes

#### 8.14 Thumbnails
- Thumbnail generation and caching
- Thumbnail service implementation

**Key Features**:
- Main application window and UI
- User interaction handling
- Service management
- Navigation between pages
- Application lifecycle management

### 9. BerryAIGen.Updater

**Purpose**: Application updater for downloading and installing updates.

**Key Components**:
- **Form1**: Main updater UI
- **CustomTextBox**: Custom text box control

**Key Features**:
- Checking for updates
- Downloading update packages
- Installing updates
- User notification about updates

### 10. BerryAIGen.Video

**Purpose**: Video support for the application, including frame extraction.

**Key Components**:
- **FrameExtractor**: Video frame extraction functionality

**Key Features**:
- Extracting frames from video files
- Generating thumbnails from videos

## Module Dependencies

The modules have the following dependency structure:

```
BerryAIGen.Toolkit
鈹溾攢鈹€ BerryAIGen.Common
鈹溾攢鈹€ BerryAIGen.Database
鈹溾攢鈹€ BerryAIGen.IO
鈹溾攢鈹€ BerryAIGen.Civitai
鈹溾攢鈹€ BerryAIGen.Github
鈹斺攢鈹€ BerryAIGen.Video

BerryAIGen.Database
鈹斺攢鈹€ BerryAIGen.Common

BerryAIGen.IO
鈹斺攢鈹€ BerryAIGen.Common

BerryAIGen.Civitai
鈹斺攢鈹€ BerryAIGen.Common

BerryAIGen.Github
鈹斺攢鈹€ BerryAIGen.Common

BerryAIGen.Video
鈹斺攢鈹€ BerryAIGen.Common

BerryAIGen.Updater
鈹斺攢鈹€ (No external dependencies within the solution)
```

## Module Interaction

Modules interact with each other through:
- Direct references (when one module depends on another)
- Shared interfaces and models from BerryAIGen.Common
- ServiceLocator pattern for accessing services across modules
- Events and callbacks for communication

## Development Best Practices for Modules

When developing new modules or modifying existing ones, follow these best practices:

1. **Single Responsibility Principle**: Each module should have a clear, single purpose
2. **Minimize Dependencies**: Keep module dependencies to a minimum
3. **Shared Functionality**: Put shared functionality in BerryAIGen.Common rather than duplicating it
4. **Consistent Naming**: Follow consistent naming conventions across modules
5. **Interface-based Design**: Use interfaces to define contracts between modules
6. **Error Handling**: Implement proper error handling for module interactions
7. **Testing**: Write tests for each module independently
8. **Documentation**: Document module functionality and usage

## Conclusion

The modular structure of BerryAIGen.Toolkit provides a solid foundation for the application, allowing for maintainable, extensible, and testable code. By following clear module boundaries and dependency rules, the codebase remains organized and easy to understand, even as new features are added.

