# MetadataScannerService Component

## Overview

The MetadataScannerService is a core component of the BerryAIGen.Toolkit application responsible for scanning files, extracting metadata from AI-generated images, and queueing the extracted metadata for database storage. It provides an efficient, asynchronous way to process large numbers of files with support for parallel execution, batch processing, and queue management.

## Core Implementation

### Location
The MetadataScannerService implementation is located in `BerryAIGen.Toolkit/Services/MetadataScannerService.cs`.

### Class Structure

```csharp
public class MetadataScannerService
{
    private Channel<FileScanJob> _channel;
    private Channel<FileScanJob> _queueChannel;
    private CancellationTokenSource _cancellationTokenSource;
    private readonly int _degreeOfParallelism = 2;
    
    // Methods
    public async Task QueueBatchAsync(IEnumerable<string> paths, ScanCompletionEvent scanCompletionEvent, CancellationToken cancellationToken);
    public void StartQueue(CancellationToken cancellationToken);
    public async Task QueueAsync(string path, CancellationToken cancellationToken);
    public StartResult<int> StartAsync(CancellationToken token);
    public void Stop();
    public void Close();
    
    // Private methods
    private async Task<int> ProcessTaskAsync(CancellationToken token);
    private async Task<int> ProcessQueueTaskAsync(CancellationToken token);
}
```

## Key Features

### 1. Asynchronous Metadata Extraction

The MetadataScannerService uses channel-based job queues to handle metadata extraction asynchronously:
- Two separate channels for batch processing and continuous queue processing
- Configurable degree of parallelism (default: 2)
- CancellationToken support for graceful cancellation
- Non-blocking API design

### 2. Batch Processing Support

The service supports batch processing for efficient scanning of multiple files:
- `QueueBatchAsync()` method for processing large sets of files
- Progress tracking through ProgressService
- Completion events for metadata extraction and database writing
- Automatic refresh of search results when batch processing completes

### 3. Continuous Queue Processing

For real-time metadata extraction:
- `QueueAsync()` method for adding individual files to the queue
- `StartQueue()` method to begin processing the queue
- Continuous processing of files as they are added
- Integration with file system watchers

### 4. Metadata Extraction

The service uses the `Metadata.ReadFromFile()` method from BerryAIGen.IO to:
- Extract PNGInfo from AI-generated images
- Read metadata from various AI image generation platforms
- Extract file hashes for duplicate detection
- Parse generation parameters (seed, steps, CFG scale, etc.)

### 5. Duplicate Detection

The service implements duplicate detection based on file hashes:
- Checks if a file with the same hash already exists in the database
- Handles file moves by updating paths for existing hashes
- Prevents duplicate entries while maintaining file integrity

### 6. Integration with DatabaseWriterService

The MetadataScannerService works closely with DatabaseWriterService to:
- Queue metadata for database storage
- Support different queue types (Add, Update, Move, Skip)
- Batch database writes for improved performance
- Handle both metadata extraction and database operations asynchronously

## Core Methods

### Batch Processing Methods

#### `QueueBatchAsync()`
```csharp
public async Task QueueBatchAsync(IEnumerable<string> paths, ScanCompletionEvent scanCompletionEvent, CancellationToken cancellationToken)
{
    // Queue multiple files for metadata extraction
}
```
Queues a batch of files for metadata extraction, starts the processing service, and handles completion events.

#### `StartAsync()`
```csharp
public StartResult<int> StartAsync(CancellationToken token)
{
    // Start the metadata extraction service
}
```
Starts the metadata extraction workers and returns a StartResult indicating if the service was started.

#### `Stop()`
```csharp
public void Stop()
{
    // Stop the metadata extraction service
}
```
Stops the metadata extraction service by canceling the CancellationToken.

### Continuous Queue Methods

#### `StartQueue()`
```csharp
public void StartQueue(CancellationToken cancellationToken)
{
    // Start the continuous queue processor
}
```
Starts the continuous queue processor if it's not already running.

#### `QueueAsync()`
```csharp
public async Task QueueAsync(string path, CancellationToken cancellationToken)
{
    // Add a single file to the processing queue
}
```
Adds a single file to the continuous processing queue.

## Metadata Extraction Workflow

1. **File Queueing**: Files are added to either the batch channel or continuous queue channel
2. **Worker Processing**: Worker threads dequeue files for processing
3. **Metadata Extraction**: `Metadata.ReadFromFile()` extracts metadata from the file
4. **Duplicate Check**: The service checks if a file with the same hash exists in the database
5. **Queue for Database**: Files are queued for database writing based on their status:
   - **Add**: New file not in database
   - **Update**: Existing file with updated metadata
   - **Move**: File moved to new location with same hash
   - **Skip**: File doesn't exist or can't be processed
6. **Database Writing**: DatabaseWriterService processes the queue and writes to the database
7. **Completion Handling**: Events are raised when processing completes

## Integration with Other Components

### Metadata (BerryAIGen.IO)

The MetadataScannerService uses the Metadata class from BerryAIGen.IO to:
- Extract PNGInfo from AI-generated images
- Parse generation parameters
- Calculate file hashes

### DatabaseWriterService

The service works closely with DatabaseWriterService to:
- Queue metadata for database storage
- Batch database operations
- Handle different queue types

### DataStore

The service uses DataStore to:
- Check for existing images by hash
- Update file paths for moved files
- Verify file existence in the database

### ProgressService

The service integrates with ProgressService to:
- Track scanning progress
- Update status messages
- Signal task completion

### ToastService

After batch processing completes, the service uses ToastService to:
- Display notifications about the number of images added/updated
- Provide user feedback on scanning results

## Performance Considerations

### Parallel Execution

- Configurable degree of parallelism (default: 2)
- Balances CPU usage and metadata extraction speed
- Can be adjusted based on hardware capabilities

### Batch Processing Efficiency

- Files are processed in batches of 33 for progress reporting
- Batch completion triggers database writes
- Automatic refresh of search results

### Memory Management

- Channel-based architecture prevents memory bloat
- Files are processed one at a time per worker
- No large in-memory collections of files

### Database Optimization

- Database writes are batched by DatabaseWriterService
- Separation of metadata extraction and database operations
- Asynchronous database writes don't block metadata extraction

## Usage Examples

### Batch Scanning

```csharp
// Get metadata scanner service instance
var metadataScanner = ServiceLocator.MetadataScannerService;

// Create scan completion event
var scanCompletion = new ScanCompletionEvent
{
    OnMetadataCompleted = () => Console.WriteLine("Metadata extraction completed"),
    OnDatabaseWriteCompleted = () => Console.WriteLine("Database writing completed")
};

// Get files to scan
var files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

// Queue batch for scanning
await metadataScanner.QueueBatchAsync(files, scanCompletion, cancellationToken);
```

### Continuous Queue Processing

```csharp
// Start the queue processor
metadataScanner.StartQueue(cancellationToken);

// Add files to the queue as they are detected
foreach (var file in newlyAddedFiles)
{
    await metadataScanner.QueueAsync(file, cancellationToken);
}
```

### Starting and Stopping the Service

```csharp
// Start the service
var startResult = metadataScanner.StartAsync(cancellationToken);

// Check if the service was started
if (!startResult.IsStarted)
{
    // Wait for completion
    var processedCount = await startResult.Task;
    Console.WriteLine($"Processed {processedCount} files");
}

// Stop the service when done
metadataScanner.Stop();
```

## Best Practices

### 1. Proper CancellationToken Usage

- Always provide a CancellationToken for long-running operations
- Use linked cancellation tokens when combining multiple cancellation sources
- Implement graceful shutdown in applications

### 2. Batch Processing for Large Collections

- Use `QueueBatchAsync()` for scanning large numbers of files
- Implement progress reporting for user feedback
- Handle completion events appropriately

### 3. Continuous Queue for Real-Time Operations

- Use `StartQueue()` and `QueueAsync()` for real-time file monitoring
- Ensure the queue processor is started before adding files
- Consider the performance impact of continuous scanning

### 4. Error Handling

- The service includes try-catch blocks for file processing
- Logs errors for troubleshooting
- Continues processing other files even if one fails
- Handles missing files gracefully

### 5. Resource Management

- Start the service only when needed
- Stop the service when no longer in use
- Dispose of resources properly

## Conclusion

The MetadataScannerService is a critical component for the BerryAIGen.Toolkit application, responsible for extracting metadata from AI-generated images and preparing it for database storage. Its asynchronous, parallel design ensures efficient processing of large numbers of files, while its integration with other services provides a seamless workflow from file scanning to search results. Understanding how the MetadataScannerService works is essential for developing and maintaining the application's core functionality.

## Related Documentation

- [Scanning Service Component](./scanning-service.md)
- [ServiceLocator Component](./service-locator.md)
- [DataStore Component](./data-store.md)
- [DatabaseWriterService](./database-writer-service.md)
- [BerryAIGen.IO Module](./../modules/berryaigen-io.md)
