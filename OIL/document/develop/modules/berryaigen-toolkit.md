# BerryAIGen.Toolkit Module

## Overview
The BerryAIGen.Toolkit module is the main application module that provides the WPF user interface and core functionality for the BerryAIGen.Toolkit application. It includes all the UI components, view models, services, and other features that users interact with directly.

## Core Submodules

### 1. Behaviors

The Behaviors submodule contains attached behaviors for UI elements that extend their functionality without subclassing:

#### Key Behaviors
- **DTBehaviors.IsDraggedOn**: Detects when an element is being dragged over
- **DTBehaviors.IsFocused**: Tracks focus state of elements
- **DTBehaviors.IsMouseOver**: Tracks mouse over state
- **DTBehaviors.IsScrollDisabled**: Disables scrolling in elements
- **DTBehaviors.IsSelected**: Tracks selection state
- **DTBehaviors.SelectOnFocus**: Automatically selects text when an element gains focus
- **DragSort**: Enables drag-and-drop sorting for lists
- **ScrollSpeed**: Controls scroll speed for elements

### 2. Common

The Common submodule provides shared utilities and interfaces used throughout the application:

#### Key Components
- **AsyncCommand**: Implementation of ICommand for asynchronous operations
- **BareKeyBinding**: Custom key binding implementation
- **BareKeyGesture**: Custom key gesture implementation
- **BindingProxy**: Proxy for data binding in XAML
- **IAsyncCommand**: Interface for asynchronous commands
- **INavigatorService**: Interface for navigation services
- **IconHelper**: Helper for working with icons
- **NavigatorService**: Navigation service implementation
- **RelayCommand**: Basic ICommand implementation for synchronous operations
- **TaskUtilities**: Utilities for working with tasks
- **ViewMode**: Enumeration for view modes
- **VisibilityAnimation**: Animation for visibility changes

### 3. Configuration

The Configuration submodule handles application settings and configuration:

#### Key Components
- **AccordionSetting**: Setting for accordion UI elements
- **AccordionState**: State management for accordion elements
- **ExternalApplication**: Configuration for external applications
- **IScanOptions**: Interface for scan options
- **MetadataSectionSettings**: Settings for metadata sections
- **NavigationSectionSettings**: Settings for navigation sections
- **Settings**: Main application settings class
- **SettingsContainer**: Container for settings

### 4. Controls

The Controls submodule contains custom WPF controls used in the application:

#### Key Controls
- **AccordionControl**: Expandable accordion control
- **ComfyNode**: Control for displaying ComfyUI nodes
- **FilterControl**: Control for building search filters
- **MetadataPanel**: Panel for displaying image metadata
- **MessagePopup**: Popup message control
- **PreviewPane**: Pane for previewing images
- **StarRating**: Control for rating images
- **ThumbnailList**: List control for displaying image thumbnails
- **ThumbnailView**: Main thumbnail display control

### 5. Converters

The Converters submodule contains value converters for data binding in XAML:

#### Key Converters
- **BoolToVisibilityConverter**: Converts boolean values to visibility
- **InverseBoolToVisibilityConverter**: Converts inverse boolean values to visibility
- **NumberFormatConverter**: Formats numbers for display
- **SizeConverter**: Converts sizes for display
- **StringMatchConverter**: Matches strings for conditional display
- **ThumbnailSizeCheckedConverter**: Converts thumbnail size settings
- **ValueVisibilityConverter**: Converts values to visibility based on conditions

### 6. Localization

The Localization submodule provides multi-language support for the application:

#### Key Components
- **JsonLocalizationProvider**: Provider for JSON-based localization
- **Language files**: JSON files for each supported language (en-US, zh-CN, etc.)
- **update.py**: Script for updating localization files

### 7. Models

The Models submodule contains view models and data models for the application:

#### Key Models
- **BaseNotify**: Base class implementing INotifyPropertyChanged
- **FolderViewModel**: View model for folders
- **ImageEntry**: Entry for images in lists
- **ImageViewModel**: View model for images
- **MainModel**: Main application view model
- **QueryModel**: View model for search queries
- **SearchModel**: View model for search functionality
- **SettingsModel**: View model for application settings

### 8. Pages

The Pages submodule contains the main application pages:

#### Key Pages
- **Search**: Main search and browsing page
- **Models**: Page for managing models
- **Prompts**: Page for viewing and managing prompts
- **Settings**: Page for application settings

### 9. Services

The Services submodule contains core application services:

#### Key Services
- **AlbumService**: Service for managing albums
- **ContextMenuService**: Service for managing context menus
- **DatabaseWriterService**: Service for writing to the database
- **ExternalApplicationsService**: Service for external application integration
- **FileService**: Service for file operations
- **FolderService**: Service for managing folders
- **MessageService**: Service for displaying messages
- **MetadataScannerService**: Service for scanning metadata
- **ProgressService**: Service for reporting progress
- **ScanningService**: Service for scanning folders
- **SearchService**: Service for search functionality
- **TagService**: Service for managing tags
- **TaggingService**: Service for tagging images
- **ThumbnailService**: Service for generating thumbnails

### 10. Themes

The Themes submodule provides theming support for the application:

#### Key Components
- **Common.xaml**: Common theme resources
- **Dark.xaml**: Dark theme definition
- **Light.xaml**: Light theme definition
- **ThemeManager**: Class for managing themes

### 11. Thumbnails

The Thumbnails submodule handles thumbnail generation and caching:

#### Key Components
- **Job**: Base class for thumbnail jobs
- **ThumbnailCache**: Cache for storing thumbnails
- **ThumbnailJob**: Job for generating thumbnails
- **ThumbnailService**: Service for managing thumbnail generation

## Main Application Flow

### Startup
1. **App.xaml.cs** initializes the application
2. **MainWindow.xaml.cs** creates the main window
3. **ServiceLocator** registers core services
4. **DataStore** initializes the database
5. **MainModel** sets up application state
6. Application navigates to the default Search page

### Image Browsing and Search
1. User enters a search query in the search bar
2. **SearchService** processes the query
3. **QueryBuilder** builds the SQL query
4. **DataStore** executes the query
5. **ThumbnailService** generates thumbnails for results
6. Results are displayed in the **ThumbnailView**

### Image Viewing
1. User selects an image from the results
2. **PreviewService** loads the full-size image
3. **MetadataPanel** displays the image metadata
4. User can edit metadata, rate the image, or apply tags
5. Changes are saved to the database via **DataStore**

### Folder Management
1. User adds folders in the Settings page
2. **FolderService** manages the list of watched folders
3. **ScanningService** scans folders for new images
4. **MetadataScannerService** extracts metadata from new images
5. Database is updated with new image records

## Key Features

### 1. Metadata Extraction
- Extracts detailed metadata from various AI image generation platforms
- Supports multiple metadata formats (AUTOMATIC1111, InvokeAI, NovelAI, etc.)
- Extracts prompts, negative prompts, seed values, and other generation parameters

### 2. Search and Filtering
- Advanced search capabilities across all metadata fields
- Support for complex search queries with multiple conditions
- Real-time search results with debouncing
- Filtering by tags, ratings, favorites, and other criteria

### 3. Image Organization
- Ratings system (1-10 stars)
- Favorite tagging
- NSFW detection and filtering
- Custom tags
- Album management with drag-and-drop support

### 4. Thumbnail Generation
- Efficient thumbnail generation for quick preview
- Caching system for improved performance
- Asynchronous generation to keep the UI responsive
- Support for various thumbnail sizes

### 5. Batch Processing
- Batch operations for images (move, copy, delete)
- Batch tagging and rating
- Batch metadata editing

### 6. Multi-language Support
- Support for multiple languages
- Easy addition of new languages
- Language switching at runtime

### 7. Dark and Light Themes
- Support for both dark and light themes
- Automatic theme detection based on system settings
- Manual theme switching

## Performance Optimization

### 1. UI Responsiveness
- Asynchronous operations for long-running tasks
- Debouncing for search queries
- Virtualization for large lists
- Lazy loading of thumbnails

### 2. Memory Management
- Efficient thumbnail caching
- Proper disposal of resources
- Weak references for cached objects when appropriate

### 3. Database Performance
- Indexed database tables for fast queries
- Read-only connections for query operations
- Batch processing for bulk updates

## Conclusion

The BerryAIGen.Toolkit module is the main application module that provides all the UI components, services, and functionality that users interact with. It follows a modular architecture with clear separation of concerns, making it maintainable and extensible. The module provides a comprehensive set of features for managing AI-generated images, including metadata extraction, search and filtering, image organization, and thumbnail generation.

