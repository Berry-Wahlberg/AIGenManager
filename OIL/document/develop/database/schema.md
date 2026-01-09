# Database Schema

## Overview
This document describes the database schema for the BerryAIGen.Toolkit application. The database uses SQLite as its backend and consists of several tables that store information about images, folders, tags, albums, and other application data.

## Database Tables

### 1. Images Table

The `Images` table stores metadata and properties for all images in the application.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `RootFolderId` | `INTEGER` | Foreign key to the Folders table (root folder) |
| `FolderId` | `INTEGER` | Foreign key to the Folders table (direct parent) |
| `Path` | `TEXT` | Full file path |
| `FileName` | `TEXT` | File name without path |
| `ModelHash` | `TEXT` | Model hash |
| `Model` | `TEXT` | Model name |
| `Seed` | `INTEGER` | Generation seed |
| `Sampler` | `TEXT` | Sampling method |
| `Height` | `INTEGER` | Image height in pixels |
| `Width` | `INTEGER` | Image width in pixels |
| `CFGScale` | `REAL` | CFG scale value |
| `Steps` | `INTEGER` | Number of generation steps |
| `AestheticScore` | `REAL` | Aesthetic score |
| `Favorite` | `INTEGER` | Whether the image is a favorite (0/1) |
| `Rating` | `INTEGER` | User rating (1-10) |
| `ForDeletion` | `INTEGER` | Whether the image is marked for deletion (0/1) |
| `NSFW` | `INTEGER` | Whether the image is NSFW (0/1) |
| `Unavailable` | `INTEGER` | Whether the file is unavailable (0/1) |
| `CreatedDate` | `TEXT` | Image creation date |
| `ModifiedDate` | `TEXT` | File modification date |
| `HyperNetwork` | `TEXT` | Hypernetwork name |
| `HyperNetworkStrength` | `REAL` | Hypernetwork strength |
| `FileSize` | `INTEGER` | File size in bytes |
| `Prompt` | `TEXT` | Generation prompt |
| `NegativePrompt` | `TEXT` | Negative prompt |
| `Workflow` | `TEXT` | Workflow information |
| `WorkflowId` | `TEXT` | Workflow ID |
| `HasError` | `INTEGER` | Whether there was an error processing the file (0/1) |
| `Hash` | `TEXT` | File hash |
| `ViewedDate` | `TEXT` | Last viewed date |
| `TouchedDate` | `TEXT` | Last touched date |
| `Type` | `TEXT` | File type (image/video) |

**Indexes**:
- `Image_RootFolderId`
- `Image_FolderId`
- `Image_Path` (unique)
- `Image_FileName`
- `Image_ModelHash`
- `Image_Model`
- `Image_Seed`
- `Image_Sampler`
- `Image_Height`
- `Image_Width`
- `Image_CFGScale`
- `Image_Steps`
- `Image_AestheticScore`
- `Image_Favorite`
- `Image_Rating`
- `Image_ForDeletion`
- `Image_NSFW`
- `Image_Unavailable`
- `Image_CreatedDate`
- `Image_HyperNetwork`
- `Image_HyperNetworkStrength`
- `Image_FileSize`
- `Image_ModifiedDate`
- `Image_Prompt`
- `Image_NegativePrompt`
- `Image_Workflow`
- `Image_WorkflowId`
- `Image_HasError`
- `Image_Hash`
- `Image_ViewedDate`
- `Image_TouchedDate`
- Composite indexes for common filter combinations

### 2. Folders Table

The `Folders` table stores information about folders in the application.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `ParentId` | `INTEGER` | Foreign key to the Folders table (parent folder) |
| `Path` | `TEXT` | Full folder path |
| `Name` | `TEXT` | Folder name without path |
| `Archived` | `INTEGER` | Whether the folder is archived (0/1) |
| `Unavailable` | `INTEGER` | Whether the folder is unavailable (0/1) |
| `Excluded` | `INTEGER` | Whether the folder is excluded from scanning (0/1) |

**Indexes**:
- `Folder_ParentId`
- `Folder_Path` (unique)
- `Folder_Archived`
- `Folder_Unavailable`
- `Folder_Excluded`

### 3. Tags Table

The `Tags` table stores user-defined tags for images.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `Name` | `TEXT` | Tag name |
| `Count` | `INTEGER` | Number of images with this tag |

**Indexes**:
- `Tag_Id`
- `Tag_Name` (unique)

### 4. ImageTags Table

The `ImageTags` table is a junction table that establishes many-to-many relationships between images and tags.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `ImageId` | `INTEGER` | Foreign key to the Images table |
| `TagId` | `INTEGER` | Foreign key to the Tags table |

**Indexes**:
- Composite index on `ImageId` and `TagId` (unique)

### 5. Albums Table

The `Albums` table stores information about user-created albums.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `Name` | `TEXT` | Album name |
| `LastUpdated` | `TEXT` | Last updated timestamp |

**Indexes**:
- `Album_Name` (unique)
- `Album_LastUpdated`

### 6. AlbumImages Table

The `AlbumImages` table is a junction table that establishes many-to-many relationships between images and albums.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `AlbumId` | `INTEGER` | Foreign key to the Albums table |
| `ImageId` | `INTEGER` | Foreign key to the Images table |

**Indexes**:
- `AlbumImage_AlbumId`
- `AlbumImage_ImageId`
- Composite index on `AlbumId` and `ImageId` (unique)

### 7. Nodes Table

The `Nodes` table stores information about ComfyUI nodes from workflows.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `ImageId` | `INTEGER` | Foreign key to the Images table |
| `NodeId` | `TEXT` | Node identifier |
| `Name` | `TEXT` | Node name |

**Indexes**:
- `Node_ImageId`
- `Node_Name`
- Composite index on `ImageId` and `NodeId` (unique)

### 8. NodeProperties Table

The `NodeProperties` table stores properties of ComfyUI nodes.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `NodeId` | `INTEGER` | Foreign key to the Nodes table |
| `Name` | `TEXT` | Property name |
| `Value` | `TEXT` | Property value |

**Indexes**:
- `NodeProperty_NodeId`
- `NodeProperty_Name`
- `NodeProperty_Value`
- Composite index on `Name` and `Value` |

### 9. Queries Table

The `Queries` table stores saved search queries.

| Column Name | Data Type | Description |
|-------------|-----------|-------------|
| `Id` | `INTEGER` | Primary key, auto-incrementing |
| `Name` | `TEXT` | Query name |
| `QueryText` | `TEXT` | Query text |

**Indexes**:
- `Query_Name` (unique)

## Table Relationships

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

### Key Relationships

1. **Images and Folders**
   - Each image has a `RootFolderId` and `FolderId` pointing to its location in the folder hierarchy
   - Folders have a hierarchical structure using the `ParentId` foreign key

2. **Images and Tags**
   - Many-to-many relationship via the `ImageTags` junction table
   - Allows images to have multiple tags and tags to be associated with multiple images

3. **Images and Albums**
   - Many-to-many relationship via the `AlbumImages` junction table
   - Allows images to be in multiple albums and albums to contain multiple images

4. **Images and Nodes**
   - One-to-many relationship (one image can have multiple nodes)
   - Nodes represent ComfyUI workflow nodes associated with an image

5. **Nodes and NodeProperties**
   - One-to-many relationship (one node can have multiple properties)
   - NodeProperties store the properties of ComfyUI nodes

## Data Types

The database uses SQLite's dynamic typing system, but the schema follows these general data type conventions:

- **INTEGER**: Used for numeric values, booleans (0/1), and IDs
- **TEXT**: Used for strings, dates, and other text-based data
- **REAL**: Used for floating-point values

## Normalization

The database follows a normalized design with the following characteristics:

1. **First Normal Form (1NF)**: All columns contain atomic values, and there are no repeating groups
2. **Second Normal Form (2NF)**: All non-key columns are fully dependent on the primary key
3. **Third Normal Form (3NF)**: All non-key columns are not transitively dependent on other non-key columns

## Performance Considerations

### Indexing Strategy
- Comprehensive indexing on all frequently queried columns
- Unique indexes on columns that should be unique (Path, Name for tags/albums/queries)
- Composite indexes for common query patterns
- Regular index rebuilds for optimal performance

### Query Optimization
- Use of read-only connections for query operations
- Parameterized queries to avoid SQL injection and improve performance
- Efficient query planning using SQLite's query optimizer
- Proper use of indexes to avoid full table scans

### Data Access Patterns
- Batch operations for bulk updates
- Efficient loading of related data
- Lazy loading of large text fields when appropriate

## Schema Evolution

The database schema is designed to be extensible and supports evolution through migrations. Migrations are handled automatically by the application and are designed to be safe and backward-compatible.

### Migration Strategy
- Versioned migrations with unique identifiers
- Pre and post table creation migrations
- Automatic execution during database initialization
- Safe migration scripts that avoid data loss
- Comprehensive logging of migration operations

## Conclusion

The database schema for BerryAIGen.Toolkit is designed to efficiently store and retrieve metadata for AI-generated images. It follows a normalized design with comprehensive indexing to ensure fast query performance. The schema supports a wide range of metadata from various AI image generators and provides flexibility for future enhancements. The use of migrations allows the schema to evolve over time while maintaining compatibility with existing data.

