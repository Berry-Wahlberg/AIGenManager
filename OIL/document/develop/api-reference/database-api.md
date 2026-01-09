# Database API Reference

## Overview

This document provides detailed reference documentation for the database access layer API of the BerryAIGen.Toolkit application. The database layer is responsible for all interactions with the SQLite database, including data retrieval, storage, and manipulation.

## Database Architecture

### 1. Core Components

#### DataStore

**Namespace**: `BerryAIGen.Database`

**Purpose**: Provides high-level database operations for images and metadata.

**Inheritance**: `IDataStore`

**Constructors**:

| Constructor | Description | Parameters |
|-------------|-------------|------------|
| `DataStore` | Creates a new DataStore instance | `IDbConnection connection` |
| `DataStore` | Creates a new DataStore instance with connection string | `string connectionString` |

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `Connection` | `IDbConnection` | The database connection |
| `IsConnected` | `bool` | Whether the connection is open |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ConnectAsync` | Opens the database connection | - | `Task<bool>` |
| `DisconnectAsync` | Closes the database connection | - | `Task<bool>` |
| `InitializeDatabaseAsync` | Initializes the database schema | - | `Task<bool>` |
| `AddImageAsync` | Adds a new image to the database | `Image image` | `Task<int>` (Image ID) |
| `GetImageByIdAsync` | Retrieves an image by its ID | `int id` | `Task<Image>` |
| `GetImagesAsync` | Retrieves images with optional filtering | `string whereClause = null`, `object parameters = null` | `Task<List<Image>>` |
| `UpdateImageAsync` | Updates an existing image | `Image image` | `Task<bool>` |
| `DeleteImageAsync` | Deletes an image by its ID | `int id` | `Task<bool>` |
| `GetImageMetadataAsync` | Retrieves metadata for an image | `int imageId` | `Task<ImageMetadata>` |
| `UpdateImageMetadataAsync` | Updates metadata for an image | `ImageMetadata metadata` | `Task<bool>` |
| `GetImagesWithMetadataAsync` | Retrieves images with their metadata | `string whereClause = null`, `object parameters = null` | `Task<List<ImageWithMetadata>>` |
| `BulkInsertImagesAsync` | Inserts multiple images in bulk | `IEnumerable<Image> images` | `Task<int>` (Number of images inserted) |
| `GetTotalImagesCountAsync` | Gets the total number of images | `string whereClause = null`, `object parameters = null` | `Task<int>` |
| `ExecuteQueryAsync` | Executes a raw SQL query | `string sql`, `object parameters = null` | `Task<int>` (Number of rows affected) |
| `QueryAsync` | Executes a query and returns results | `string sql`, `object parameters = null` | `Task<List<T>>` |

**Example Usage**:

```csharp
// Create a DataStore instance
using var connection = new SqliteConnection("DataSource=app.db");
var dataStore = new DataStore(connection);

// Initialize the database
await dataStore.InitializeDatabaseAsync();

// Add a new image
var imageId = await dataStore.AddImageAsync(new Image
{
    FilePath = "C:\images\test.jpg",
    ModelName = "test model",
    Seed = 12345,
    Steps = 20,
    CFGScale = 7.5
});

// Get images with metadata
var images = await dataStore.GetImagesWithMetadataAsync(
    "model = @model AND steps >= @steps",
    new { model = "test model", steps = 20 });
```

#### QueryBuilder

**Namespace**: `BerryAIGen.Database`

**Purpose**: Builds SQL queries from natural language search terms.

**Inheritance**: `IQueryBuilder`

**Constructors**:

| Constructor | Description | Parameters |
|-------------|-------------|------------|
| `QueryBuilder` | Creates a new QueryBuilder instance | - |

**Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Parse` | Parses a search prompt into SQL components | `string prompt` | `QueryResult` |
| `QueryPrompt` | Generates a WHERE clause from a search prompt | `string prompt` | `string` |
| `ParseParameters` | Parses parameters from a search prompt | `string prompt` | `Dictionary<string, object>` |
| `BuildQuery` | Builds a complete SQL query | `string select = "*"`, `string table = "images"`, `string whereClause = null`, `string orderBy = null`, `int? limit = null`, `int? offset = null` | `string` |

**Example Usage**:

```csharp
// Create a QueryBuilder instance
var queryBuilder = new QueryBuilder();

// Parse a search query
var queryResult = queryBuilder.Parse(
    "cat model:\"test model\" steps:20-30 cfg:7.0-8.0");

// Build a complete SQL query
var sql = queryBuilder.BuildQuery(
    select: "id, filePath, model, seed, steps",
    table: "images",
    whereClause: queryResult.WhereClause,
    orderBy: "createdDate DESC",
    limit: 100,
    offset: 0);

// Execute the query
var images = await _dataStore.QueryAsync<Image>(sql, queryResult.Parameters);
```

### 2. Database Models

#### Image

**Namespace**: `BerryAIGen.Data`

**Purpose**: Represents an image in the database.

**Properties**:

| Property | Type | Description | Column Name |
|----------|------|-------------|-------------|
| `Id` | `int` | Primary key | `id` |
| `FilePath` | `string` | Full path to the image file | `filePath` |
| `FileName` | `string` | Name of the image file | `fileName` |
| `FolderPath` | `string` | Path to the folder containing the image | `folderPath` |
| `ModelName` | `string` | Name of the model used to generate the image | `model` |
| `ModelHash` | `string` | Hash of the model used to generate the image | `modelHash` |
| `Seed` | `long?` | Seed used to generate the image | `seed` |
| `Steps` | `int?` | Number of steps used to generate the image | `steps` |
| `CFGScale` | `double?` | CFG scale used to generate the image | `cfgScale` |
| `Sampler` | `string` | Sampler used to generate the image | `sampler` |
| `Width` | `int` | Width of the image in pixels | `width` |
| `Height` | `int` | Height of the image in pixels | `height` |
| `CreatedDate` | `DateTime` | Date the image was created | `createdDate` |
| `ModifiedDate` | `DateTime` | Date the image was last modified | `modifiedDate` |
| `Rating` | `int` | User rating (1-10) | `rating` |
| `IsFavorite` | `bool` | Whether the image is marked as favorite | `isFavorite` |
| `IsNSFW` | `bool` | Whether the image is marked as NSFW | `isNSFW` |
| `ThumbnailPath` | `string` | Path to the thumbnail image | `thumbnailPath` |

#### ImageMetadata

**Namespace**: `BerryAIGen.Data`

**Purpose**: Represents metadata for an image.

**Properties**:

| Property | Type | Description | Column Name |
|----------|------|-------------|-------------|
| `Id` | `int` | Primary key | `id` |
| `ImageId` | `int` | Foreign key to the Image table | `imageId` |
| `Prompt` | `string` | Full prompt used to generate the image | `prompt` |
| `NegativePrompt` | `string` | Negative prompt used to generate the image | `negativePrompt` |
| `MetadataJson` | `string` | Raw metadata in JSON format | `metadataJson` |
| `CreatedDate` | `DateTime` | Date the metadata was created | `createdDate` |
| `ModifiedDate` | `DateTime` | Date the metadata was last modified | `modifiedDate` |

#### ImageWithMetadata

**Namespace**: `BerryAIGen.Data`

**Purpose**: Combines an Image with its associated ImageMetadata.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `Image` | `Image` | The image entity |
| `Metadata` | `ImageMetadata` | The associated metadata |

## Database Schema

### 1. Tables

#### Images Table

```sql
CREATE TABLE IF NOT EXISTS Images (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    filePath TEXT NOT NULL UNIQUE,
    fileName TEXT NOT NULL,
    folderPath TEXT NOT NULL,
    model TEXT,
    modelHash TEXT,
    seed INTEGER,
    steps INTEGER,
    cfgScale REAL,
    sampler TEXT,
    width INTEGER NOT NULL,
    height INTEGER NOT NULL,
    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    modifiedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    rating INTEGER DEFAULT 0,
    isFavorite BOOLEAN DEFAULT 0,
    isNSFW BOOLEAN DEFAULT 0,
    thumbnailPath TEXT
);
```

#### ImageMetadata Table

```sql
CREATE TABLE IF NOT EXISTS ImageMetadata (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    imageId INTEGER NOT NULL,
    prompt TEXT,
    negativePrompt TEXT,
    metadataJson TEXT,
    createdDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    modifiedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (imageId) REFERENCES Images(id) ON DELETE CASCADE
);
```

### 2. Indexes

```sql
-- Images table indexes
CREATE INDEX IF NOT EXISTS IX_Images_ModelHash ON Images(modelHash);
CREATE INDEX IF NOT EXISTS IX_Images_Seed ON Images(seed);
CREATE INDEX IF NOT EXISTS IX_Images_CreatedDate ON Images(createdDate);
CREATE INDEX IF NOT EXISTS IX_Images_Rating ON Images(rating);
CREATE INDEX IF NOT EXISTS IX_Images_IsFavorite ON Images(isFavorite);
CREATE INDEX IF NOT EXISTS IX_Images_IsNSFW ON Images(isNSFW);
CREATE INDEX IF NOT EXISTS IX_Images_Model ON Images(model);

-- ImageMetadata table indexes
CREATE INDEX IF NOT EXISTS IX_ImageMetadata_ImageId ON ImageMetadata(imageId);
```

## Database Migrations

### 1. Migration System

The application uses a simple migration system to manage database schema changes:

**Migration Class**:

```csharp
public class Migration
{
    public int Version { get; set; }
    public string Name { get; set; }
    public string[] SqlStatements { get; set; }
}
```

**Migration Methods**:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetCurrentVersionAsync` | Gets the current database version | - | `Task<int>` |
| `GetPendingMigrationsAsync` | Gets pending migrations | `int currentVersion` | `Task<List<Migration>>` |
| `ApplyMigrationAsync` | Applies a single migration | `Migration migration` | `Task<bool>` |
| `MigrateDatabaseAsync` | Applies all pending migrations | - | `Task<bool>` |

### 2. Example Migration

```csharp
var migration = new Migration
{
    Version = 2,
    Name = "AddIsNSFWColumn",
    SqlStatements = new[]
    {
        "ALTER TABLE Images ADD COLUMN isNSFW BOOLEAN DEFAULT 0;",
        "CREATE INDEX IX_Images_IsNSFW ON Images(isNSFW);"
    }
};

await _dataStore.ApplyMigrationAsync(migration);
```

## Query Result

### 1. QueryResult Class

**Namespace**: `BerryAIGen.Database`

**Purpose**: Represents the result of parsing a search query.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| `WhereClause` | `string` | The generated WHERE clause |
| `Parameters` | `Dictionary<string, object>` | The parsed parameters |
| `TextSearch` | `string` | The text search portion of the query |
| `OriginalQuery` | `string` | The original search query |

**Example Usage**:

```csharp
var queryResult = _queryBuilder.Parse("cat model:\"test model\"");

Console.WriteLine("WHERE Clause: " + queryResult.WhereClause);
Console.WriteLine("Text Search: " + queryResult.TextSearch);
Console.WriteLine("Parameters: " + string.Join(", ", 
    queryResult.Parameters.Select(p => $"{p.Key}={p.Value}")));
```

## Error Handling

### 1. Database Exceptions

**DatabaseException**:

- **Namespace**: `BerryAIGen.Common.Exceptions`
- **Purpose**: Base exception for database-related errors
- **Properties**: `ErrorCode` (int), `Sql` (string), `Parameters` (object)

**Example Usage**:

```csharp
try
{
    await _dataStore.AddImageAsync(new Image { FilePath = "duplicate/path.jpg" });
}
catch (DatabaseException ex)
{
    Log.Error(ex, "Database error: {Message}", ex.Message);
    Log.Debug("SQL: {Sql}, Parameters: {Parameters}", ex.Sql, ex.Parameters);
}
catch (Exception ex)
{
    Log.Error(ex, "Unexpected error: {Message}", ex.Message);
}
```

## Transactions

### 1. Transaction Support

The DataStore supports transactions for atomic operations:

| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `BeginTransactionAsync` | Starts a new transaction | `IsolationLevel isolationLevel = IsolationLevel.ReadCommitted` | `Task<IDbTransaction>` |
| `CommitTransactionAsync` | Commits a transaction | `IDbTransaction transaction` | `Task<bool>` |
| `RollbackTransactionAsync` | Rolls back a transaction | `IDbTransaction transaction` | `Task<bool>` |

**Example Usage**:

```csharp
// Start a transaction
var transaction = await _dataStore.BeginTransactionAsync();

try
{
    // Perform multiple operations
    await _dataStore.AddImageAsync(image1, transaction);
    await _dataStore.AddImageAsync(image2, transaction);
    await _dataStore.UpdateImageAsync(image3, transaction);
    
    // Commit the transaction
    await _dataStore.CommitTransactionAsync(transaction);
}
catch (Exception ex)
{
    // Rollback on error
    await _dataStore.RollbackTransactionAsync(transaction);
    Log.Error(ex, "Transaction failed: {Message}", ex.Message);
    throw;
}
```

## Performance Optimization

### 1. Query Optimization

- **Parameterized Queries**: All queries use parameters to prevent SQL injection and improve performance
- **Index Usage**: Queries are designed to leverage database indexes
- **Selective Columns**: Only select necessary columns when possible
- **Pagination**: Use `LIMIT` and `OFFSET` for large result sets

### 2. Bulk Operations

- **BulkInsertImagesAsync**: Inserts multiple images in a single transaction
- **Batch Updates**: Update multiple records in a single query when possible
- **Connection Pooling**: The underlying SQLite connection uses connection pooling

### 3. Caching

- **Query Result Caching**: Frequently executed queries have their results cached
- **Image Metadata Caching**: Recently accessed metadata is cached in memory
- **Connection Caching**: Database connections are reused when possible

## Usage Best Practices

### 1. Connection Management

- Use the same DataStore instance for multiple operations to reuse connections
- Dispose of DataStore instances when no longer needed
- Use transactions for multiple related operations

### 2. Query Design

- Use the QueryBuilder for user-generated queries
- Use parameterized queries to prevent SQL injection
- Limit the number of returned rows with `LIMIT` and `OFFSET`
- Select only necessary columns

### 3. Error Handling

- Catch specific database exceptions
- Log SQL statements and parameters for debugging
- Provide meaningful error messages to users

## Conclusion

The database API provides a robust and efficient way to interact with the application's SQLite database. By using the DataStore and QueryBuilder classes, developers can easily perform complex database operations while maintaining performance and security. The API is designed to be extensible, allowing for future enhancements to the database schema and functionality.
