# BerryAIGen.Common Module

## Overview
The BerryAIGen.Common module contains shared utilities, interfaces, and components used across multiple modules in the BerryAIGen.Toolkit application. It provides a foundation of common functionality that other modules can rely on, ensuring consistency and reducing code duplication.

## Core Components

### 1. Query System

The Query namespace provides classes for building and executing queries across the application:

#### Filter
- **Purpose**: Base class for building filter expressions
- **Key Features**: 
  - Supports complex filter expressions
  - Can be combined with other filters
  - Used across multiple modules for consistent querying

#### NodeFilter
- **Purpose**: Specialized filter for ComfyUI nodes
- **Key Features**: 
  - Filters based on node properties
  - Supports comparison operations
  - Used in ComfyUI workflow analysis

#### QueryOptions
- **Purpose**: Base class for query options
- **Key Features**: 
  - Pagination support
  - Sorting options
  - Filtering capabilities

#### ComfyQueryOptions
- **Purpose**: Query options for ComfyUI-specific queries
- **Key Features**: 
  - Extends QueryOptions
  - Adds ComfyUI-specific filtering
  - Supports node-level filtering

### 2. Application Information

#### AppInfo
- **Purpose**: Provides application-wide information
- **Key Properties**: 
  - `Version`: Application version
  - `SettingsPath`: Path to application settings
  - `DatabasePath`: Path to database file
  - `IsPortable`: Whether the application is running in portable mode

### 3. Configuration Management

#### Configuration<T>
- **Purpose**: Generic configuration management class
- **Key Features**: 
  - Loads and saves configuration from JSON files
  - Supports portable mode
  - Automatic file creation if missing
  - Change detection

### 4. File System Utilities

#### FileUtility
- **Purpose**: Provides file system-related utilities
- **Key Methods**: 
  - `GetFileSize`: Gets human-readable file size
  - `GetFileExtension`: Gets file extension
  - `EnsureDirectory`: Ensures a directory exists

### 5. Hashing

#### Hashing
- **Purpose**: Provides hash generation utilities
- **Key Methods**: 
  - `ComputeSHA256`: Computes SHA256 hash for a file
  - `ComputeMD5`: Computes MD5 hash for a file
  - `ComputeHash`: Computes hash using specified algorithm

### 6. Logging

#### Logger
- **Purpose**: Simple logging implementation
- **Key Methods**: 
  - `Log`: Logs a message
  - `LogError`: Logs an error message
  - `LogDebug`: Logs a debug message

### 7. Model Representation

#### Model
- **Purpose**: Common model representation
- **Key Properties**: 
  - `Path`: Model file path
  - `Filename`: Model filename
  - `SHA256`: Model SHA256 hash
  - `IsLocal`: Whether the model is local or remote

#### ModelInfo
- **Purpose**: Model information structure
- **Key Properties**: 
  - `Name`: Model name
  - `Version`: Model version
  - `Description`: Model description

### 8. Versioning

#### SemanticVersion
- **Purpose**: Semantic version parsing and comparison
- **Key Features**: 
  - Parses semantic version strings
  - Supports version comparison
  - Handles prerelease versions

#### SemanticVersionHelper
- **Purpose**: Helper methods for SemanticVersion
- **Key Methods**: 
  - `Parse`: Parses a version string
  - `Compare`: Compares two versions
  - `ToString`: Formats a version as string

### 9. Update Checking

#### UpdateChecker
- **Purpose**: Checks for application updates
- **Key Methods**: 
  - `CheckForUpdate`: Checks if a new version is available
  - `GetLatestVersion`: Gets the latest version information

### 10. General Utilities

#### Utility
- **Purpose**: General utility methods
- **Key Methods**: 
  - `FormatBytes`: Formats bytes as human-readable string
  - `TruncateString`: Truncates a string to specified length
  - `EscapeString`: Escapes special characters in a string

#### Win32FileAPI
- **Purpose**: Win32 file API wrappers
- **Key Methods**: 
  - `GetFileAttributes`: Gets file attributes using Win32 API
  - `SetFileAttributes`: Sets file attributes using Win32 API

## Usage Examples

### Using Configuration<T>

```csharp
// Create a configuration instance
var config = new Configuration<Settings>(AppInfo.SettingsPath, AppInfo.IsPortable);

// Load configuration
config.Load(out var settings);

// Save configuration
settings.SomeProperty = "new value";
config.Save(settings);
```

### Using Hashing

```csharp
// Compute SHA256 hash for a file
var hash = Hashing.ComputeSHA256(filePath);

// Compute MD5 hash for a file
var md5Hash = Hashing.ComputeMD5(filePath);
```

### Using SemanticVersion

```csharp
// Parse a version string
if (SemanticVersion.TryParse(versionString, out var version))
{
    // Compare versions
    if (version < AppInfo.Version)
    {
        // Do something
    }
}
```

## Module Dependencies

The BerryAIGen.Common module has no dependencies on other modules in the solution. It is designed to be a standalone library that can be used by any other module.

## Usage in Other Modules

Almost every module in the solution depends on BerryAIGen.Common for shared functionality:

- **BerryAIGen.Database**: Uses Query classes for database queries
- **BerryAIGen.IO**: Uses FileUtility, Hashing, and Logger
- **BerryAIGen.Toolkit**: Uses Configuration, AppInfo, and various utilities
- **BerryAIGen.Civitai**: Uses Logger and Hashing
- **BerryAIGen.Github**: Uses Logger and UpdateChecker

## Design Principles

1. **Reusability**: Components are designed to be reused across multiple modules
2. **Consistency**: Provides consistent interfaces for common operations
3. **Simplicity**: Simple, easy-to-use APIs
4. **Dependency-free**: No dependencies on other modules in the solution
5. **Cross-platform**: Works on all supported platforms

## Conclusion

The BerryAIGen.Common module is a foundational component of the BerryAIGen.Toolkit application. It provides a wide range of utilities and interfaces that are used by almost every other module in the solution, ensuring consistency and reducing code duplication. Its simple and reusable design makes it easy for developers to work with and extend as needed.

