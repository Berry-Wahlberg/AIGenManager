# MainWindow Component

## Overview

The MainWindow is the central component of the BerryAIGen.Toolkit application, serving as the main entry point and container for all UI elements. It manages the application lifecycle, coordinates between different services and pages, and provides the main user interface for interacting with the application.

## Core Implementation

### Location
The MainWindow implementation is located in `BerryAIGen.Toolkit/MainWindow.xaml.cs` with additional partial classes for specific functionality:
- `MainWindow.xaml.Albums.cs`
- `MainWindow.xaml.Events.cs`
- `MainWindow.xaml.Folders.cs`
- `MainWindow.xaml.Models.cs`
- `MainWindow.xaml.Portable.cs`
- `MainWindow.xaml.Queries.cs`
- `MainWindow.xaml.Scanning.cs`
- `MainWindow.xaml.Tags.cs`
- `MainWindow.xaml.Toast.cs`
- `MainWindow.xaml.Tools.cs`
- `MainWindow.xaml.Updater.cs`

### Class Structure

```csharp
public partial class MainWindow : BorderlessWindow
{
    private readonly MainModel _model;
    private NavigatorService _navigatorService;
    private DataStore _dataStore => ServiceLocator.DataStore;
    private Settings _settings;
    // ... more fields
}
```

## Key Features

### 1. Application Initialization

The MainWindow constructor is responsible for:
- Setting up logging and configuration
- Initializing UI components
- Registering global exception handlers
- Reading configuration files (e.g., samplers.txt)
- Initializing the NavigatorService
- Setting up service locator instances
- Initializing commands and event handlers

### 2. Navigation Management

The MainWindow manages application navigation through the NavigatorService, which handles:
- Page transitions between Search, Models, Prompts, and Settings
- URL-based navigation
- View state management

### 3. Service Coordination

The MainWindow acts as a central coordinator for various services:
- ServiceLocator for accessing all application services
- DataStore for database operations
- Settings for application configuration
- ThumbnailService for thumbnail generation
- ScanningService for folder scanning
- SearchService for image searching

### 4. UI Management

The MainWindow manages the main UI components:
- Thumbnail view and size settings
- Preview panel visibility and popout functionality
- Navigation menu and toolbar
- Status bar and notifications
- Theme management
- GitHub repository link integration in top-right toolbar

#### GitHub Icon Integration
A GitHub icon is displayed in the top-right button stack, providing direct access to the project's GitHub repository. The icon:
- Is 24×24px in size with hover effects
- Has a tooltip "访问项目Github仓库" (Visit GitHub Repository)
- Opens the repository in a new browser tab when clicked
- Uses the FontAwesome GitHub icon
- Changes color to accent color #2196F3 on hover

### 5. File and Folder Management

The MainWindow provides functionality for:
- Adding and removing watched folders
- Scanning folders for new images
- Managing unavailable files
- Opening files with external applications

### 6. Model and Metadata Management

The MainWindow handles:
- Loading and scanning models
- Extracting and managing metadata
- Tag and album management
- Query management

## Core Methods

### Initialization Methods

#### `MainWindow()` Constructor
```csharp
public MainWindow()
{
    // Application initialization logic
}
```
Responsible for setting up the application, initializing services, and configuring the UI.

#### `OnLoaded(object sender, RoutedEventArgs e)`
```csharp
private async void OnLoaded(object sender, RoutedEventArgs e)
{
    // Logic executed when the window is loaded
}
```
Handles post-initialization tasks, including loading settings, initializing pages, and setting up database connections.

### Navigation Methods

#### `OnNavigate(object sender, NavigateEventArgs args)`
```csharp
private void OnNavigate(object sender, NavigateEventArgs args)
{
    // Update active view based on navigation
}
```
Updates the active view when navigation occurs.

### Theme and UI Methods

#### `UpdateTheme(string theme)`
```csharp
private void UpdateTheme(string theme)
{
    ThemeManager.ChangeTheme(theme);
}
```
Changes the application theme based on user settings or system preferences.

#### `TogglePreview()`
```csharp
private void TogglePreview()
{
    // Toggle preview visibility
}
```
Toggles the visibility of the image preview panel.

#### `SetThumbnailSize(int size)`
```csharp
private void SetThumbnailSize(int size)
{
    // Set thumbnail size and notify services
}
```
Updates the thumbnail size across the application.

### File and Folder Methods

#### `ShowInExplorer(FolderViewModel folder)`
```csharp
private void ShowInExplorer(FolderViewModel folder)
{
    // Open folder in Windows Explorer
}
```
Opens a folder in Windows Explorer.

#### `OnCurrentImageOpen(ImageViewModel obj)`
```csharp
private void OnCurrentImageOpen(ImageViewModel obj)
{
    // Open current image with configured viewer
}
```
Opens the current image using the configured viewer (built-in, system default, or custom).

### Model Management Methods

#### `LoadModels()`
```csharp
private void LoadModels()
{
    // Load and scan models from disk
}
```
Loads and scans models from the configured model root path.

## Component Interactions

The MainWindow interacts with numerous components:

| Component | Interaction | Purpose |
|-----------|-------------|---------|
| **ServiceLocator** | Access to all services | Central service management |
| **NavigatorService** | Navigation between pages | Page transition management |
| **DataStore** | Database operations | Data persistence |
| **Settings** | Application configuration | User preferences |
| **Search** | Image search functionality | Core search capabilities |
| **Models** | Model management | Model scanning and viewing |
| **Prompts** | Prompt management | Prompt viewing and management |
| **SettingsPage** | Application settings | User configuration |
| **ThumbnailService** | Thumbnail generation | Image preview |
| **ScanningService** | Folder scanning | Image indexing |
| **MessageService** | User notifications | Message display |
| **ToastService** | Toast notifications | Non-intrusive notifications |

## Application Lifecycle

### Startup Flow

1. **Constructor**: Initialize basic components and services
2. **OnLoaded**: Load settings, initialize pages, create database, and start services
3. **Navigation**: Navigate to the initial view (typically search)
4. **Service Startup**: Start background services (thumbnail generation, folder watching)

### Shutdown Flow

1. **Closing event**: Save settings and clean up resources
2. **Service shutdown**: Stop background services
3. **Window closing**: Close all child windows and release resources

## Key Events

### Global Events

- **UnhandledException**: Global exception handler for unexpected errors
- **UserPreferenceChanged**: Handle system theme changes
- **StateChanged**: Track window state changes (minimized, maximized, normal)
- **SizeChanged**: Track window size changes
- **LocationChanged**: Track window position changes

### Navigation Events

- **OnNavigate**: Handle navigation between pages
- **GotoUrl**: Command for navigating to external URLs

### UI Events

- **TogglePreview**: Toggle preview visibility
- **PopoutPreview**: Open preview in a separate window
- **SetThumbnailSize**: Change thumbnail size
- **ToggleAutoRefresh**: Enable/disable automatic refresh

## Usage Patterns

### Accessing the MainWindow

```csharp
// From within the application
var mainWindow = Application.Current.MainWindow as MainWindow;

// From services, use the WindowService
var mainWindow = ServiceLocator.WindowService.GetMainWindow();
```

### Navigating Between Pages

```csharp
// Navigate to the search page
ServiceLocator.NavigatorService.Goto("search");

// Navigate to the models page
ServiceLocator.NavigatorService.Goto("models");

// Navigate to the settings page
ServiceLocator.NavigatorService.Goto("settings");
```

### Updating Application State

```csharp
// Update thumbnail size
ServiceLocator.Settings.ThumbnailSize = 256;
ServiceLocator.MainModel.ThumbnailSize = 256;

// Toggle preview visibility
ServiceLocator.MainModel.IsPreviewVisible = !ServiceLocator.MainModel.IsPreviewVisible;
```

## Best Practices

### 1. Service Access

- Always use the ServiceLocator to access services rather than directly instantiating them
- Register core services during application startup
- Use lazy initialization for non-critical services

### 2. UI Updates

- Use the Dispatcher for UI updates from background threads
- Avoid long-running operations on the UI thread
- Use async/await for asynchronous operations

### 3. Error Handling

- Register global exception handlers to catch unexpected errors
- Log all errors and exceptions
- Provide user-friendly error messages

### 4. State Management

- Store application state in the MainModel
- Persist user settings to disk
- Use the Settings class for configuration management

## Conclusion

The MainWindow is the central hub of the BerryAIGen.Toolkit application, responsible for coordinating all major components and services. It provides the main user interface and manages the application lifecycle from startup to shutdown. Understanding the MainWindow implementation is essential for developing and maintaining the application, as it touches almost every aspect of the system.

## Related Documentation

- [ServiceLocator Component](./service-locator.md)
- [Scanning Service Component](./scanning-service.md)
- [Search Service Component](./search-service.md)
- [Thumbnail Service Component](./thumbnail-service.md)
- [MVVM Pattern](./../ui-ux/mvvm-pattern.md)
- [Theming System](./../ui-ux/theming.md)
