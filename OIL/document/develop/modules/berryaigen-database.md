# BerryAIGen.Database Module

## Overview
The BerryAIGen.Database module provides the database implementation for the BerryAIGen.Toolkit application. It uses SQLite as its database backend and includes schema definition, migrations, and data access functionality. This module is responsible for storing and retrieving all application data, including images, folders, tags, albums, and metadata.

## Core Components

### 1. DataStore

The `DataStore` class is the main entry point for database operations. It manages database connections, table creation, and provides methods for data access.

#### Key Features
- **Database Connection Management**: Handles opening and closing database connections
- **Read-Only Connections**: Supports read-only connections for query operations
- **Database Creation**: Creates database tables and indexes
- **Migration Support**: Handles database schema migrations
- **Backup and Restore**: Provides functionality for database backup and restore

#### Key Methods
- `OpenConnection()`: Opens a new database connection
- `OpenReadonlyConnection()`: Opens a read-only database connection
- `Create()`: Creates database tables and indexes
- `RebuildIndexes()`: Rebuilds database indexes for performance
- `CreateBackup()`: Creates a backup of the database
- `TryRestoreBackup()`: Attempts to restore a database from a backup

### 2. Database Models

The module defines several database models that represent tables in the database:

#### Image
- **Purpose**: Represents an image file in the database
- **Key Properties**: 
  - `Id`: Unique identifier
  - `Path`: File path
  - `FileName`: File name
  - `ModelHash`: Model hash
  - `Model`: Model name
  - `Seed`: Generation seed
  - `Sampler`: Sampling method
  - `Height`: Image height
  - `Width`: Image width
  - `CFGScale`: CFG scale value
  - `Steps`: Number of steps
  - `AestheticScore`: Aesthetic score
  - `Favorite`: Whether the image is a favorite
  - `Rating`: User rating
  - `ForDeletion`: Whether the image is marked for deletion
  - `NSFW`: Whether the image is NSFW
  - `Unavailable`: Whether the image file is unavailable
  - `Prompt`: Generation prompt
  - `NegativePrompt`: Negative prompt
  - `Workflow`: Workflow information

#### Folder
- **Purpose**: Represents a folder in the database
- **Key Properties**: 
  - `Id`: Unique identifier
  - `ParentId`: Parent folder ID
  - `Path`: Folder path
  - `Name`: Folder name
  - `Archived`: Whether the folder is archived
  - `Unavailable`: Whether the folder is unavailable
  - `Excluded`: Whether the folder is excluded from scanning

#### Tag
- **Purpose**: Represents a user-defined tag
- **Key Properties**: 
  - `Id`: Unique identifier
  - `Name`: Tag name
  - `Count`: Number of images with this tag

#### ImageTag
- **Purpose**: Represents the many-to-many relationship between images and tags
- **Key Properties**: 
  - `ImageId`: Image ID
  - `TagId`: Tag ID

#### Album
- **Purpose**: Represents a user-created album
- **Key Properties**: 
  - `Id`: Unique identifier
  - `Name`: Album name
  - `LastUpdated`: Last updated timestamp

#### AlbumImage
- **Purpose**: Represents the many-to-many relationship between images and albums
- **Key Properties**: 
  - `AlbumId`: Album ID
  - `ImageId`: Image ID

#### Node
- **Purpose**: Represents a ComfyUI node in a workflow
- **Key Properties**: 
  - `Id`: Unique identifier
  - `ImageId`: Image ID
  - `NodeId`: Node identifier
  - `Name`: Node name

#### NodeProperty
- **Purpose**: Represents a property of a ComfyUI node
- **Key Properties**: 
  - `Id`: Unique identifier
  - `NodeId`: Node ID
  - `Name`: Property name
  - `Value`: Property value

#### Query
- **Purpose**: Represents a saved search query
- **Key Properties**: 
  - `Id`: Unique identifier
  - `Name`: Query name
  - `QueryText`: Query text

### 3. Migrations

The module includes a migration system to handle database schema changes over time. Migrations are defined in the `Migrations` class and are executed automatically when the database is initialized.

#### Key Features
- **Pre and Post Migrations**: Supports both pre and post table creation migrations
- **Versioned Migrations**: Each migration is versioned for tracking
- **Automatic Execution**: Migrations are executed automatically when needed
- **Safe Execution**: Migrations are executed in a safe manner to prevent data loss

#### Migration Examples
- `RupertAvery20231224_0001_CleanupOrphanedAlbumImageEntries`: Cleans up orphaned album image entries
- `RupertAvery20231224_0002_AlbumImageForeignKeys`: Adds foreign keys to the AlbumImage table
- `RupertAvery20240102_0001_LoadFileNamesFromPaths`: Loads file names from paths
- `RupertAvery20240203_0001_UniquePaths`: Ensures unique paths in the database

### 4. Query Building

The module provides classes for building database queries:

#### QueryBuilder
- **Purpose**: Builds SQL queries for searching images
- **Key Features**: 
  - Supports complex search conditions
  - Handles pagination and sorting
  - Filters out deleted, NSFW, or unavailable images based on settings
  - Supports various search operators

#### ComfyUIQueryBuilder
- **Purpose**: Builds queries for ComfyUI workflows
- **Key Features**: 
  - Specialized for ComfyUI node data
  - Supports node-level filtering
  - Handles complex workflow queries

### 5. Database Extensions

The module includes extensions for SQLite functionality:

#### SQLiteConnectionExtensions
- **Purpose**: Provides extension methods for SQLiteConnection
- **Key Features**: 
  - Adds additional functionality to SQLiteConnection
  - Supports common database operations

## Database Schema

The database schema consists of the following tables:

```
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?   Image    鈹傗攢鈹€鈹€鈹€鈻垛攤  ImageTag   鈹傗梹鈹€鈹€鈹€鈹€鈹?    Tag     鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
     鈹?                                     鈹?
     鈻?                                     鈻?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?   Folder   鈹?    鈹? AlbumImage 鈹?    鈹?   Album    鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
     鈹?                                     鈻?
     鈻?                                     鈹?
鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹屸攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
鈹?   Node     鈹傗攢鈹€鈹€鈹€鈻垛攤 NodeProperty鈹?    鈹?   Query    鈹?
鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?    鈹斺攢鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹€鈹?
```

## Usage Examples

### Creating a DataStore Instance

```csharp
// Create a new DataStore instance
var dataStore = new DataStore("database.db");

// Initialize the database
await dataStore.Create(
    () => Console.WriteLine("Updating database..."),
    (handle) => Console.WriteLine("Database update complete")
);
```

### Opening a Database Connection

```csharp
// Open a read-write connection
using var db = dataStore.OpenConnection();

// Open a read-only connection for queries
using var readonlyDb = dataStore.OpenReadonlyConnection();
```

### Creating a Backup

```csharp
// Create a backup of the database
dataStore.CreateBackup();
```

## Performance Considerations

### Indexing
The module creates comprehensive indexes for frequently queried columns to improve query performance:
- Indexes on `Image.Path` for fast path lookups
- Indexes on `Image.ModelHash`, `Image.Seed`, `Image.Sampler` for metadata queries
- Indexes on `Image.Favorite`, `Image.NSFW`, `Image.ForDeletion` for filtering
- Indexes on join columns like `ImageTag.ImageId` and `ImageTag.TagId`

### Read-Only Connections
Read-only connections are used for query operations to improve performance and reduce contention:
- Read-only connections use shared cache
- They don't block write operations
- They're optimized for query performance

### Batch Operations
The module supports batch operations for bulk updates:
- Batch inserts for new images
- Batch updates for metadata changes
- Efficient deletion of multiple records

## Module Dependencies

The BerryAIGen.Database module depends on:
- **BerryAIGen.Common**: For shared utilities and interfaces
- **SQLite-net-pcl**: For SQLite database access

## Conclusion

The BerryAIGen.Database module provides a robust and efficient database implementation for the BerryAIGen.Toolkit application. It handles all aspects of database management, from table creation and migrations to data access and query building. With its comprehensive indexing and performance optimizations, it ensures fast and reliable access to application data even for large collections of images.

