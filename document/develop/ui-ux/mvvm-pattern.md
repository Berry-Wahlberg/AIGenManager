# MVVM Pattern Implementation

## Overview
The BerryAIGen.Toolkit application extensively uses the Model-View-ViewModel (MVVM) pattern for its user interface. This pattern separates the UI (View) from the business logic and data (Model) through an intermediary (ViewModel), resulting in a more maintainable, testable, and extensible codebase.

## Core MVVM Components

### 1. Model

The Model represents the data and business logic of the application. In BerryAIGen.Toolkit, models are typically:
- Database entities (Image, Folder, Tag, Album, etc.)
- Service layer components
- Data transfer objects (DTOs)

#### Key Model Characteristics
- **Data-Centric**: Focused on data representation and business rules
- **No UI Dependencies**: Models don't reference UI components or frameworks
- **Testable**: Can be tested independently of the UI
- **Serializable**: Often can be serialized to JSON or other formats

#### Example Model
```csharp
// Database model example
public class Image
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public string Model { get; set; }
    public int Seed { get; set; }
    // Additional properties...
}
```

### 2. View

The View is the user interface layer, consisting of XAML files that define the visual layout and appearance of the application. Views in BerryAIGen.Toolkit are:
- WPF XAML files with code-behind
- Custom controls and user controls
- Pages and windows

#### Key View Characteristics
- **Declarative**: Defined using XAML markup
- **Data-Bound**: Binds to ViewModel properties
- **No Business Logic**: Contains minimal code-behind, focused on UI concerns
- **Reactive**: Updates automatically when bound data changes

#### Example View (XAML)
```xaml
<Window x:Class="BerryAIGen.Toolkit.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="BerryAIGen.Toolkit"
        DataContext="{Binding Main, Source={StaticResource Locator}}">
    <Grid>
        <TextBlock Text="{Binding Title}" />
        <Button Content="Search" Command="{Binding SearchCommand}" />
        <!-- Additional UI elements... -->
    </Grid>
</Window>
```

### 3. ViewModel

The ViewModel acts as an intermediary between the View and Model. It exposes data from the Model to the View and handles user interactions from the View. ViewModels in BerryAIGen.Toolkit:
- Implement `INotifyPropertyChanged` for data binding
- Contain commands for handling UI actions
- Manage application state
- Coordinate with services

#### Key ViewModel Characteristics
- **UI-Focused**: Exposes data in a format suitable for the UI
- **Implements INotifyPropertyChanged**: Notifies the View of data changes
- **Contains Commands**: For handling user interactions
- **No UI Dependencies**: Doesn't reference UI components directly
- **Testable**: Can be tested independently of the UI

#### Base Notify Class
The application provides a base class for ViewModels that implements `INotifyPropertyChanged`:

```csharp
public class BaseNotify : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

#### Example ViewModel
```csharp
public class MainViewModel : BaseNotify
{
    private string _title = "BerryAIGen.Toolkit";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
    
    private ICommand _searchCommand;
    public ICommand SearchCommand
    {
        get => _searchCommand ??= new RelayCommand(Search);
    }
    
    private void Search()
    {
        // Search logic
    }
    
    // Additional properties and commands...
}
```

## Command Implementation

### RelayCommand
The `RelayCommand` class implements `ICommand` for synchronous operations:

```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;
    
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }
    
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
    
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
```

### AsyncCommand
The `AsyncCommand` class implements `IAsyncCommand` for asynchronous operations:

```csharp
public class AsyncCommand : IAsyncCommand
{
    private readonly Func<object, Task> _execute;
    private readonly Func<object, bool> _canExecute;
    private bool _isExecuting;
    
    public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object parameter)
    {
        return !_isExecuting && (_canExecute == null || _canExecute(parameter));
    }
    
    public async Task ExecuteAsync(object parameter)
    {
        if (CanExecute(parameter))
        {
            try
            {
                _isExecuting = true;
                OnCanExecuteChanged();
                await _execute(parameter);
            }
            finally
            {
                _isExecuting = false;
                OnCanExecuteChanged();
            }
        }
    }
    
    // Additional ICommand implementation...
}
```

## Data Binding

### Binding Modes
The application uses various binding modes depending on the scenario:

| Mode | Description | Usage |
|------|-------------|-------|
| **OneWay** | Data flows from ViewModel to View | Displaying read-only data |
| **TwoWay** | Data flows in both directions | Editable fields |
| **OneWayToSource** | Data flows from View to ViewModel | Capturing user input |
| **OneTime** | Data flows once from ViewModel to View | Static data |

### Value Converters
The application uses value converters to transform data between the ViewModel and View:

```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility)value == Visibility.Visible;
    }
}
```

### Binding Proxy
The `BindingProxy` class allows binding to data contexts in XAML where direct binding is not possible:

```csharp
public class BindingProxy : Freezable
{
    protected override Freezable CreateInstanceCore()
    {
        return new BindingProxy();
    }
    
    public object Data
    {
        get { return GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }
    
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        "Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
}
```

## ViewModelLocator Pattern

The application uses a simple ViewModelLocator pattern through the `ServiceLocator` to connect Views to ViewModels:

```csharp
// Accessing ViewModels through ServiceLocator
var mainModel = ServiceLocator.MainModel;
var searchModel = ServiceLocator.SearchModel;
```

## Benefits of MVVM in BerryAIGen.Toolkit

### 1. Separation of Concerns
- Clear separation between UI, business logic, and data
- Easier to maintain and modify individual components
- Better organization of code

### 2. Testability
- ViewModels can be tested independently of the UI
- Models can be tested without UI dependencies
- Better test coverage

### 3. Maintainability
- Changes to the UI don't require changes to business logic
- Changes to business logic don't require changes to the UI
- Easier to update and extend functionality

### 4. Reusability
- ViewModels can be reused across multiple Views
- Views can be redesigned without changing ViewModels
- Components can be shared between projects

### 5. Designer-Developer Workflow
- Designers can work on XAML files independently
- Developers can work on ViewModels and Models independently
- Better collaboration between designers and developers

## MVVM Best Practices

### 1. Keep Views Simple
- Minimize code-behind in Views
- Use XAML for UI layout and styling
- Focus on presentation, not business logic

### 2. Keep ViewModels Focused
- Each ViewModel should have a single responsibility
- Avoid putting UI logic in ViewModels
- Keep ViewModels testable

### 3. Use Commands for User Interactions
- Use `RelayCommand` for synchronous operations
- Use `AsyncCommand` for asynchronous operations
- Avoid event handlers in code-behind

### 4. Implement Property Change Notification
- Use the `SetProperty` method from `BaseNotify`
- Notify changes for all bound properties
- Be mindful of performance when notifying changes

### 5. Use Dependency Injection
- Inject services into ViewModels
- Avoid tight coupling between components
- Use the `ServiceLocator` for service access

### 6. Use Data Templates
- Define data templates for complex data types
- Use content controls with data templates for dynamic UI
- Keep UI consistent across the application

## Conclusion

The MVVM pattern is a fundamental part of the BerryAIGen.Toolkit application architecture. It provides a clear separation of concerns, improves testability, and makes the codebase more maintainable and extensible. By following MVVM best practices, the application achieves a clean, well-organized structure that allows for easy updates and enhancements.

