# Initial Startup Checks

## Overview

This document describes the automatic checks and procedures performed during the first startup of the BerryAIGen.Toolkit application. These checks ensure the application is properly configured and ready for use, providing a smooth onboarding experience for new users.

## Trigger Conditions

The initial startup checks are triggered when the application detects that no configuration file exists. This is determined by checking for the presence of the `config.json` file in the application directory (or appropriate user-specific location for non-portable mode).

```csharp
// Simplified logic from MainWindow.xaml.cs
if (!_configuration.Exists())
{
    // Execute first-time startup checks and procedures
    isFirstTime = true;
}
```

## Execution Content

The following checks and procedures are executed during the initial startup:

### 1. Configuration Initialization

- **Check**: Verify if configuration file exists
- **Procedure**: 
  - Create default configuration settings
  - Initialize application directories and paths
  - Set default values for all configuration options
- **Files Created**: `config.json` in the application directory

### 2. Language Selection

- **Check**: No language preference has been set
- **Procedure**: 
  - Display `LanguageSelectionWindow` to the user
  - Allow user to select preferred language from available options
  - Apply selected language settings
- **Options**: Supports multiple languages (de-DE, es-ES, fr-FR, ja-JP, uk-UA, zh-CN, zh-TW)

### 3. Welcome Wizard

- **Check**: First-time application usage
- **Procedure**: 
  - Display `WelcomeWindow` with application introduction
  - Allow user to select initial folders for image scanning
  - Configure basic application preferences
- **Key Options**: 
  - Select root folders for image library
  - Enable/disable automatic scanning
  - Set initial thumbnail size

### 4. Database Initialization

- **Check**: No database file exists at the configured location
- **Procedure**: 
  - Create SQLite database file
  - Initialize database schema with all required tables
  - Set up initial database indexes
  - Configure database connection settings
- **File Created**: Database file (default: `aigen-manager.db` in application directory)

### 5. Thumbnail Cache Initialization

- **Check**: No thumbnail cache exists
- **Procedure**: 
  - Create `ThumbnailCache` instance with default settings
  - Configure cache size based on page size settings
  - Initialize background thumbnail generation service

### 6. Update Check (Conditional)

- **Check**: If `CheckForUpdatesOnStartup` setting is enabled (default: true)
- **Procedure**: 
  - Execute update check against GitHub API
  - Compare local version with latest release
  - Notify user if update is available
- **Technical Details**: Uses `UpdateChecker` class with 3-second timeout

### 7. Initial Folder Scanning

- **Check**: User has selected folders in the Welcome Wizard
- **Procedure**: 
  - Add selected folders to watched folders list
  - Start background scanning process for images
  - Extract metadata from discovered images
  - Populate database with image information
  - Generate thumbnails for all detected images
- **Post-Scan**: Automatically navigate to search page to display results

## Result Handling Mechanisms

### Success State

- **Behavior**: Application completes all checks and procedures successfully
- **User Feedback**: 
  - Progress indicators during scanning process
  - Confirmation messages for successful operations
  - Automatic navigation to the main interface
- **Log**: Detailed success messages in application logs

### Warning State

- **Conditions**: 
  - Optional components fail to initialize
  - Partial success in folder scanning
  - Minor configuration issues
- **User Feedback**: 
  - Non-blocking warning messages
  - Suggestions for resolving issues
  - Continued application operation
- **Log**: Warning messages with suggested resolutions

### Error State

- **Conditions**: 
  - Critical component initialization failure
  - Database creation error
  - Configuration file write failure
- **User Feedback**: 
  - Modal error dialog with detailed message
  - Application exit after user acknowledgment
- **Log**: Comprehensive error details including stack traces

## Distinctions Between Initial and Regular Checks

| Aspect | Initial Startup Checks | Regular Automatic Checks |
|--------|------------------------|--------------------------|
| **Trigger** | Missing configuration file | Application startup with existing configuration |
| **Scope** | Comprehensive initialization | Focused on specific checks |
| **User Interaction** | Extensive (language selection, welcome wizard) | Minimal or none |
| **Files Created** | Configuration, database, cache | None (updates existing files) |
| **Duration** | Longer (includes scanning and setup) | Shorter (focused checks) |
| **Frequency** | Once per installation | Every startup |
| **Focus** | Configuration and setup | Validation and updates |
| **UI Flow** | Guided onboarding | Direct to main interface |

## Regular Automatic Checks

During normal system operation, the following automatic checks are performed on every startup:

1. **Configuration Validation**: Verify configuration file integrity
2. **Update Check**: Check for new releases (if enabled)
3. **Database Connection**: Verify database accessibility
4. **Service Initialization**: Ensure all required services are available
5. **Settings Migration**: Update settings format for version compatibility

## Technical Implementation

### Key Classes and Components

| Component | Purpose | Location |
|-----------|---------|----------|
| `MainWindow` | Orchestrates startup process | `BerryAIGen.Toolkit` |
| `UpdateChecker` | Handles update checking | `BerryAIGen.Common` |
| `Configuration` | Manages configuration files | `BerryAIGen.Toolkit` |
| `LanguageSelectionWindow` | Language selection UI | `BerryAIGen.Toolkit` |
| `WelcomeWindow` | Welcome wizard UI | `BerryAIGen.Toolkit` |
| `DataStore` | Database initialization | `BerryAIGen.Database` |
| `ThumbnailCache` | Thumbnail cache management | `BerryAIGen.Toolkit` |

### Flow Diagram

```
+-------------------+
| Application Start |
+-------------------+
          |
          v
+-------------------+
| Check Config File |
+-------------------+
          |
          v
+-------------------+
|                   |
|  +-------------+  |
|  | Config Exists?|--No--> Initial Startup Checks
|  +-------------+  |
|         | Yes     |
|         v         |
|  +-------------+  |
|  | Regular Checks |
|  +-------------+  |
|                   |
+-------------------+
          |
          v
+-------------------+
| Main Interface    |
+-------------------+
```

## Configuration Options

| Setting | Default | Description |
|---------|---------|-------------|
| `CheckForUpdatesOnStartup` | `true` | Enable/disable automatic update checks on startup |
| `PortableMode` | `true` | Store configuration and database in application directory |
| `Theme` | `System` | UI theme preference |
| `Language` | System default | Application language |

## Troubleshooting

### Common Initial Startup Issues

1. **Configuration Write Error**
   - **Cause**: Insufficient permissions to write to application directory
   - **Solution**: Run application with administrative privileges or move to a user-writable location

2. **Database Creation Error**
   - **Cause**: SQLite library issues or disk space constraints
   - **Solution**: Ensure adequate disk space and verify SQLite dependencies

3. **Folder Scanning Failure**
   - **Cause**: Invalid folder paths or permission issues
   - **Solution**: Select valid folders with read permissions during setup

4. **Update Check Timeout**
   - **Cause**: Network connectivity issues
   - **Solution**: Check network connection or disable update checks in settings

## Future Enhancements

- Add more comprehensive system requirements checks
- Implement progress tracking for all startup procedures
- Add detailed diagnostic reporting for startup failures
- Support for automated recovery from common startup issues
- Enhanced logging for troubleshooting purposes

## Conclusion

The initial startup checks ensure that the BerryAIGen.Toolkit application is properly configured and ready for use, providing a smooth onboarding experience for new users. By understanding these checks and their behavior, developers can effectively troubleshoot startup issues and maintain the application's reliability.