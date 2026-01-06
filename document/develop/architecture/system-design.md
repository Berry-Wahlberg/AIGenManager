# System Design

## Overview
This document describes the overall system design and architecture of BerryAIGen.Toolkit. It provides a high-level view of the system's components, their relationships, and the design patterns used throughout the codebase.

## System Architecture

BerryAIGen.Toolkit follows a layered architecture with clear separation of concerns. The system is divided into four main layers:

### 1. Presentation Layer

The Presentation Layer is responsible for the user interface and user interaction. It includes:

- **WPF UI Elements**: XAML files defining the visual layout and appearance
- **Custom Controls**: Reusable UI components like ThumbnailView, MetadataPanel, and FilterControl
- **Pages**: Application views such as Search, Models, Prompts, and Settings
- **Behaviors**: Attached behaviors for UI elements
- **Converters**: Value converters for data binding

### 2. Business Layer

The Business Layer contains the application's core logic and manages the interaction between the Presentation and Data layers. It includes:

- **ViewModels**: Classes that provide data and behavior for the UI elements
- **Services**: Core functionality services like SearchService, ThumbnailService, and ScanningService
- **Commands**: Implementation of the Command pattern for UI actions
- **Common Utilities**: Helper classes and extension methods

### 3. Data Layer

The Data Layer handles data persistence and retrieval. It includes:

- **Database Models**: Classes representing database tables
- **DataStore**: Main database access class
- **Migrations**: Database schema migration scripts
- **Query Builders**: Classes for constructing database queries

### 4. External Layer

The External Layer manages interactions with external systems and resources. It includes:

- **File System Access**: Reading and writing files
- **External APIs**: Integration with Civitai and GitHub APIs
- **External Libraries**: Usage of third-party libraries like MetadataExtractor

## Design Patterns

### 1. Model-View-ViewModel (MVVM)

The application extensively uses the MVVM pattern for separating UI concerns from business logic:

- **Model**: Represents the data and business objects
- **View**: XAML files that define the UI
- **ViewModel**: Acts as an intermediary between the View and Model, providing data binding and command handling

### 2. Service Locator Pattern

The ServiceLocator pattern is used for managing dependencies and providing access to services throughout the application:

```csharp
// Usage example
var searchService = ServiceLocator.SearchService;
var thumbnailService = ServiceLocator.ThumbnailService;
```

### 3. Command Pattern

The Command pattern is implemented for handling UI actions:

- **RelayCommand**: Basic implementation for synchronous commands
- **AsyncCommand**: Implementation for asynchronous commands
- **IAsyncCommand**: Interface for async commands with cancellation support

### 4. Singleton Pattern

Several core components use the Singleton pattern to ensure only one instance exists:

- **ServiceLocator**: Central service registry
- **ThumbnailCache**: Image thumbnail cache
- **ToastService**: Notification service

### 5. Lazy Initialization

Many services use lazy initialization to improve startup performance:

```csharp
private static PreviewService? _previewService;
public static PreviewService PreviewService
{
    get { return _previewService ??= new PreviewService(); }
}
```

### 6. Observer Pattern

The Observer pattern is used for event handling and notifications:

- **INotifyPropertyChanged**: Implemented by ViewModels for data binding
- **Event Handlers**: Used throughout the application for communication between components
- **MessageService**: For displaying messages and dialogs

### 7. Repository Pattern

The Repository pattern is implemented through the DataStore class, which provides a centralized interface for database operations:

```csharp
// Example usage
var dataStore = ServiceLocator.DataStore;
var images = await dataStore.GetImagesAsync(query);
```

## Core System Components

### 1. ServiceLocator

The ServiceLocator is a central registry that provides access to all core services. It uses lazy initialization to create services only when they are first requested.

### 2. DataStore

The DataStore manages all database operations, including:
- Database connection management
- Table creation and indexing
- Data retrieval and modification
- Database migrations

### 3. MainWindow

The MainWindow is the application's main entry point. It manages:
- Application lifecycle
- Navigation between pages
- Main menu and toolbar
- Global application state

### 4. SearchService

The SearchService handles all search-related functionality:
- Query parsing and execution
- Search result caching
- Filtering and sorting

### 5. ScanningService

The ScanningService is responsible for:
- Scanning folders for new images
- Extracting metadata from images
- Updating the database with new or changed images

### 6. ThumbnailService

The ThumbnailService manages:
- Thumbnail generation for images
- Thumbnail caching for improved performance
- Asynchronous thumbnail loading

## Data Flow

### 1. Image Import and Indexing

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹? ScanningService   鈹?    鈹? MetadataScanner   鈹?    鈹?    DataStore      鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - Scan folders    鈹傗攢鈹€鈹€鈹€鈻垛攤  - Extract metadata鈹傗攢鈹€鈹€鈹€鈻垛攤  - Store metadata  鈹?
鈹? - Detect new files鈹?    鈹? - Generate hashes 鈹?    鈹? - Create records  鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
          鈹?                                                    鈻?
          鈹?                                                    鈹?
          鈻?                                                    鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹? ThumbnailService  鈹?    鈹? FileSystem        鈹?    鈹?    Thumbnail     鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - Generate         鈹傗攢鈹€鈹€鈹€鈻垛攤  - Read image files鈹傗攢鈹€鈹€鈹€鈻垛攤  - Cache thumbnails鈹?
鈹?   thumbnails      鈹?    鈹?                   鈹?    鈹? - Return to UI    鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

### 2. Search and Display

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?   Search View     鈹?    鈹? SearchService     鈹?    鈹?    DataStore      鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - User enters     鈹傗攢鈹€鈹€鈹€鈻垛攤  - Parse query     鈹傗攢鈹€鈹€鈹€鈻垛攤  - Execute query   鈹?
鈹?   search query    鈹?    鈹? - Build SQL       鈹?    鈹? - Return results  鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
          鈻?                                                    鈹?
          鈹?                                                    鈹?
          鈹?                                                    鈻?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹? ThumbnailView     鈹?    鈹? ThumbnailService  鈹?    鈹? Search Results    鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - Display         鈹傗攢鈹€鈹€鈹€鈻垛攤  - Load thumbnails 鈹傗攢鈹€鈹€鈹€鈻垛攤  - Store in cache  鈹?
鈹?   thumbnails      鈹?    鈹? - Return to view  鈹?    鈹? - Provide to UI   鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

### 3. User Interaction

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?      User         鈹?    鈹? Presentation      鈹?    鈹? Business Layer    鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? Layer             鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - Interacts with  鈹傗攢鈹€鈹€鈹€鈻垛攤  - Handle UI       鈹傗攢鈹€鈹€鈹€鈻垛攤  - Process command 鈹?
鈹?   UI elements     鈹?    鈹?   events          鈹?    鈹? - Update data     鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
          鈹?                                                    鈻?
          鈹?                                                    鈹?
          鈹?                                                    鈹?
          鈻?                                                    鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹? Database Updates  鈹?    鈹? Data Layer        鈹?    鈹? ServiceLocator    鈹?
鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?    鈹? 鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€   鈹?
鈹? - Save changes to 鈹傗攢鈹€鈹€鈹€鈻垛攤  - Execute database鈹傗攢鈹€鈹€鈹€鈻垛攤  - Provide service 鈹?
鈹?   database        鈹?    鈹?   operations      鈹?    鈹?   instances       鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

## Performance Considerations

### 1. Database Optimization

- **Indexes**: Comprehensive indexing on frequently queried columns
- **Read-Only Connections**: Separate read-only connections for query operations
- **Batch Operations**: Batch processing for bulk updates
- **Caching**: Query result caching for frequently accessed data

### 2. UI Responsiveness

- **Asynchronous Operations**: Long-running operations (scanning, thumbnail generation) are performed in background threads
- **Lazy Loading**: Thumbnails and metadata are loaded on demand
- **Debouncing**: Search queries are debounced to avoid excessive database calls
- **Virtualization**: UI controls use virtualization for large datasets

### 3. Memory Management

- **Thumbnail Caching**: Efficient thumbnail cache with size limits
- **Disposable Patterns**: Proper disposal of resources like database connections
- **Weak References**: Use of weak references for cached objects when appropriate

## Extensibility

The system is designed to be extensible, allowing for easy addition of new features:

- **Metadata Format Support**: New metadata formats can be added by extending the MetadataScanner
- **Search Filters**: Additional search filters can be implemented without major changes
- **UI Components**: New UI components can be added using the existing MVVM pattern
- **Services**: New services can be registered with the ServiceLocator

## Security Considerations

- **SQL Injection Protection**: Parameterized queries are used throughout the application
- **File System Access**: Proper error handling for file operations
- **External API Calls**: Secure handling of API keys and external requests
- **Exception Handling**: Global exception handling to prevent crashes

## Conclusion

The system design of BerryAIGen.Toolkit follows modern software engineering principles, with clear separation of concerns, well-defined layers, and appropriate design patterns. This architecture provides a solid foundation for current functionality while allowing for future growth and extensibility.

