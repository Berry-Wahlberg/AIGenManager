# Public Interfaces

## Overview

This document provides reference documentation for the public interfaces exposed by the BerryAIGen.Toolkit application. These interfaces define the core API surface that external components and extensions can use to interact with the application.

## Core Interfaces

### 1. IDataStore

**Namespace**: `BerryAIGen.Database`

**Purpose**: Provides data access operations for images and metadata.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `AddImageAsync` | Adds a new image to the database | `Image image` | `Task<int>` (Image ID) |
| `GetImageByIdAsync` | Retrieves an image by its ID | `int id` | `Task<Image>` |
| `GetImagesAsync` | Retrieves images with optional filtering | `string whereClause = null`, `object parameters = null` | `Task<List<Image>>` |
| `UpdateImageAsync` | Updates an existing image | `Image image` | `Task<bool>` |
| `DeleteImageAsync` | Deletes an image by its ID | `int id` | `Task<bool>` |
| `GetImageMetadataAsync` | Retrieves metadata for an image | `int imageId` | `Task<ImageMetadata>` |
| `UpdateImageMetadataAsync` | Updates metadata for an image | `ImageMetadata metadata` | `Task<bool>` |
| `GetImagesWithMetadataAsync` | Retrieves images with their metadata | `string whereClause = null`, `object parameters = null` | `Task<List<ImageWithMetadata>>` |

**Example Usage**:

```csharp
// Get all images with model "test model"
var images = await _dataStore.GetImagesAsync(
    "model = @model", 
    new { model = "test model" });
```

### 2. IThumbnailService

**Namespace**: `BerryAIGen.Toolkit.Thumbnails`

**Purpose**: Provides thumbnail generation and caching services.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetThumbnailAsync` | Retrieves or generates a thumbnail for an image | `string filePath`, `int width = 256`, `int height = 256` | `Task<string>` (Thumbnail path) |
| `GenerateThumbnailsAsync` | Generates thumbnails for multiple images | `IEnumerable<string> filePaths`, `int width = 256`, `int height = 256` | `Task<Dictionary<string, string>>` |
| `ClearCache` | Clears the thumbnail cache | `bool clearAll = false` | `void` |
| `DeleteThumbnail` | Deletes a specific thumbnail | `string filePath` | `bool` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `ThumbnailGenerated` | Raised when a thumbnail is generated | `ThumbnailGeneratedEventArgs` (ImagePath, ThumbnailPath) |
| `ThumbnailGenerationProgress` | Raised during batch thumbnail generation | `ProgressEventArgs` (Current, Total) |

**Example Usage**:

```csharp
// Get thumbnail for an image
var thumbnailPath = await _thumbnailService.GetThumbnailAsync(
    "C:\images\test.jpg", 
    width: 512, 
    height: 512);
```

### 3. ISearchService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Provides search functionality for images.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `SearchAsync` | Performs a search query | `string query`, `int page = 1`, `int pageSize = 50` | `Task<SearchResults>` |
| `GetFilterOptionsAsync` | Retrieves available filter options | - | `Task<FilterOptions>` |
| `ClearFilters` | Clears all active filters | - | `void` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `SearchCompleted` | Raised when a search is completed | `SearchCompletedEventArgs` (Results, Query) |
| `SearchStarted` | Raised when a search is started | `SearchStartedEventArgs` (Query) |

**Example Usage**:

```csharp
// Perform a search
var results = await _searchService.SearchAsync(
    "cat model:\"test model\"", 
    page: 1, 
    pageSize: 20);
```

### 4. IMetadataScannerService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Scans images for metadata and updates the database.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ScanDirectoryAsync` | Scans a directory for images and extracts metadata | `string directoryPath`, `bool recursive = true` | `Task<ScanResult>` |
| `ScanFileAsync` | Scans a single file for metadata | `string filePath` | `Task<ScanResult>` |
| `EnqueueFile` | Adds a file to the scanning queue | `string filePath` | `void` |
| `EnqueueDirectory` | Adds a directory to the scanning queue | `string directoryPath`, `bool recursive = true` | `void` |
| `Start` | Starts the scanning service | - | `void` |
| `Stop` | Stops the scanning service | - | `void` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `FileScanned` | Raised when a file is scanned | `FileScannedEventArgs` (FilePath, Success, Error) |
| `ScanCompleted` | Raised when a scan operation is completed | `ScanCompletedEventArgs` (DirectoryPath, FilesScanned, FilesAdded) |

**Example Usage**:

```csharp
// Scan a directory
var result = await _metadataScanner.ScanDirectoryAsync(
    "C:\images", 
    recursive: true);
```

### 5. IScanningService

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Manages directory scanning and file system watching.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `AddWatchDirectory` | Adds a directory to the watch list | `string directoryPath`, `bool recursive = true` | `Task<bool>` |
| `RemoveWatchDirectory` | Removes a directory from the watch list | `string directoryPath` | `Task<bool>` |
| `GetWatchDirectoriesAsync` | Retrieves all watched directories | - | `Task<List<WatchDirectory>>` |
| `RescanDirectoryAsync` | Rescans a specific directory | `string directoryPath` | `Task<ScanResult>` |
| `RescanAllDirectoriesAsync` | Rescans all watched directories | - | `Task<List<ScanResult>>` |

**Events**:

| Event | Description | Event Args |
|-------|-------------|------------|
| `DirectoryWatched` | Raised when a directory is added to the watch list | `DirectoryWatchedEventArgs` (DirectoryPath) |
| `DirectoryUnwatched` | Raised when a directory is removed from the watch list | `DirectoryUnwatchedEventArgs` (DirectoryPath) |

**Example Usage**:

```csharp
// Add directory to watch list
await _scanningService.AddWatchDirectory(
    "C:\images", 
    recursive: true);
```

### 6. IQueryBuilder

**Namespace**: `BerryAIGen.Database`

**Purpose**: Builds SQL queries from natural language search terms.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Parse` | Parses a search prompt into SQL components | `string prompt` | `QueryResult` |
| `QueryPrompt` | Generates a WHERE clause from a search prompt | `string prompt` | `string` |
| `ParseParameters` | Parses parameters from a search prompt | `string prompt` | `Dictionary<string, object>` |

**Example Usage**:

```csharp
// Parse a search query
var result = _queryBuilder.Parse("cat model:\"test model\" steps:20");

// Use the generated WHERE clause and parameters
var images = await _dataStore.GetImagesAsync(
    result.WhereClause, 
    result.Parameters);
```

### 7. IServiceLocator

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Provides dependency resolution and service location.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetService` | Retrieves a service by its type | `Type serviceType` | `object` |
| `GetService<T>` | Retrieves a service by its generic type | - | `T` |
| `Register` | Registers a service implementation | `Type serviceType`, `object implementation`, `Lifetime lifetime = Lifetime.Singleton` | `void` |
| `Register<TService, TImplementation>` | Registers a service implementation with generic types | `Lifetime lifetime = Lifetime.Singleton` | `void` |
| `RegisterInstance` | Registers a service instance | `Type serviceType`, `object instance` | `void` |
| `RegisterInstance<TService>` | Registers a service instance with generic type | `TService instance` | `void` |

**Example Usage**:

```csharp
// Register a service
_serviceLocator.Register<IDataStore, DataStore>(Lifetime.Singleton);

// Resolve a service
var dataStore = _serviceLocator.GetService<IDataStore>();
```

## Civitai API Interfaces

### 8. ICivitaiClient

**Namespace**: `BerryAIGen.Civitai`

**Purpose**: Provides access to the Civitai API for model information.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetModelInfoAsync` | Retrieves information about a model | `string modelId` | `Task<ModelInfo>` |
| `GetModelInfoByHashAsync` | Retrieves model information by its hash | `string hash` | `Task<ModelInfo>` |
| `GetModelsAsync` | Retrieves models with optional filtering | `string search = null`, `int limit = 10`, `int page = 1` | `Task<List<ModelInfo>>` |
| `GetModelVersionsAsync` | Retrieves versions for a model | `string modelId` | `Task<List<ModelVersion>>` |

**Example Usage**:

```csharp
// Get model info by hash
var modelInfo = await _civitaiClient.GetModelInfoByHashAsync(
    "abc123def456");
```

## GitHub API Interfaces

### 9. IGithubClient

**Namespace**: `BerryAIGen.Github`

**Purpose**: Provides access to the GitHub API for release information.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetLatestReleaseAsync` | Retrieves the latest release for a repository | `string owner`, `string repo` | `Task<ReleaseInfo>` |
| `GetReleasesAsync` | Retrieves releases for a repository | `string owner`, `string repo`, `int limit = 10` | `Task<List<ReleaseInfo>>` |
| `GetTagsAsync` | Retrieves tags for a repository | `string owner`, `string repo`, `int limit = 10` | `Task<List<TagInfo>>` |
| `GetReleaseByTagAsync` | Retrieves a release by its tag | `string owner`, `string repo`, `string tag` | `Task<ReleaseInfo>` |

**Example Usage**:

```csharp
// Get latest release
var release = await _githubClient.GetLatestReleaseAsync(
    "RupertAvery", 
    "BerryAIGen.Toolkit");
```

## Scripting Interfaces

### 10. IScriptingEngine

**Namespace**: `BerryAIGen.Scripting`

**Purpose**: Provides scripting capabilities for extensions and automation.

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ExecuteScriptAsync` | Executes a script | `string scriptText`, `ScriptContext context = null` | `Task<ScriptResult>` |
| `ExecuteFileAsync` | Executes a script from a file | `string filePath`, `ScriptContext context = null` | `Task<ScriptResult>` |
| `RegisterObject` | Registers an object for use in scripts | `string name`, `object obj` | `void` |
| `RegisterType` | Registers a type for use in scripts | `Type type` | `void` |

**Example Usage**:

```csharp
// Execute a simple script
var result = await _scriptingEngine.ExecuteScriptAsync(
    "var images = dataStore.GetImagesAsync(); return images.Count;",
    new ScriptContext { { "dataStore", _dataStore } });
```

## Extension Interfaces

### 11. IPlugin

**Namespace**: `BerryAIGen.Toolkit.Plugins`

**Purpose**: Defines the interface for application plugins.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `Name` | `string` | The name of the plugin |
| `Version` | `Version` | The version of the plugin |
| `Author` | `string` | The author of the plugin |
| `Description` | `string` | A description of the plugin |
| `IsEnabled` | `bool` | Whether the plugin is enabled |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Initialize` | Initializes the plugin | `IServiceLocator serviceLocator` | `Task<bool>` |
| `Shutdown` | Shuts down the plugin | - | `Task<bool>` |
| `Enable` | Enables the plugin | - | `Task<bool>` |
| `Disable` | Disables the plugin | - | `Task<bool>` |

**Example Usage**:

```csharp
// Implementing a simple plugin
public class MyPlugin : IPlugin
{
    public string Name => "My Plugin";
    public Version Version => new Version(1, 0, 0);
    public string Author => "Author Name";
    public string Description => "A simple plugin";
    public bool IsEnabled { get; private set; }
    
    public Task<bool> Initialize(IServiceLocator serviceLocator)
    {
        // Initialize plugin
        return Task.FromResult(true);
    }
    
    // Other method implementations
}
```

## Data Models

### 1. Image

**Namespace**: `BerryAIGen.Data`

**Purpose**: Represents an image in the database.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `int` | Primary key |
| `FilePath` | `string` | Full path to the image file |
| `FileName` | `string` | Name of the image file |
| `FolderPath` | `string` | Path to the folder containing the image |
| `ModelName` | `string` | Name of the model used to generate the image |
| `ModelHash` | `string` | Hash of the model used to generate the image |
| `Seed` | `long?` | Seed used to generate the image |
| `Steps` | `int?` | Number of steps used to generate the image |
| `CFGScale` | `double?` | CFG scale used to generate the image |
| `Sampler` | `string` | Sampler used to generate the image |
| `Width` | `int` | Width of the image in pixels |
| `Height` | `int` | Height of the image in pixels |
| `CreatedDate` | `DateTime` | Date the image was created |
| `ModifiedDate` | `DateTime` | Date the image was last modified |
| `Rating` | `int` | User rating (1-10) |
| `IsFavorite` | `bool` | Whether the image is marked as favorite |
| `IsNSFW` | `bool` | Whether the image is marked as NSFW |
| `ThumbnailPath` | `string` | Path to the thumbnail image |

### 2. ImageMetadata

**Namespace**: `BerryAIGen.Data`

**Purpose**: Represents metadata for an image.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `int` | Primary key |
| `ImageId` | `int` | Foreign key to the Image table |
| `Prompt` | `string` | Full prompt used to generate the image |
| `NegativePrompt` | `string` | Negative prompt used to generate the image |
| `MetadataJson` | `string` | Raw metadata in JSON format |
| `CreatedDate` | `DateTime` | Date the metadata was created |
| `ModifiedDate` | `DateTime` | Date the metadata was last modified |

## Enums

### 1. Lifetime

**Namespace**: `BerryAIGen.Toolkit.Services`

**Purpose**: Defines the lifetime of services in the service locator.

**Values**:

| Value | Description |
|-------|-------------|
| `Singleton` | Single instance shared across all requests |
| `Transient` | New instance created for each request |
| `Scoped` | Single instance per scope (not currently used) |

### 2. ScanStatus

**Namespace**: `BerryAIGen.Data`

**Purpose**: Defines the status of a scanning operation.

**Values**:

| Value | Description |
|-------|-------------|
| `Pending` | Scanning has not started |
| `InProgress` | Scanning is in progress |
| `Completed` | Scanning completed successfully |
| `Failed` | Scanning failed |
| `Cancelled` | Scanning was cancelled |

## Error Handling

### 1. ServiceException

**Namespace**: `BerryAIGen.Common.Exceptions`

**Purpose**: Base exception class for service-related errors.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `ServiceName` | `string` | Name of the service that threw the exception |
| `ErrorCode` | `int` | Error code |

**Example Usage**:

```csharp
try
{
    await _thumbnailService.GetThumbnailAsync("invalid/path.jpg");
}
catch (ServiceException ex)
{
    Log.Error(ex, "Failed to generate thumbnail: {Message}", ex.Message);
}
```

## Conclusion

These public interfaces form the core API surface of the BerryAIGen.Toolkit application. By using these interfaces, external components, extensions, and plugins can interact with the application in a structured and maintainable way. The interfaces are designed to be stable, with backward compatibility maintained across versions whenever possible.
