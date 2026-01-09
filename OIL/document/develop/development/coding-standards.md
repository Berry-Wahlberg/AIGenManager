# Coding Standards

## Overview

This document outlines the coding standards and conventions followed in the BerryAIGen.Toolkit project. Adhering to these standards ensures code consistency, readability, and maintainability across the entire codebase.

## General Principles

### 1. Readability First

- Code should be self-documenting
- Use descriptive names for variables, methods, and classes
- Avoid overly complex expressions
- Break long methods into smaller, focused methods

### 2. Consistency

- Follow the existing code style
- Use the same conventions throughout the codebase
- Maintain consistent indentation and formatting

### 3. Maintainability

- Write code that is easy to understand and modify
- Minimize dependencies between components
- Use appropriate design patterns
- Document complex logic

### 4. Performance

- Write efficient code without sacrificing readability
- Avoid unnecessary computations
- Use appropriate data structures
- Consider memory usage

## C# Coding Standards

### 1. Naming Conventions

#### Classes and Interfaces

- Use PascalCase for class and interface names
- Use descriptive, noun-based names
- Interfaces should start with "I"

```csharp
// Good
public class ImageProcessor { }
public interface IImageService { }

// Bad
public class imgProc { }
public interface ImageService { } // Missing "I" prefix
```

#### Methods and Functions

- Use PascalCase for method names
- Use verb-based names that describe the action
- Include descriptive parameters

```csharp
// Good
public void ProcessImage(string filePath) { }
public async Task<ImageMetadata> ExtractMetadataAsync(string filePath) { }

// Bad
public void DoStuff(string f) { }
```

#### Variables and Fields

- Use camelCase for local variables and private fields
- Use PascalCase for public fields and properties
- Private fields should start with an underscore
- Constants should be in SCREAMING_SNAKE_CASE

```csharp
// Good
private string _filePath;
public string FilePath { get; set; }
private const int MAX_RETRY_COUNT = 3;

// Bad
private string filePath; // Missing underscore
public string filepath { get; set; } // Incorrect casing
```

#### Properties

- Use PascalCase for property names
- Prefer auto-implemented properties when possible
- Use meaningful names

```csharp
// Good
public string Name { get; set; }
public int Width { get; private set; }

// Bad
public string n { get; set; }
```

#### Namespaces

- Use PascalCase for namespace components
- Follow the project structure
- Use the company name as the root namespace

```csharp
// Good
namespace BerryAIGen.Toolkit.Services

// Bad
namespace berryAIGen.toolkit.services
```

### 2. Formatting

#### Indentation

- Use 4 spaces per indentation level
- Do not use tabs
- Maintain consistent indentation throughout

#### Line Length

- Limit lines to 120 characters
- Break long lines appropriately
- Use line breaks for readability

#### Braces

- Use Allman style braces (new line for opening and closing braces)
- Always use braces, even for single-line statements
- Align closing braces with the corresponding opening statement

```csharp
// Good
if (condition)
{
    // Code here
}

// Bad
if (condition) // Missing braces
    DoSomething();
```

#### Spaces

- Use a single space after keywords like `if`, `while`, `for`, `foreach`
- Use a single space before and after operators
- Do not use spaces inside parentheses or brackets
- Use spaces around commas and semicolons

```csharp
// Good
if (x > 5)
{
    // Code here
}

for (int i = 0; i < 10; i++)
{
    // Code here
}

// Bad
if(x>5)
{
    // Code here
}
```

### 3. Comments

#### XML Documentation Comments

- Use XML documentation comments for all public members
- Include summary, parameters, returns, and exceptions
- Use <see> and <seealso> tags for cross-references

```csharp
/// <summary>
/// Extracts metadata from an image file.
/// </summary>
/// <param name="filePath">The path to the image file.</param>
/// <returns>The extracted metadata.</returns>
/// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
public async Task<ImageMetadata> ExtractMetadataAsync(string filePath)
{
    // Implementation
}
```

#### Inline Comments

- Use inline comments sparingly
- Explain why, not what
- Use comments for complex logic or non-obvious code

```csharp
// Good: Explains the purpose of the delay
await Task.Delay(1000); // Wait for file system to stabilize

// Bad: States the obvious
int x = 5; // Assign 5 to x
```

### 4. Control Structures

#### If-Else Statements

- Keep if-else chains short and readable
- Use early returns to simplify logic
- Avoid deeply nested if statements

```csharp
// Good: Early return
if (!File.Exists(filePath))
    return null;

// Continue with processing

// Bad: Deep nesting
if (condition1)
{
    if (condition2)
    {
        if (condition3)
        {
            // Code here
        }
    }
}
```

#### Switch Statements

- Always include a default case
- Use break statements appropriately
- Consider using pattern matching when appropriate

```csharp
// Good
switch (status)
{
    case Status.Success:
        // Handle success
        break;
    case Status.Error:
        // Handle error
        break;
    default:
        // Handle unexpected status
        break;
}
```

#### Loops

- Use foreach when iterating over collections
- Prefer LINQ for complex queries
- Avoid infinite loops

```csharp
// Good
foreach (var file in files)
{
    // Process file
}

// Good: LINQ query
var imageFiles = files.Where(f => f.EndsWith(".jpg") || f.EndsWith(".png"));
```

### 5. Asynchronous Programming

- Use async/await for asynchronous operations
- Name async methods with Async suffix
- Avoid mixing sync and async code unnecessarily
- Use ConfigureAwait(false) for library code

```csharp
// Good
public async Task ProcessFilesAsync(IEnumerable<string> files)
{
    foreach (var file in files)
    {
        await ProcessFileAsync(file).ConfigureAwait(false);
    }
}

// Bad
public void ProcessFiles(IEnumerable<string> files)
{
    foreach (var file in files)
    {
        ProcessFileAsync(file).Wait(); // Blocking call
    }
}
```

### 6. Error Handling

- Use exceptions for exceptional conditions
- Catch specific exceptions, not general Exception
- Include meaningful error messages
- Use try-catch-finally for resource cleanup

```csharp
// Good
try
{
    await File.WriteAllTextAsync(filePath, content);
}
catch (IOException ex)
{
    Log.Error(ex, "Failed to write to file: {FilePath}", filePath);
    throw;
}

// Bad
try
{
    // Code here
}
catch (Exception ex) // Catches all exceptions
{
    Console.WriteLine("Error: " + ex.Message); // Generic error message
}
```

## XAML Coding Standards

### 1. Naming Conventions

- Use PascalCase for control names
- Use meaningful, descriptive names
- Use prefixes for control types (optional but consistent)

```xaml
<!-- Good -->
<Button x:Name="SearchButton" Content="Search" />
<TextBox x:Name="SearchTextBox" />

<!-- Bad -->
<Button x:Name="btn1" Content="Search" />
```

### 2. Formatting

- Indent XAML elements with 4 spaces
- Close tags on the same line for simple elements
- Use separate lines for complex elements with children
- Align attributes for readability

```xaml
<!-- Good -->
<Button x:Name="SearchButton" 
        Content="Search" 
        Command="{Binding SearchCommand}" />

<Grid>
    <StackPanel Orientation="Vertical">
        <TextBlock Text="Results" />
        <ListView ItemsSource="{Binding Results}" />
    </StackPanel>
</Grid>

<!-- Bad -->
<Button x:Name="SearchButton" Content="Search" Command="{Binding SearchCommand}" />
<Grid><StackPanel Orientation="Vertical"><TextBlock Text="Results" /><ListView ItemsSource="{Binding Results}" /></StackPanel></Grid>
```

### 3. Resource Naming

- Use descriptive names for resources
- Use PascalCase for resource keys
- Group related resources together

```xaml
<!-- Good -->
<SolidColorBrush x:Key="PrimaryButtonBackground" Color="#0078D7" />
<Style x:Key="DefaultButtonStyle" TargetType="Button">
    <!-- Style definition -->
</Style>

<!-- Bad -->
<SolidColorBrush x:Key="Brush1" Color="#0078D7" />
```

### 4. Binding Conventions

- Use relative source for element bindings
- Use proper binding modes
- Use string formatting appropriately
- Avoid code-behind for simple bindings

```xaml
<!-- Good -->
<TextBox Text="{Binding Name, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
<TextBlock Text="{Binding Count, StringFormat='Items: {0}'}" />
<ListView ItemsSource="{Binding Items}" />

<!-- Bad -->
<TextBox Text="{Binding}" />
```

## Design Patterns

### 1. MVVM (Model-View-ViewModel)

- Separate UI logic from business logic
- Use data binding for communication between View and ViewModel
- Implement INotifyPropertyChanged for ViewModel properties
- Use RelayCommand/AsyncCommand for commands

### 2. Service Locator

- Use ServiceLocator for dependency management
- Register services during application startup
- Resolve services when needed
- Avoid direct service instantiation

### 3. Repository Pattern

- Use repositories for data access
- Abstract database operations behind interfaces
- Implement unit-of-work pattern when appropriate

### 4. Command Pattern

- Use RelayCommand for synchronous commands
- Use AsyncCommand for asynchronous commands
- Implement CanExecute logic appropriately
- Avoid business logic in commands

## Best Practices

### 1. Code Organization

- Keep related code together
- Follow the project structure
- Use folders to organize code by feature or functionality
- Limit the size of files (preferably under 500 lines)

### 2. Dependency Management

- Use constructor injection for dependencies
- Register dependencies with the ServiceLocator
- Avoid circular dependencies
- Use interfaces to decouple components

### 3. Testing

- Write unit tests for business logic
- Use mock objects for dependencies
- Test edge cases
- Maintain a good test coverage

### 4. Documentation

- Document public APIs with XML comments
- Update documentation when changing code
- Write clear, concise comments
- Document complex algorithms and logic

### 5. Performance

- Optimize hot paths
- Use efficient algorithms and data structures
- Avoid unnecessary object creation
- Dispose of resources properly

### 6. Security

- Validate user input
- Use parameterized queries to prevent SQL injection
- Securely store sensitive data
- Follow security best practices for external API calls

## Code Review Checklist

- [ ] Follows naming conventions
- [ ] Proper formatting and indentation
- [ ] Readable and maintainable code
- [ ] Proper error handling
- [ ] Correct use of design patterns
- [ ] Adequate documentation
- [ ] Test coverage
- [ ] Performance considerations
- [ ] Security best practices

## Conclusion

Adhering to these coding standards ensures that the BerryAIGen.Toolkit codebase remains consistent, readable, and maintainable. By following these guidelines, developers can work more efficiently and collaboratively, reducing the likelihood of bugs and making the codebase easier to extend and modify in the future.
