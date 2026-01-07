# ThumbnailService Component

## Overview

The ThumbnailService is a core component of the BerryAIGen.Toolkit application responsible for generating, caching, and managing thumbnails for images and videos. It provides an efficient, asynchronous way to generate thumbnails with support for batch processing, caching, and parallel execution.

## Core Implementation

### Location
The ThumbnailService implementation is located in `BerryAIGen.Toolkit/Thumbnails/ThumbnailService.cs`.

### Class Structure

```csharp
public class ThumbnailService
{
    private static ThumbnailService? _instance;
    private readonly Channel<Job<ThumbnailJob, ThumbailResult>> _channel;
    private readonly int _degreeOfParallelism = 2;
    private CancellationTokenSource cancellationTokenSource;
    private Stream _defaultStream;
    
    // Constructor
    public ThumbnailService();
    
    // Methods
    public void RebuildThumbnail(ImageEntry image);
    public void QueueImage(ImageEntry image);
    public void Stop();
    public Task Start();
    public Task StartAsync();
    public long StartBatch();
    public void StopCurrentBatch();
    public Task QueueAsync(ThumbnailJob job, Action<ThumbailResult> completion);
    public BitmapImage GetThumbnailDirect(ImageType type, string path, int width, int height);
    public (int, int) GetBitmapSize(string path);
    public BitmapImage GetThumbnailImmediate(ImageType type, string path, int width, int height, int size);
    
    // Properties
    public int Size { get; set; }
    public bool EnableCache { get; set; }
}
```

## Key Features

### 1. Asynchronous Thumbnail Generation

The ThumbnailService uses a channel-based job queue to handle thumbnail generation asynchronously:
- Jobs are queued and processed in the background
- Multiple worker threads process jobs in parallel
- Configurable degree of parallelism (default: 2)
- Callbacks notify when thumbnails are ready

### 2. Caching Mechanism

The ThumbnailService integrates with ThumbnailCache to optimize performance:
- Thumbnails are cached based on file path and size
- Cache can be enabled/disabled dynamically
- Cache is cleared when thumbnail size changes
- Support for rebuilding thumbnails when needed

### 3. Batch Processing

The ThumbnailService supports batch processing:
- Generate batch IDs for related thumbnail requests
- Cancel processing for outdated batches
- Efficiently manage large sets of thumbnail requests
- Improve performance when displaying search results or large collections

### 4. Multi-Format Support

The ThumbnailService handles various media formats:
- Image files (PNG, JPG, etc.)
- Video files (MP4, WebM, MKV via FrameExtractor)
- Support for different image types through ImageType enum

### 5. Configurable Thumbnail Size

- Dynamic thumbnail size adjustment
- Consistent thumbnail dimensions across the application
- Automatic recalculation when size changes

## Core Methods

### Queueing Methods

#### `QueueImage(ImageEntry image)`
```csharp
public void QueueImage(ImageEntry image)
{
    // Add image to thumbnail generation queue
}
```
Adds an image to the thumbnail generation queue. The image's LoadState is set to Loading, and updated to Loaded when the thumbnail is ready.

#### `RebuildThumbnail(ImageEntry image)`
```csharp
public void RebuildThumbnail(ImageEntry image)
{
    // Rebuild thumbnail for an image
}
```
Rebuilds a thumbnail for an image, bypassing the cache. Useful when images have been modified.

### Service Lifecycle Methods

#### `StartAsync()`
```csharp
public Task StartAsync()
{
    // Start thumbnail generation workers
}
```
Starts the thumbnail generation worker threads asynchronously.

#### `Stop()`
```csharp
public void Stop()
{
    // Stop thumbnail generation workers
}
```
Stops the thumbnail generation worker threads and cancels pending jobs.

### Batch Management Methods

#### `StartBatch()`
```csharp
public long StartBatch()
{
    // Generate a new batch ID
}
```
Generates a new batch ID for grouping thumbnail requests.

#### `StopCurrentBatch()`
```csharp
public void StopCurrentBatch()
{
    // Stop processing the current batch
}
```
Stops processing the current batch, useful when navigating away from a view.

### Synchronous Methods

#### `GetThumbnailImmediate()`
```csharp
public BitmapImage GetThumbnailImmediate(ImageType type, string path, int width, int height, int size)
{
    // Synchronously generate a thumbnail
}
```
Synchronously generates a thumbnail for a file, useful for immediate display needs.

#### `GetThumbnailDirect()`
```csharp
public BitmapImage GetThumbnailDirect(ImageType type, string path, int width, int height)
{
    // Directly generate a thumbnail
}
```
Directly generates a thumbnail using the current configured size.

## Thumbnail Generation Workflow

1. **Queueing**: `QueueImage()` or `RebuildThumbnail()` is called with an ImageEntry
2. **Job Creation**: A ThumbnailJob is created with image details
3. **Enqueueing**: The job is added to the Channel queue
4. **Processing**: Worker threads dequeue jobs in parallel
5. **Cache Check**: If caching is enabled, check if thumbnail exists in cache
6. **Thumbnail Generation**: If not in cache or rebuild requested, generate thumbnail
7. **Cache Update**: Update cache with new thumbnail
8. **Completion**: Callback is invoked with the thumbnail result
9. **UI Update**: ImageEntry properties are updated on UI thread via Dispatcher

## Integration with Other Components

### ImageEntry

The ThumbnailService works closely with ImageEntry objects:
- Updates LoadState during thumbnail generation
- Sets Thumbnail property when ready
- Updates ThumbnailHeight and ThumbnailWidth
- Sets Unavailable flag if thumbnail generation fails

### ThumbnailCache

The ThumbnailCache (in `BerryAIGen.Toolkit/Thumbnails/ThumbnailCache.cs`) provides:
- Memory-based caching of thumbnails
- Efficient lookup by file path and size
- Cache eviction policies
- Thread-safe operations

### FrameExtractor

For video thumbnails, the ThumbnailService uses FrameExtractor from BerryAIGen.Video:
- Extracts frames from video files
- Converts frames to PNG for thumbnails
- Supports various video formats

### ServiceLocator

The ThumbnailService uses ServiceLocator for:
- Accessing the Dispatcher for UI updates
- Integration with other services

## Performance Considerations

### Parallel Processing

- Configurable degree of parallelism (default: 2)
- Balances CPU usage and thumbnail generation speed
- Can be adjusted based on hardware capabilities

### Caching Strategy

- Memory-efficient caching
- Reduces disk I/O for repeated thumbnail requests
- Clears cache appropriately when settings change

### Batch Optimization

- Cancels processing for outdated batches
- Prioritizes current view content
- Reduces unnecessary work when navigating between views

### Asynchronous Design

- Non-blocking API for UI responsiveness
- Background processing avoids UI freezes
- Callbacks ensure timely UI updates

## Usage Examples

### Basic Usage

```csharp
// Get thumbnail service instance
var thumbnailService = ServiceLocator.ThumbnailService;

// Start the service
await thumbnailService.StartAsync();

// Queue an image for thumbnail generation
thumbnailService.QueueImage(imageEntry);

// Stop the service when no longer needed
thumbnailService.Stop();
```

### Batch Processing

```csharp
// Start a new batch
var batchId = thumbnailService.StartBatch();

// Queue multiple images with the same batch ID
foreach (var image in images)
{
    image.BatchId = batchId;
    thumbnailService.QueueImage(image);
}

// Stop processing when navigating away
thumbnailService.StopCurrentBatch();
```

### Rebuilding Thumbnails

```csharp
// Rebuild thumbnail for a specific image
thumbnailService.RebuildThumbnail(imageEntry);
```

## Configuration

### Thumbnail Size

```csharp
// Set thumbnail size
thumbnailService.Size = 256;

// Get current thumbnail size
var currentSize = thumbnailService.Size;
```

### Cache Control

```csharp
// Enable/disable cache
thumbnailService.EnableCache = true;

// Check cache status
var isCacheEnabled = thumbnailService.EnableCache;
```

## Best Practices

### 1. Proper Lifecycle Management

- Start the ThumbnailService during application initialization
- Stop the service when the application closes
- Use StartAsync() for non-blocking startup

### 2. Batch Processing for Large Collections

- Use batch IDs when displaying search results
- Call StartBatch() when loading new views
- Call StopCurrentBatch() when navigating away

### 3. Cache Management

- Keep cache enabled for better performance
- Use RebuildThumbnail() when images change
- Consider memory usage when setting cache size

### 4. UI Thread Safety

- Always update UI properties via Dispatcher
- Use callbacks to update UI when thumbnails are ready
- Avoid synchronous thumbnail generation on UI thread

## Conclusion

The ThumbnailService is a critical component for providing a smooth user experience in BerryAIGen.Toolkit. Its asynchronous design, caching mechanism, and batch processing capabilities ensure efficient thumbnail generation even for large collections. By understanding how the ThumbnailService works and following best practices, developers can optimize performance and responsiveness when working with media thumbnails.

## Related Documentation

- [ServiceLocator Component](./service-locator.md)
- [Scanning Service Component](./scanning-service.md)
- [Search Service Component](./search-service.md)
- [DataStore Component](./data-store.md)
- [MVVM Pattern](./../ui-ux/mvvm-pattern.md)
