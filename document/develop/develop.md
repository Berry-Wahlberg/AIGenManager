# BerryAIGen.Toolkit - Technical Development Documentation

## 1. Overview

BerryAIGen.Toolkit is a comprehensive image metadata indexer and viewer designed specifically for AI-generated images. It provides powerful tools to help users organize, search, and manage their growing collection of AI-generated media. This documentation provides detailed technical information for developers working on the project.

### 1.1 Project Vision

The vision of BerryAIGen.Toolkit is to create a user-friendly, performant, and extensible tool for managing AI-generated images, with a focus on:
- Comprehensive metadata extraction and indexing
- Powerful search and filtering capabilities
- Intuitive user interface
- Extensibility for future features and metadata formats

### 1.2 Key Features

- **Metadata Extraction**: Extracts detailed metadata (PNGInfo) from various AI image generation platforms
- **Search and Filtering**: Advanced search capabilities across all extracted metadata
- **Image Organization**: Ratings, tags, and albums for organizing images
- **Thumbnail Generation**: Efficient thumbnail generation for quick preview
- **Batch Processing**: Support for batch operations on images
- **Multi-language Support**: Internationalization support for different languages

## 2. Architecture and System Design

### 2.1 System Architecture

The project follows a modular architecture with clear separation of concerns:

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

### 2.2 Project Structure

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

## 3. Technical Stack

### 3.1 Core Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Core framework |
| WPF | .NET 8.0 | UI framework |
| SQLite | 3.x | Database |
| C# | 12.0 | Programming language |
| XAML | - | UI markup |

### 3.2 Key Dependencies

| Library | Version | Purpose |
|---------|---------|---------|
| Dapper | 2.0.123 | Database access |
| FontAwesome.WPF | 4.7.0.9 | Icon library |
| WPFLocalizeExtension | - | Internationalization |

### 3.3 Development Tools

- Visual Studio 2022 or later
- .NET 8.0 SDK
- Git for version control

## 4. Implementation Details

### 4.1 Database Design

The application uses SQLite as its database backend. The database schema includes tables for:

- **Images**: Core image metadata
- **Folders**: Folder structure and settings
- **Tags**: User-defined tags for images
- **Albums**: Image albums and collections
- **Models**: AI model information
- **Prompts**: Extracted prompts from images

### 4.2 Metadata Extraction

Metadata extraction is handled by the `MetadataService` which supports multiple metadata formats:

- **AUTOMATIC1111 and compatible**: Tensor.Art, SDNext
- **InvokeAI**: Dream/sd-metadata/invokeai_metadata
- **NovelAI**
- **Stable Diffusion**
- **EasyDiffusion**
- **Fooocus family**: RuinedFooocus, Fooocus, FooocusMRE
- **Stable Swarm**

### 4.3 UI Architecture

The UI follows the MVVM (Model-View-ViewModel) pattern:

- **Models**: Represent the data and business logic
- **Views**: XAML files defining the UI structure
- **ViewModels**: Connect the views to the models and handle user interactions

Key UI components:

- **ThumbnailView**: Custom control for displaying image thumbnails
- **PreviewPane**: Displays detailed image information and metadata
- **SearchPanel**: Advanced search and filtering interface
- **SettingsWindow**: Application settings management

### 4.4 Search Implementation

The search functionality is implemented using a combination of:

- **SQLite Full-Text Search**: For fast text searching across prompts and metadata
- **Dynamic Query Generation**: For building complex search queries based on user input
- **Debouncing**: For optimizing search performance by delaying search execution during user input

### 4.5 Thumbnail Generation

Thumbnails are generated asynchronously to improve UI responsiveness:

- **Background Processing**: Thumbnails are generated in a background thread
- **Caching**: Generated thumbnails are cached for quick access
- **Lazy Loading**: Thumbnails are loaded on demand as they come into view

## 5. Technical Challenges and Solutions

### 5.1 Thread Safety in UI Operations

**Challenge**: WPF UI elements can only be accessed from the UI thread, but many operations (like thumbnail generation, metadata extraction) are performed in background threads.

**Solution**: 
- Use `Dispatcher.Invoke` to marshal UI access from background threads
- Implement proper thread checking in UI-related methods
- Use async/await pattern for asynchronous operations

**Example**: Added thread safety checks in `ReloadThumbnailsView` method:
```csharp
public void ReloadThumbnailsView()
{
    // Ensure in UI thread
    if (!Dispatcher.CheckAccess())
    {
        Dispatcher.Invoke(() => ReloadThumbnailsView());
        return;
    }
    // UI operations here
}
```

### 5.2 SQLite Extension Loading

**Challenge**: The application uses SQLite extensions for additional functionality, but loading extensions can fail for various reasons (missing files, permission issues).

**Solution**: 
- Added robust error handling around extension loading
- Made extension loading optional, allowing the application to continue running without extensions
- Added detailed logging for extension loading issues

**Example**: Improved extension loading in `DataStore.Create` method:
```csharp
try
{
    db.EnableLoadExtension(true);
    var extensionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "extensions", "path0.dll");
    if (File.Exists(extensionPath))
    {
        try
        {
            db.LoadExtension(extensionPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Failed to load SQLite extension: {ex.Message}
