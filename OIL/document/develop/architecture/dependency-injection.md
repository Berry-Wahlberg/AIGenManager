# Dependency Injection

## Overview
This document describes the dependency injection and service management approach used in BerryAIGen.Toolkit. While the application doesn't use a full-fledged dependency injection container, it implements a ServiceLocator pattern for managing dependencies and providing access to services throughout the application.

## ServiceLocator Pattern

### Introduction

The ServiceLocator pattern is used as a lightweight alternative to full dependency injection containers. It provides a centralized registry for services, allowing components to retrieve dependencies without having to create them directly.

### Core Implementation

The main implementation of the ServiceLocator pattern is found in `BerryAIGen.Toolkit/Services/ServiceLocator.cs`. This class maintains a registry of all core services used in the application.

### Service Registration

Services are registered with the ServiceLocator either:
1. During application startup
2. When first requested (lazy initialization)
3. Explicitly by components

### Service Access

Components can access services through the ServiceLocator using static properties:

```csharp
// Example: Accessing the SearchService
var searchService = ServiceLocator.SearchService;

// Example: Accessing the ThumbnailService
var thumbnailService = ServiceLocator.ThumbnailService;
```

## ServiceLocator Implementation

### ServiceLocator Class Structure

```csharp
public class ServiceLocator
{
    // Service backing fields
    private static NavigatorService? _navigatorService;
    private static MessageService? _messageService;
    private static DataStore? _dataStore;
    // ... more service backing fields

    // Service properties with lazy initialization
    public static PreviewService PreviewService
    {
        get { return _previewService ??= new PreviewService(); }
    }
    
    // Service registration methods
    public static void SetDataStore(DataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    // ... more service registration methods
}
```

### Lazy Initialization

Most services use lazy initialization to improve startup performance. This means services are only created when they are first requested:

```csharp
private static ThumbnailService? _thumbnailService;
public static ThumbnailService ThumbnailService
{
    get { return _thumbnailService ??= new ThumbnailService(); }
}
```

### Explicit Registration

Some core services need to be explicitly registered during application startup, typically with specific configuration:

```csharp
// Example: Registering the DataStore
var dataStore = new DataStore(AppInfo.DatabasePath);
ServiceLocator.SetDataStore(dataStore);

// Example: Registering the Settings
ServiceLocator.SetSettings(_settings);
```

## Core Services

The following are the core services managed by the ServiceLocator:

| Service | Description | Initialization Type |
|---------|-------------|---------------------|
| **DataStore** | Database access and management | Explicit |
| **Settings** | Application configuration | Explicit |
| **NavigatorService** | Application navigation | Explicit |
| **MessageService** | User message and dialog management | Explicit |
| **SearchService** | Search functionality | Explicit |
| **PreviewService** | Image preview management | Lazy |
| **ThumbnailNavigationService** | Thumbnail navigation | Lazy |
| **TaggingService** | Image tagging functionality | Lazy |
| **NotificationService** | User notifications | Lazy |
| **ScanningService** | Folder scanning and indexing | Lazy |
| **FolderService** | Folder management | Lazy |
| **ProgressService** | Progress reporting | Lazy |
| **MetadataScannerService** | Metadata extraction | Lazy |
| **DatabaseWriterService** | Database write operations | Lazy |
| **ThumbnailService** | Thumbnail generation and caching | Lazy |
| **ContextMenuService** | Context menu management | Lazy |
| **ExternalApplicationsService** | External application integration | Lazy |
| **FileService** | File system operations | Lazy |
| **AlbumService** | Album management | Lazy |
| **TagService** | Tag management | Lazy |
| **WindowService** | Window management | Lazy |
| **ToastService** | Toast notification service | Explicit |

## Service Dependencies

Services often depend on other services. These dependencies are typically resolved through the ServiceLocator itself:

```csharp
// Example: Service dependency resolution
public class SearchService
{
    private readonly DataStore _dataStore;
    private readonly Settings _settings;
    
    public SearchService()
    {
        _dataStore = ServiceLocator.DataStore;
        _settings = ServiceLocator.Settings;
    }
}
```

## Benefits of the ServiceLocator Pattern

1. **Centralized Service Management**: All services are registered and accessed from a single location
2. **Lazy Initialization**: Services are only created when needed, improving startup performance
3. **Easy Access**: Components can easily access services without complex dependency chains
4. **Reduced Coupling**: Components depend on abstractions rather than concrete implementations
5. **Testability**: Services can be replaced with mock implementations for testing

## Limitations of the ServiceLocator Pattern

1. **Hidden Dependencies**: Dependencies are not explicit in constructor parameters
2. **Global State**: The ServiceLocator maintains global state, which can make testing more difficult
3. **Runtime Resolution**: Dependency resolution happens at runtime, which can lead to runtime errors
4. **Less Transparent**: It's harder to see a component's dependencies at a glance
5. **Potential for Misuse**: Components may become too dependent on the ServiceLocator

## Best Practices for Using the ServiceLocator

1. **Use Sparingly**: Only use the ServiceLocator for core services, not for every dependency
2. **Favor Constructor Injection**: When possible, use constructor injection for explicit dependencies
3. **Limit Service Dependencies**: Keep service dependencies to a minimum
4. **Register Early**: Register core services during application startup
5. **Mock for Testing**: Create mock implementations of services for unit testing
6. **Document Dependencies**: Clearly document a component's dependencies

## Future Considerations

While the ServiceLocator pattern works well for the current application size, there are considerations for future development:

1. **Full Dependency Injection Container**: Consider migrating to a full DI container like Microsoft.Extensions.DependencyInjection for better dependency management
2. **Constructor Injection**: Increase the use of constructor injection for explicit dependencies
3. **Service Lifetime Management**: Implement proper service lifetime management (singleton, scoped, transient)
4. **Dependency Graph Validation**: Add validation to ensure all dependencies are properly registered
5. **Test Support**: Improve support for unit testing with better mock service registration

## Conclusion

The ServiceLocator pattern provides a lightweight and effective way to manage dependencies in BerryAIGen.Toolkit. It allows for centralized service management, lazy initialization, and easy access to services throughout the application. While it has some limitations, it works well for the current application architecture and size.

As the application grows, it may be beneficial to consider migrating to a full dependency injection container to address some of the limitations of the ServiceLocator pattern, particularly around explicit dependencies and testability.

