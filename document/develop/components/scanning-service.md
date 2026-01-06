# ScanningService Component

## Overview
The ScanningService is a core component in the BerryAIGen.Toolkit application responsible for scanning folders for image files, extracting metadata from those files, and updating the database with the extracted information. It provides functionality for both full scans and incremental scans of watched folders.

## Core Implementation

### Location
The ScanningService implementation is located in `BerryAIGen.Toolkit/Services/ScanningService.cs`.

### Key Features

1. **Folder Scanning**
   - Scans specified folders for image files
   - Supports recursive scanning of subfolders
   - Detects new, modified, and deleted files
   - Handles both full scans and incremental scans

2. **Metadata Extraction**
   - Extracts metadata from various image formats
   - Supports multiple metadata formats from different AI image generators
   - Extracts prompts, negative prompts, generation parameters, and other metadata

3. **Database Updates**
   - Adds new image records to the database
   - Updates existing records with new or changed metadata
   - Marks deleted files as unavailable
   - Maintains database integrity

4. **Progress Reporting**
   - Reports scan progress to the UI
   - Provides detailed information about scan operations
   - Supports cancellation of ongoing scans

5. **Error Handling**
   - Robust error handling for corrupt files
   - Graceful handling of access denied errors
   - Detailed logging of scan errors

## Scan Types

### 1. Full Scan
- Scans all files in specified folders
- Rechecks all files, including those already in the database
- Updates metadata for all files
- Useful when initializing the application or when metadata extraction logic changes

### 2. Incremental Scan
- Scans only files that have been added or modified since the last scan
- Faster than full scans
- Automatically performed on watched folders
- Uses file system change notifications when available

### 3. Watch Mode
- Monitors watched folders for changes
- Automatically scans new or modified files
- Provides near-real-time updates to the database
- Reduces the need for manual scans

## Scan Process

### 1. Initialization
- Validates scan parameters
- Sets up progress reporting
- Initializes metadata extractors

### 2. Folder Enumeration
- Enumerates files in specified folders
- Filters files by supported extensions
- Determines which files need scanning

### 3. File Processing
- Reads each file
- Extracts metadata using MetadataScanner
- Generates thumbnails if needed
- Validates extracted metadata

### 4. Database Update
- Adds new records to the database
- Updates existing records
- Marks deleted files as unavailable
- Maintains referential integrity

### 5. Cleanup
- Cleans up temporary resources
- Reports final scan results
- Updates scan statistics

## Usage Examples

### Starting a Scan

```csharp
// Get the ScanningService from ServiceLocator
var scanningService = ServiceLocator.ScanningService;

// Start a full scan of specified folders
var folders = new List<string> { "C:\\Images\\AI", "D:\\AI-Generated" };
task.Run(async () =>
{
    if (await ServiceLocator.ProgressService.TryStartTask())
    {
        try
        {
            await scanningService.ScanFolders(folders, true, true, ServiceLocator.ProgressService.CancellationToken);
        }
        finally
        {
            ServiceLocator.ProgressService.CompleteTask();
            ServiceLocator.ProgressService.SetStatus("Scan completed");
        }
    }
});
```

### Scanning Watched Folders

```csharp
// Scan all watched folders incrementally
Task.Run(async () =>
{
    if (await ServiceLocator.ProgressService.TryStartTask())
    {
        try
        {
            await scanningService.ScanWatchedFolders(false, false, ServiceLocator.ProgressService.CancellationToken);
        }
        finally
        {
            ServiceLocator.ProgressService.CompleteTask();
        }
    }
});
```

### Scanning for Unavailable Files

```csharp
// Scan for files that are no longer available
var windowModel = new UnavailableFilesModel();
Task.Run(async () =>
{
    if (await ServiceLocator.ProgressService.TryStartTask())
    {
        try
        {
            await scanningService.ScanUnavailable(windowModel, ServiceLocator.ProgressService.CancellationToken);
        }
        finally
        {
            ServiceLocator.ProgressService.CompleteTask();
        }
    }
});
```

## Integration with Other Components

### 1. MetadataScanner
- Uses the MetadataScanner from the BerryAIGen.IO module to extract metadata
- Supports multiple metadata formats
- Handles various image file types

### 2. DatabaseWriterService
- Uses the DatabaseWriterService to write scan results to the database
- Ensures database operations are performed efficiently
- Maintains database integrity

### 3. ProgressService
- Reports scan progress to the UI
- Allows cancellation of ongoing scans
- Provides detailed status updates

### 4. FolderService
- Coordinates with the FolderService to determine which folders to scan
- Gets information about watched folders
- Updates folder status information

## Performance Optimization

### 1. Asynchronous Processing
- Performs scan operations asynchronously
- Keeps the UI responsive during scans
- Uses parallel processing for metadata extraction

### 2. Incremental Scans
- Only scans files that have changed since the last scan
- Uses file modification times to determine which files need scanning
- Avoids unnecessary processing of unchanged files

### 3. Caching
- Caches scan results to avoid redundant processing
- Uses database records to determine which files need scanning
- Caches metadata extractors for reuse

### 4. Efficient File I/O
- Reads files in optimized chunks
- Uses async file I/O operations
- Properly disposes of file handles

## Error Handling

### 1. Corrupt Files
- Detects and skips corrupt files
- Logs detailed error information
- Continues scanning other files

### 2. Access Denied Errors
- Handles access denied errors gracefully
- Skips folders that cannot be accessed
- Reports errors to the user

### 3. Invalid Metadata
- Validates extracted metadata
- Handles files with invalid or missing metadata
- Adds files to the database even without metadata

## Scan Events

The ScanningService raises various events to notify the UI of scan progress and results:

- **ScanningStarted**: Raised when a scan starts
- **FileScanned**: Raised when a file is scanned
- **ScanProgress**: Raised to report scan progress
- **ScanCompleted**: Raised when a scan completes
- **ScanError**: Raised when an error occurs during scanning

## Conclusion

The ScanningService is a critical component in the BerryAIGen.Toolkit application, responsible for keeping the database in sync with the file system. Its efficient design, robust error handling, and support for both full and incremental scans make it a reliable tool for managing large collections of AI-generated images. By working closely with other components like MetadataScanner and DatabaseWriterService, it ensures that the application always has up-to-date information about the user's image collection.

