# Database Migrations

## Overview
This document describes the database migration system used in the BerryAIGen.Toolkit application. Migrations are used to manage database schema changes over time, ensuring that the database schema evolves safely and consistently across different versions of the application.

## Core Implementation

### Location
The migration system implementation is located in `BerryAIGen.Database/Migrations.cs`.

### Key Features

1. **Versioned Migrations**
   - Each migration has a unique identifier
   - Migrations are executed in chronological order
   - Migration history is tracked in the database

2. **Pre and Post Table Creation Migrations**
   - **Pre Migrations**: Executed before tables are created
   - **Post Migrations**: Executed after tables are created
   - Allows for flexible migration strategies

3. **Automatic Execution**
   - Migrations are executed automatically when the database is initialized
   - No manual intervention required
   - Safe to run multiple times

4. **Safe Migration Scripts**
   - Migrations are designed to avoid data loss
   - Uses SQLite's transaction support for atomic operations
   - Comprehensive error handling

5. **Detailed Logging**
   - Migration operations are logged for debugging purposes
   - Logs include migration name, type, and execution time
   - Error logs include detailed information about failed migrations

## Migration Types

### 1. Pre Migrations
- Executed before database tables are created
- Used for initial schema setup or changes that require table recreation
- Examples include: creating initial tables, setting up indexes, or major schema changes

### 2. Post Migrations
- Executed after database tables are created
- Used for data updates, additional indexes, or minor schema changes
- Examples include: updating existing records, adding new columns with default values, or creating new indexes

## Migration Implementation

### Migration Attribute
Migrations are identified by the `[Migration]` attribute, which specifies a unique migration identifier:

```csharp
[Migration("RupertAvery20231224_0001_CleanupOrphanedAlbumImageEntries")]
private void CleanupOrphanedAlbumImageEntries(SQLiteConnection db)
{
    // Migration implementation
}
```

### Migration Identifier Format
Migration identifiers follow a specific format:
- **Author**: Developer name (e.g., "RupertAvery")
- **Date**: YYYYMMDD format (e.g., "20231224")
- **Version**: Sequential number (e.g., "0001")
- **Description**: Short description of the migration (e.g., "CleanupOrphanedAlbumImageEntries")

### Migration Execution Order
Migrations are executed in the following order:
1. All pre migrations in chronological order based on their identifiers
2. Database tables and indexes are created
3. All post migrations in chronological order based on their identifiers

## Migration Examples

### Example 1: Cleanup Orphaned Entries

```csharp
[Migration("RupertAvery20231224_0001_CleanupOrphanedAlbumImageEntries")]
private void CleanupOrphanedAlbumImageEntries(SQLiteConnection db)
{
    // Delete orphaned AlbumImage entries where the ImageId doesn't exist in Images table
    db.Execute("DELETE FROM AlbumImage WHERE ImageId NOT IN (SELECT Id FROM Image)");
}
```

### Example 2: Add Foreign Keys

```csharp
[Migration("RupertAvery20231224_0002_AlbumImageForeignKeys")]
private void AddAlbumImageForeignKeys(SQLiteConnection db)
{
    // Add foreign key constraints to AlbumImage table
    db.Execute("ALTER TABLE AlbumImage ADD FOREIGN KEY(AlbumId) REFERENCES Album(Id)");
    db.Execute("ALTER TABLE AlbumImage ADD FOREIGN KEY(ImageId) REFERENCES Image(Id)");
}
```

### Example 3: Load File Names From Paths

```csharp
[Migration("RupertAvery20240102_0001_LoadFileNamesFromPaths")]
private void LoadFileNamesFromPaths(SQLiteConnection db)
{
    // Update FileName column from Path for existing records
    db.Execute("UPDATE Image SET FileName = SUBSTR(Path, INSTR(Path, '\\') + 1) WHERE FileName IS NULL OR FileName = ''");
}
```

### Example 4: Set Image Type

```csharp
[Migration("RupertAvery20260103_0001_SetImageType")]
private void SetImageType(SQLiteConnection db)
{
    // Set default ImageType for existing records
    db.Execute("UPDATE Image SET Type = 'image' WHERE Type IS NULL OR Type = ''");
}
```

## Migration Execution Flow

### 1. Database Initialization
- When the application starts, the `DataStore.Create()` method is called
- This method checks if migrations need to be executed

### 2. Migration Detection
- The migration system scans for all methods with the `[Migration]` attribute
- It groups migrations into pre and post migrations based on their attribute
- It checks which migrations have already been executed

### 3. Migration Execution
- Pre migrations are executed in order
- Tables and indexes are created
- Post migrations are executed in order
- Each migration is executed in a transaction for atomicity
- Migration execution is logged

### 4. Migration Tracking
- A record of executed migrations is maintained
- This prevents migrations from being executed multiple times
- Allows for incremental updates to the database schema

## Adding a New Migration

### Steps to Add a Migration

1. **Create a new migration method** in the appropriate partial class
2. **Add the `[Migration]` attribute** with a unique identifier
3. **Implement the migration logic** using safe SQL practices
4. **Test the migration** thoroughly
5. **Include the migration** in the application build

### Best Practices for Writing Migrations

1. **Use Transactions**: Ensure migrations are atomic
2. **Be Idempotent**: Migrations should be safe to run multiple times
3. **Avoid Data Loss**: Always consider the impact on existing data
4. **Test Thoroughly**: Test migrations with various database states
5. **Use Descriptive Names**: Make migration names clear and informative
6. **Follow the Format**: Use the standard migration identifier format
7. **Log Effectively**: Include appropriate logging in migration scripts

## Migration Safety

### Transaction Support
- Migrations are executed within transactions
- If a migration fails, the entire transaction is rolled back
- Ensures database consistency even if migrations fail

### Error Handling
- Comprehensive error handling for migration failures
- Detailed error messages are logged
- The application continues to run if non-critical migrations fail
- Critical migration failures are reported to the user

### Data Preservation
- Migrations are designed to preserve existing data
- When adding new columns, default values are used if appropriate
- When modifying columns, data type conversions are handled carefully
- Data backups are recommended before running migrations

## Migration History

The migration system maintains a record of all executed migrations:
- Migration history is stored in the database
- Includes migration name, execution time, and status
- Allows for debugging and auditing purposes
- Used to determine which migrations still need to be executed

## Conclusion

The database migration system in BerryAIGen.Toolkit provides a safe and efficient way to manage database schema changes over time. By following best practices for writing migrations and leveraging the system's automatic execution and transaction support, the application can evolve its database schema while maintaining data integrity and consistency. This ensures that users can upgrade to new versions of the application without losing their data or experiencing database-related issues.

