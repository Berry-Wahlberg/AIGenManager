# SearchService Component

## Overview

The SearchService is a core component of the BerryAIGen.Toolkit application responsible for managing search functionality, coordinating search events, and providing a central point for search-related operations. It acts as a bridge between the UI components and the underlying search logic, facilitating communication and event propagation.

## Core Implementation

### Location
The SearchService implementation is located in `BerryAIGen.Toolkit/Services/SearchService.cs`.

### Class Structure

```csharp
public class SearchService
{
    // Event declarations
    public event EventHandler<string> SortBy;
    public event EventHandler<string> SortOrder;
    public event EventHandler<SearchFilter> SearchFilter;
    public event EventHandler<FolderViewModel> OpenFolder;
    public event EventHandler<string> OpenPath;
    public event EventHandler Search;
    public event EventHandler Refresh;
    public event EventHandler<SearchView> View;
    
    // Constructor
    public SearchService(FilterControlModel filter, SearchSettings searchSettings);
    
    // Methods
    public void SetSortBy(string value);
    public void SetSortOrder(string value);
    public void SetFilter(SearchFilter value);
    public void ExecuteSearch();
    public void RefreshResults();
    public void SetView(SearchView view);
    public void AddNodeFilter(string property, string value);
    public void AddDefaultSearchProperty(string property);
    public void ExecuteOpenFolder(FolderViewModel folder);
    public void ExecuteOpenPath(string path);
    
    // Properties
    public FilterControlModel Filter { get; }
    public SearchSettings SearchSettings { get; }
    public ViewMode CurrentViewMode { get; set; }
}
```

## Key Features

### 1. Event-Driven Architecture

The SearchService uses an event-driven architecture to communicate between components:
- **SortBy**: Triggered when the sort criteria changes
- **SortOrder**: Triggered when the sort direction changes
- **SearchFilter**: Triggered when search filters are applied
- **OpenFolder**: Triggered when a folder is opened
- **OpenPath**: Triggered when a specific path is opened
- **Search**: Triggered when a search is executed
- **Refresh**: Triggered when results are refreshed
- **View**: Triggered when the view mode changes

### 2. Filter Management

The SearchService manages search filters through the FilterControlModel, which provides:
- Dynamic filter creation
- Filter application and removal
- Support for multiple filter types
- Integration with the search query builder

### 3. Sorting and View Management

The SearchService handles:
- Sort criteria selection (e.g., Date Created, Filename, Size)
- Sort direction (A-Z, Z-A)
- View mode changes (Grid, List, Folder)

### 4. Search Coordination

The SearchService coordinates search operations between:
- UI components (search input, filters, results view)
- SearchModel (view model for search functionality)
- DataStore (database access)
- ThumbnailService (thumbnail generation)

## Integration with SearchModel

The SearchModel (located in `BerryAIGen.Toolkit/Models/SearchModel.cs`) works closely with the SearchService to provide the view model for search functionality. It includes:

### Key Properties

- `Images`: Observable collection of image entries matching the search criteria
- `CurrentImage`: Currently selected image
- `SearchText`: Text entered in the search box
- `SearchHistory`: History of previous searches
- `Filter`: Filter control model for advanced filtering
- `SortBy`: Current sort criteria
- `SortDirection`: Current sort direction
- `CurrentViewMode`: Current view mode (Grid, List, Folder)

### Key Methods and Commands

- `SearchCommand`: Command executed when a search is initiated
- `Refresh`: Command to refresh search results
- `ClearSearch`: Command to clear the search input
- `ShowFilter/HideFilter`: Commands to toggle filter visibility
- `PageChangedCommand`: Command for pagination

## Search Workflow

1. **User Input**: User enters search text or applies filters
2. **Event Trigger**: SearchService triggers the appropriate event
3. **Query Construction**: SearchModel constructs the search query
4. **Database Query**: DataStore executes the query against the database
5. **Results Processing**: Results are processed and mapped to ImageEntry objects
6. **UI Update**: SearchModel updates the Images collection, triggering UI refresh
7. **Thumbnail Generation**: ThumbnailService generates thumbnails for the results
8. **Results Display**: Results are displayed in the appropriate view mode

## Usage Examples

### Executing a Search

```csharp
// From UI component
searchService.ExecuteSearch();

// This triggers the Search event, which is handled by the SearchModel
```

### Applying a Sort

```csharp
// Set sort by date created
searchService.SetSortBy("Date Created");

// Set sort direction
searchService.SetSortOrder("Z-A");
```

### Changing View Mode

```csharp
// Switch to grid view
searchService.SetView(SearchView.Grid);

// Switch to folder view
searchService.SetView(SearchView.Folder);
```

### Adding a Filter

```csharp
// Add a tag filter
searchService.AddNodeFilter("tag", "landscape");

// Apply the filter
var filter = new SearchFilter { /* filter configuration */ };
searchService.SetFilter(filter);
```

## Related Components

### SearchSettings

Located in `BerryAIGen.Toolkit/Models/SearchSettings.cs`, SearchSettings manages:
- Default search properties
- Search behavior configuration
- User preferences for search functionality

### FilterControlModel

Located in `BerryAIGen.Toolkit/Controls/FilterControlModel.cs`, FilterControlModel provides:
- Dynamic filter creation UI
- Filter validation
- Integration with search queries

### QueryBuilder

The QueryBuilder class constructs SQL queries based on search criteria, handling:
- Text search
- Filter application
- Sorting
- Pagination

## Performance Considerations

### Query Optimization

- Search queries are optimized for performance with appropriate indexing
- Pagination is implemented to limit the number of results loaded at once
- Filtering is applied at the database level to minimize data transfer

### Thumbnail Generation

- Thumbnails are generated asynchronously
- Thumbnail caching is implemented to improve performance
- Thumbnails are generated on-demand based on visible results

### UI Responsiveness

- Search operations are executed on background threads
- Progress indicators are displayed during long-running searches
- The UI remains responsive during search operations

## Best Practices

### 1. Event Handling

- Register event handlers during component initialization
- Unregister event handlers to prevent memory leaks
- Use weak event patterns for long-lived event subscriptions

### 2. Filter Design

- Keep filter logic simple and focused
- Avoid overly complex filters that may impact performance
- Provide clear feedback to users about applied filters

### 3. Search UX

- Implement debouncing for search input to reduce unnecessary queries
- Display search results incrementally if possible
- Provide clear feedback about search status and results count

### 4. Performance Monitoring

- Monitor search query performance
- Optimize frequently used queries
- Consider implementing search result caching for common searches

## Conclusion

The SearchService is a critical component of the BerryAIGen.Toolkit application, providing the central coordination point for all search-related functionality. Its event-driven architecture allows for loose coupling between components, making the system more maintainable and extensible. By understanding how the SearchService works, developers can effectively extend and modify the search functionality to meet evolving requirements.

## Related Documentation

- [MainWindow Component](./main-window.md)
- [ServiceLocator Component](./service-locator.md)
- [DataStore Component](./data-store.md)
- [Thumbnail Service Component](./thumbnail-service.md)
- [MVVM Pattern](./../ui-ux/mvvm-pattern.md)
