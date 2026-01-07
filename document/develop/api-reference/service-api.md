# Service API Reference

## Overview

This document provides detailed reference documentation for the service layer API of the BerryAIGen.Toolkit application. The service layer acts as an intermediary between the UI and the data access layer, implementing business logic and coordinating operations between components.

## Service Architecture

### 1. Service Base Classes

#### ServiceBase

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Base class for all services, providing common functionality.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `ServiceLocator` | `IServiceLocator` | Access to the service locator |
| `Logger` | `ILogger` | Logger instance for the service |
| `IsInitialized` | `bool` | Whether the service has been initialized |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Initialize` | Initializes the service | `IServiceLocator serviceLocator` | `Task<bool>` |
| `OnInitialized` | Called after initialization is complete | - | `Task` |
| `Shutdown` | Shuts down the service | - | `Task<bool>` |

### 2. Core Services

#### SearchService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Implements search functionality for images.

**Inheritance**: `ServiceBase`, `ISearchService`

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `CurrentQuery` | `string` | The current search query |
| `CurrentResults` | `SearchResults` | The current search results |
| `IsSearching` | `bool` | Whether a search is in progress |
| `CurrentPage` | `int` | The current page of results |
| `PageSize` | `int` | The number of results per page |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `SearchAsync` | Performs a search query | `string query`, `int page = 1`, `int pageSize = 50` | `Task<SearchResults>` |
| `GetFilterOptionsAsync` | Retrieves available filter options | - | `Task<FilterOptions>` |
| `ApplyFilter` | Applies a filter to the current search | `Filter filter` | `Task<SearchResults>` |
| `RemoveFilter` | Removes a filter from the current search | `string filterId` | `Task<SearchResults>` |
| `ClearFilters` | Clears all active filters | - | `void` |
| `RefreshResults` | Refreshes the current search results | - | `Task<SearchResults>` |
| `NextPage` | Navigates to the next page of results | - | `Task<SearchResults>` |
| `PreviousPage` | Navigates to the previous page of results | - | `Task<SearchResults>` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `SearchCompleted` | Raised when a search is completed | `SearchCompletedEventArgs` (Results, Query) |
| `SearchStarted` | Raised when a search is started | `SearchStartedEventArgs` (Query) |
| `ResultsChanged` | Raised when search results change | `ResultsChangedEventArgs` (Results) |
| `FiltersChanged` | Raised when filters are changed | `FiltersChangedEventArgs` (Filters) |

**Example Usage**:

```csharp
// Subscribe to search events
_searchService.SearchCompleted += OnSearchCompleted;

// Perform a search
var results = await _searchService.SearchAsync(
    "cat model:\"test model\"", 
    page: 1, 
    pageSize: 20);

// Apply a filter
await _searchService.ApplyFilter(new Filter
{
    Id = "rating",
    Name = "Rating",
    Value = ">= 8",
    Type = FilterType.Number
});
```

#### MetadataScannerService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Scans images for metadata and updates the database.

**Inheritance**: `ServiceBase`, `IMetadataScannerService`

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `IsRunning` | `bool` | Whether the scanner is running |
| `QueueCount` | `int` | Number of items in the scanning queue |
| `CurrentOperation` | `string` | Description of the current operation |
| `Progress` | `double` | Progress of the current operation (0-100) |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ScanDirectoryAsync` | Scans a directory for images and extracts metadata | `string directoryPath`, `bool recursive = true` | `Task<ScanResult>` |
| `ScanFileAsync` | Scans a single file for metadata | `string filePath` | `Task<ScanResult>` |
| `EnqueueFile` | Adds a file to the scanning queue | `string filePath` | `void` |
| `EnqueueDirectory` | Adds a directory to the scanning queue | `string directoryPath`, `bool recursive = true` | `void` |
| `Start` | Starts the scanning service | - | `void` |
| `Stop` | Stops the scanning service | - | `void` |
| `ClearQueue` | Clears all items from the scanning queue | - | `void` |
| `GetScanHistoryAsync` | Retrieves the scan history | - | `Task<List<ScanHistoryItem>>` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `FileScanned` | Raised when a file is scanned | `FileScannedEventArgs` (FilePath, Success, Error) |
| `ScanCompleted` | Raised when a scan operation is completed | `ScanCompletedEventArgs` (DirectoryPath, FilesScanned, FilesAdded) |
| `ScanProgressChanged` | Raised when scan progress changes | `ProgressChangedEventArgs` (Progress, Current, Total) |
| `QueueChanged` | Raised when the scanning queue changes | `QueueChangedEventArgs` (QueueCount) |

**Example Usage**:

```csharp
// Subscribe to scan events
_metadataScanner.FileScanned += OnFileScanned;
_metadataScanner.ScanCompleted += OnScanCompleted;

// Start the scanner
_metadataScanner.Start();

// Enqueue a directory for scanning
_metadataScanner.EnqueueDirectory(
    "C:\images", 
    recursive: true);
```

#### ScanningService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Manages directory scanning and file system watching.

**Inheritance**: `ServiceBase`, `IScanningService`

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `WatchDirectories` | `ObservableCollection<WatchDirectory>` | Directories being watched |
| `IsWatching` | `bool` | Whether the service is watching directories |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `AddWatchDirectory` | Adds a directory to the watch list | `string directoryPath`, `bool recursive = true` | `Task<bool>` |
| `RemoveWatchDirectory` | Removes a directory from the watch list | `string directoryPath` | `Task<bool>` |
| `GetWatchDirectoriesAsync` | Retrieves all watched directories | - | `Task<List<WatchDirectory>>` |
| `RescanDirectoryAsync` | Rescans a specific directory | `string directoryPath` | `Task<ScanResult>` |
| `RescanAllDirectoriesAsync` | Rescans all watched directories | - | `Task<List<ScanResult>>` |
| `StartWatching` | Starts watching directories for changes | - | `Task<bool>` |
| `StopWatching` | Stops watching directories | - | `Task<bool>` |
| `SaveWatchDirectoriesAsync` | Saves the watch directory list to configuration | - | `Task<bool>` |
| `LoadWatchDirectoriesAsync` | Loads the watch directory list from configuration | - | `Task<bool>` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `DirectoryWatched` | Raised when a directory is added to the watch list | `DirectoryWatchedEventArgs` (DirectoryPath) |
| `DirectoryUnwatched` | Raised when a directory is removed from the watch list | `DirectoryUnwatchedEventArgs` (DirectoryPath) |
| `DirectoryChanged` | Raised when a watched directory changes | `DirectoryChangedEventArgs` (DirectoryPath, ChangeType) |

**Example Usage**:

```csharp
// Add a directory to watch
await _scanningService.AddWatchDirectory(
    "C:\images", 
    recursive: true);

// Start watching directories
await _scanningService.StartWatching();

// Subscribe to directory change events
_scanningService.DirectoryChanged += OnDirectoryChanged;
```

#### ThumbnailService

**Namespace**: `BerryAIGen.Toolkit.Thumbnails`

**Purpose**: Provides thumbnail generation and caching services.

**Inheritance**: `ServiceBase`, `IThumbnailService`

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `CacheDirectory` | `string` | Directory where thumbnails are cached |
| `DefaultWidth` | `int` | Default thumbnail width (default: 256) |
| `DefaultHeight` | `int` | Default thumbnail height (default: 256) |
| `CacheSize` | `long` | Current size of the thumbnail cache in bytes |
| `MaxCacheSize` | `long` | Maximum allowed cache size in bytes (default: 1GB) |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetThumbnailAsync` | Retrieves or generates a thumbnail for an image | `string filePath`, `int width = 256`, `int height = 256` | `Task<string>` (Thumbnail path) |
| `GenerateThumbnailsAsync` | Generates thumbnails for multiple images | `IEnumerable<string> filePaths`, `int width = 256`, `int height = 256` | `Task<Dictionary<string, string>>` |
| `ClearCache` | Clears the thumbnail cache | `bool clearAll = false` | `void` |
| `DeleteThumbnail` | Deletes a specific thumbnail | `string filePath` | `bool` |
| `CleanupCache` | Cleans up old thumbnails to reduce cache size | `long targetSize = -1` | `Task<long>` (Bytes freed) |
| `InitializeCacheDirectory` | Initializes the cache directory | - | `Task<bool>` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `ThumbnailGenerated` | Raised when a thumbnail is generated | `ThumbnailGeneratedEventArgs` (ImagePath, ThumbnailPath) |
| `ThumbnailGenerationProgress` | Raised during batch thumbnail generation | `ProgressEventArgs` (Current, Total) |
| `CacheCleaned` | Raised when the cache is cleaned up | `CacheCleanedEventArgs` (BytesFreed, NewCacheSize) |

**Example Usage**:

```csharp
// Configure thumbnail service
_thumbnailService.DefaultWidth = 512;
_thumbnailService.DefaultHeight = 512;
_thumbnailService.MaxCacheSize = 2L * 1024 * 1024 * 1024; // 2GB

// Get thumbnail for an image
var thumbnailPath = await _thumbnailService.GetThumbnailAsync(
    "C:\images\test.jpg");

// Generate thumbnails for multiple images
var filePaths = new[] { "C:\images\test1.jpg", "C:\images\test2.jpg" };
var thumbnails = await _thumbnailService.GenerateThumbnailsAsync(filePaths);
```

## Service Coordination

### 1. Service Dependencies

The service layer has the following dependency relationships:

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?     鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹? SearchService     鈹傗梽鈹€鈹€鈹€鈹€鈹€鈹? ThumbnailService  鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?     鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
          鈹?
          鈻?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?     鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?MetadataScannerService 鈹傗梽鈹€鈹€鈹€鈹€鈹€鈹?ScanningService   鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?     鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
          鈹?
          鈻?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?  DataStore        鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

### 2. Service Initialization Order

Services are initialized in the following order:

1. `ServiceLocator` - Core dependency resolution
2. `DataStore` - Database access
3. `ThumbnailService` - Thumbnail generation
4. `MetadataScannerService` - Metadata extraction
5. `ScanningService` - Directory watching
6. `SearchService` - Search functionality

## Configuration

### 1. Service Configuration

Services are configured through the application's configuration system, with settings stored in `appsettings.json`:

```json
{
  "ThumbnailService": {
    "DefaultWidth": 256,
    "DefaultHeight": 256,
    "MaxCacheSize": 1073741824, // 1GB
    "CacheDirectory": "%APPDATA%\\BerryAIGen\\Thumbnails"
  },
  "MetadataScannerService": {
    "MaxConcurrentScans": 4,
    "ScanInterval": 5000
  },
  "ScanningService": {
    "WatchInterval": 1000
  }
}
```

### 2. Configuration Management

The `ConfigurationService` provides access to configuration settings:

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetSetting` | Retrieves a setting value | `string key`, `T defaultValue` | `T` |
| `SetSetting` | Sets a setting value | `string key`, `T value` | `Task<bool>` |
| `LoadConfiguration` | Loads configuration from file | - | `Task<bool>` |
| `SaveConfiguration` | Saves configuration to file | - | `Task<bool>` |

**Example Usage**:

```csharp
// Get thumbnail service configuration
var defaultWidth = _configurationService.GetSetting<int>(
    "ThumbnailService:DefaultWidth", 
    256);

var maxCacheSize = _configurationService.GetSetting<long>(
    "ThumbnailService:MaxCacheSize", 
    1073741824);
```

## Error Handling

### 1. Service-Specific Exceptions

Each service can throw service-specific exceptions that inherit from `ServiceException`:

| Exception | Service | Description |
|-----------|---------|-------------|
| `ThumbnailGenerationException` | ThumbnailService | Thrown when thumbnail generation fails |
| `ScanException` | MetadataScannerService | Thrown when scanning fails |
| `SearchException` | SearchService | Thrown when search fails |
| `DatabaseException` | DataStore | Thrown when database operations fail |

**Example Usage**:

```csharp
try
{
    await _thumbnailService.GetThumbnailAsync("invalid/path.jpg");
}
catch (ThumbnailGenerationException ex)
{
    Log.Error(ex, "Failed to generate thumbnail: {Message}", ex.Message);
}
catch (ServiceException ex)
{
    Log.Error(ex, "Service error: {Message}", ex.Message);
}
```

## Service Events

### 1. Event System

The service layer uses a custom event system that allows components to subscribe to service events:

#### EventBase

**Namespace**: `BerryAIGen.Common.Events`

**Purpose**: Base class for all events, providing common functionality.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `EventId` | `Guid` | Unique event identifier |
| `Timestamp` | `DateTime` | Event timestamp |
| `Source` | `object` | Event source |

#### EventAggregator

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Manages event subscription and publication.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Subscribe<TEvent>` | Subscribes to an event type | `Action<TEvent> handler` | `Guid` (Subscription ID) |
| `Unsubscribe` | Unsubscribes from an event | `Guid subscriptionId` | `bool` |
| `Publish` | Publishes an event | `TEvent @event` | `Task` |

**Example Usage**:

```csharp
// Subscribe to an event
var subscriptionId = _eventAggregator.Subscribe<ThumbnailGeneratedEvent>(
    @event => {
        Log.Information("Thumbnail generated: {ThumbnailPath}", @event.ThumbnailPath);
    });

// Publish an event
await _eventAggregator.Publish(new ThumbnailGeneratedEvent
{
    ImagePath = "C:\images\test.jpg",
    ThumbnailPath = "C:\thumbnails\test.jpg" + ".thumb.jpg"
});

// Unsubscribe from an event
_eventAggregator.Unsubscribe(subscriptionId);
```

## Conclusion

The service layer API provides a structured and maintainable way to access the application's business logic. By using these services, the UI can interact with the underlying functionality without directly accessing the data layer, promoting separation of concerns and improving testability. The service architecture is designed to be extensible, allowing new services to be added as the application evolves.
