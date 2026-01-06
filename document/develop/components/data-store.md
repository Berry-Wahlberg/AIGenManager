# DataStore Component

## Overview
The DataStore is a core component in the BerryAIGen.Toolkit application that provides database access and management functionality. It is responsible for creating database tables, handling migrations, and providing methods for data retrieval and modification. The DataStore uses SQLite as its database backend.

## Core Implementation

### Location
The DataStore implementation is located in `BerryAIGen.Database/DataStore.cs`.

### Class Structure

```csharp
public partial class DataStore
{
    private readonly object _lock = new object();
    public static int _instances;
    
    public string DatabasePath { get; }
    public bool RescanRequired { get; set; }
    
    private SQLiteConnection? _readOnlyConnection;
    
    public SQLiteConnection OpenReadonlyConnection()
    {
        // Implementation for read-only connection
    }
    
    public SQLiteConnection OpenConnection()
    {
        // Implementation for read-write connection
    }
    
    public DataStore(string databasePath)
    {
        // Constructor implementation
    }
    
    public async Task Create(Func<object> notify, Action<object> complete)
    {
        // Implementation for database creation and migration
    }
    
    public void Close()
    {
        // Implementation for closing connections
    }
    
    public void RebuildIndexes()
    {
        // Implementation for rebuilding indexes
    }
    
    public void CreateBackup()
    {
        // Implementation for creating backups
    }
    
    public bool TryRestoreBackup(string path)
    {
        // Implementation for restoring backups
    }
}
```

## Key Features

### 1. Database Connection Management
- **Read-Write Connections**: For data modification operations
- **Read-Only Connections**: For query operations, optimized for performance
- **Connection Pooling**: Efficient management of database connections
- **Proper Disposal**: Ensures connections are properly closed when not in use

### 2. Database Creation and Initialization
- **Table Creation**: Creates all necessary database tables
- **Index Creation**: Creates comprehensive indexes for fast querying
- **Migration Support**: Handles database schema migrations automatically
- **SQLite Extensions**: Attempts to load SQLite extensions for enhanced functionality

### 3. Migration System
- **Pre and Post Migrations**: Supports migrations that run before and after table creation
- **Versioned Migrations**: Each migration has a unique version number
- **Automatic Execution**: Migrations are executed automatically when needed
- **Safe Execution**: Migrations are designed to be safe and avoid data loss

### 4. Data Access Methods
The DataStore provides a wide range of methods for accessing and modifying data. These methods are implemented in partial classes for better organization:

#### Image Data Access
- Methods for adding, updating, and deleting images
- Methods for querying images by various criteria
- Methods for updating image properties (rating, favorite, NSFW, etc.)

#### Folder Data Access
- Methods for managing folders
- Methods for querying folders
- Methods for updating folder properties

#### Tag Data Access
- Methods for managing tags
- Methods for querying tags
- Methods for associating tags with images

#### Album Data Access
- Methods for managing albums
- Methods for querying albums
- Methods for adding and removing images from albums

#### Query Data Access
- Methods for managing saved queries
- Methods for executing queries

### 5. Backup and Restore
- **Create Backup**: Creates a backup of the entire database
- **Restore Backup**: Attempts to restore a database from a backup file
- **Safe Restoration**: Ensures restoration is performed safely to avoid data corruption

### 6. Index Management
- **Rebuild Indexes**: Rebuilds database indexes for improved performance
- **Automatic Index Creation**: Creates indexes during database initialization
- **Comprehensive Indexing**: Indexes all frequently queried columns

## Database Schema

The DataStore creates and manages the following tables:

| Table | Description |
|-------|-------------|
| **Images** | Stores image metadata and properties |
| **Folders** | Stores folder information |
| **Tags** | Stores user-defined tags |
| **ImageTags** | Junction table for image-tag relationships |
| **Albums** | Stores album information |
| **AlbumImages** | Junction table for album-image relationships |
| **Nodes** | Stores ComfyUI nodes from workflows |
| **NodeProperties** | Stores properties of ComfyUI nodes |
| **Queries** | Stores saved search queries |

## Usage Examples

### Creating a DataStore Instance

```csharp
// Create a new DataStore instance
var dataStore = new DataStore("database.db");
```

### Initializing the Database

```csharp
// Initialize the database with migration support
await dataStore.Create(
    () => Console.WriteLine("Updating database..."),
    (handle) => Console.WriteLine("Database update complete")
);
```

### Opening a Connection

```csharp
// Open a read-write connection for data modification
using var db = dataStore.OpenConnection();

// Open a read-only connection for queries
using var readonlyDb = dataStore.OpenReadonlyConnection();
```

### Creating a Backup

```csharp
// Create a backup of the database
dataStore.CreateBackup();
```

### Restoring from a Backup

```csharp
// Attempt to restore from a backup
if (dataStore.TryRestoreBackup("backup.db"))
{
    Console.WriteLine("Backup restored successfully");
}
else
{
    Console.WriteLine("Failed to restore backup");
}
```

## Performance Optimization

### 1. Read-Only Connections
- **Optimized for Queries**: Read-only connections use shared cache and are optimized for query performance
- **Reduced Contention**: Don't block write operations
- **Faster Access**: Avoid write locks and other overhead associated with read-write connections

### 2. Comprehensive Indexing
The DataStore creates indexes for frequently queried columns:
- `Image.Path` for fast path lookups
- `Image.ModelHash`, `Image.Seed`, `Image.Sampler` for metadata queries
- `Image.Favorite`, `Image.NSFW`, `Image.ForDeletion` for filtering
- Join columns like `ImageTag.ImageId` and `ImageTag.TagId` for efficient joins

### 3. Batch Operations
- **Batch Inserts**: For adding multiple records efficiently
- **Batch Updates**: For updating multiple records in a single operation
- **Batch Deletes**: For deleting multiple records efficiently

### 4. Query Optimization
- **Parameterized Queries**: To avoid SQL injection and improve query performance
- **Efficient Query Planning**: SQLite's query planner uses indexes effectively
- **Lazy Loading**: Data is loaded only when needed

## Migration Examples

The DataStore executes migrations to update the database schema. Some examples of migrations include:

```csharp
// Migration to cleanup orphaned album image entries
[Migration("RupertAvery20231224_0001_CleanupOrphanedAlbumImageEntries")]
private void CleanupOrphanedAlbumImageEntries(SQLiteConnection db)
{
    // Implementation to remove orphaned entries
}

// Migration to add foreign keys to AlbumImage table
[Migration("RupertAvery20231224_0002_AlbumImageForeignKeys")]
private void AddAlbumImageForeignKeys(SQLiteConnection db)
{
    // Implementation to add foreign keys
}
```

## Error Handling

### 1. SQLite Extension Loading
The DataStore attempts to load SQLite extensions for enhanced functionality, but handles cases where extensions are not available gracefully:

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
            // Continue execution without extension
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Failed to enable SQLite extension loading: {ex.Message}");
    // Continue execution without extensions
}
```

### 2. Database Operations
The DataStore includes proper error handling for all database operations, ensuring the application remains stable even when database errors occur.

## Integration with Other Components

### 1. ServiceLocator
The DataStore is registered with the ServiceLocator during application startup:

```csharp
var dataStore = new DataStore(AppInfo.DatabasePath);
ServiceLocator.SetDataStore(dataStore);
```

### 2. Services
Many services depend on the DataStore for data access:
- **SearchService**: Uses the DataStore to execute search queries
- **ScanningService**: Uses the DataStore to save scanned images
- **AlbumService**: Uses the DataStore to manage albums
- **TagService**: Uses the DataStore to manage tags

### 3. DatabaseWriterService
The DatabaseWriterService uses the DataStore to write data changes, providing a layer of abstraction for database operations.

## Conclusion

The DataStore component is a critical part of the BerryAIGen.Toolkit application, providing comprehensive database management functionality. Its efficient design, robust error handling, and comprehensive feature set make it a reliable foundation for the application's data storage needs. The DataStore's support for migrations, backup and restore, and optimized querying ensures the application can scale and adapt to changing requirements over time.
