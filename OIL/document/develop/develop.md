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

### 2.2 Project Structure

```
BerryAIGen.Toolkit/
+-- BerryAIGen.Civitai/          # Civitai integration
+-- BerryAIGen.Common/           # Common utilities and interfaces
+-- BerryAIGen.Data/             # Data models and interfaces
+-- BerryAIGen.Database/         # Database implementation (SQLite)
+-- BerryAIGen.Github/           # GitHub integration
+-- BerryAIGen.IO/               # File I/O operations
+-- BerryAIGen.Scripting/        # Scripting support
+-- BerryAIGen.Toolkit/          # Main application (WPF)
    +-- Controls/               # Custom WPF controls
    +-- Converters/             # Value converters
    +-- Configuration/          # Configuration management
    +-- Localization/           # Internationalization support
    +-- Models/                 # View models
    +-- Pages/                  # Application pages
    +-- Services/               # Business services
    +-- Thumbnails/             # Thumbnail generation
    +-- Themes/                 # UI themes and resources
+-- BerryAIGen.Updater/          # Application updater
+-- BerryAIGen.Video/            # Video support
+-- TestBed/                     # Test application
+-- TestHarness/                 # Unit tests
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
            Console.WriteLine($"Warning: Failed to load SQLite extension: {ex.Message}");
        }
    }
}
```

## 10. GUI Changes

### 10.1 Wide Sidebar Layout

Recently, the GUI was updated to implement a wide sidebar layout with descriptive text elements. This change enhances the user experience by providing clearer navigation options and better visual organization.

#### 10.1.1 MainWindow.xaml Changes

- Converted from DockPanel to Grid layout with explicit column definitions
- Added wide sidebar (200px width, 150px min width) with background color and border
- Updated navigation buttons to include text labels and larger height (32px)
- Added section headers "Main" and "Tools"
- Implemented localization for all new text elements

#### 10.1.2 Search.xaml Changes

- Modified main grid column definitions to expand left sidebar
- Added "Navigation" descriptive label above folders accordion
- Enhanced visual hierarchy with section headers

#### 10.1.3 Localization Support

- Added new localization keys to support multi-language display
- Updated all language files (de-DE, es-ES, fr-FR, ja-JP, uk-UA, zh-CN, zh-TW)
- Keys include: `Navigation.Section.Main`, `Navigation.Item.Folders`, `Navigation.Section.Navigation`, etc.

#### 10.1.4 Technical Details

- WPF Grid layout for precise control over sidebar width
- WPFLocalizeExtension (lex:Loc) for localization
- FontAwesome icons combined with text labels for enhanced navigation
- Consistent styling with existing theme colors and brushes

## 11. Update Checking Mechanism

### 11.1 Overview

The application includes an automatic update checking mechanism that periodically checks for new releases on GitHub.

### 11.2 Trigger Mechanism

- **Startup Check**: The application checks for updates on startup if the `CheckForUpdatesOnStartup` setting is enabled
- **Manual Check**: Users can manually trigger an update check from the menu: `Help > Check for updates`

### 11.3 Configuration

- **Setting**: `CheckForUpdatesOnStartup` (boolean)
- **Location**: Application settings, accessible from the Settings page
- **Default**: Enabled

### 11.4 Check Logic

1. **GitHub API Integration**: Uses GitHub API to fetch the latest release from the repository `https://github.com/Berry-Wahlberg/AIGenManager`
2. **Version Comparison**: Compares the local application version with the latest GitHub release version
3. **Timeout**: The update check has a timeout of 3000ms (3 seconds)
4. **Result Handling**:
   - If an update is available: Shows a notification to the user with option to download
   - If no update is available: Continues to main application
   - If check fails (timeout or error): Silently continues to main application

### 11.5 Update Process

1. **Update Detection**: Application detects a new release
2. **User Notification**: Shows a message box asking if user wants to update
3. **GitHub Navigation**: If user agrees, opens `https://github.com/Berry-Wahlberg/AIGenManager/releases/latest` in browser
4. **Manual Update**: User downloads and installs the update manually from GitHub

### 11.6 Technical Implementation

- **Class**: `UpdateChecker` in `BerryAIGen.Common` project
- **GitHub Client**: Uses `GithubClient` class to interact with GitHub API
- **UI Component**: `UpdateDetectionWindow` shows update checking progress
- **Service Locator**: Uses ServiceLocator for dependency management

## 12. Documentation Server

### 12.1 Overview

Currently, the project documentation is maintained in the GitHub repository and accessible through GitHub Pages.

### 12.2 Documentation Location

- **GitHub Repository**: `https://github.com/Berry-Wahlberg/AIGenManager`
- **Documentation Directory**: `/document/develop/` in the repository
- **Local Access**: Documentation files are included in the application package for offline access

### 12.3 Documentation Updates

- Documentation is updated alongside code changes
- Follow the same version control process as code
- No separate documentation server is used at this time

### 12.4 Access Parameters

- **Protocol**: HTTPS
- **Authentication**: None required for public documentation
- **Rate Limiting**: Subject to GitHub API rate limits for automated access

### 12.5 Future Plans

- Consider implementing a dedicated documentation server for improved accessibility
- Add versioned documentation support
- Implement documentation search functionality
