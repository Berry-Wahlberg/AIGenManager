# Testing Strategy

## Overview

The BerryAIGen.Toolkit project follows a comprehensive testing strategy to ensure the quality, reliability, and maintainability of the codebase. This document outlines the testing approach, frameworks used, and best practices for writing and maintaining tests.

## Testing Levels

### 1. Unit Testing

Unit tests verify the functionality of individual components in isolation. They are the foundation of our testing strategy and provide the fastest feedback on code changes.

#### Purpose
- Test individual methods and classes
- Verify business logic
- Ensure code behaves as expected under various conditions
- Catch regression bugs early

#### Scope
- Business logic components
- Utility classes
- Data access layers
- Service implementations

#### Framework
- **Test Framework**: NUnit
- **Mocking**: Moq
- **Assertion**: NUnit Assert API

#### Example

```csharp
[TestFixture]
public class QueryBuilderTests
{
    [Test]
    public void Parse_ValidModelQuery_ReturnsCorrectWhereClause()
    {
        // Arrange
        var queryBuilder = new QueryBuilder();
        var query = "model:\"test model\"";
        
        // Act
        var result = queryBuilder.Parse(query);
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result.WhereClause);
        Assert.Contains("model = @model", result.WhereClause, StringComparison.OrdinalIgnoreCase);
    }
}
```

### 2. Integration Testing

Integration tests verify the interaction between multiple components and external systems, such as databases and APIs.

#### Purpose
- Test component interactions
- Verify database operations
- Test API integrations
- Ensure end-to-end functionality

#### Scope
- Database interactions
- Service layer integration
- External API calls
- File system operations

#### Framework
- **Test Framework**: NUnit
- **Database**: SQLite in-memory database
- **API Testing**: HttpClient with test servers

#### Example

```csharp
[TestFixture]
public class DataStoreIntegrationTests
{
    private IDbConnection _connection;
    private DataStore _dataStore;
    
    [SetUp]
    public void SetUp()
    {
        // Create in-memory database
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        
        // Initialize schema
        DatabaseInitializer.Initialize(_connection);
        
        // Create data store
        _dataStore = new DataStore(_connection);
    }
    
    [Test]
    public async Task AddImageAsync_ValidImage_ReturnsId()
    {
        // Arrange
        var image = new Image { FilePath = "test.jpg", ModelName = "Test Model" };
        
        // Act
        var id = await _dataStore.AddImageAsync(image);
        
        // Assert
        Assert.Greater(id, 0);
        
        // Verify image was added
        var retrievedImage = await _dataStore.GetImageByIdAsync(id);
        Assert.IsNotNull(retrievedImage);
        Assert.AreEqual(image.FilePath, retrievedImage.FilePath);
    }
    
    [TearDown]
    public void TearDown()
    {
        _connection.Close();
    }
}
```

### 3. UI Testing

UI tests verify the functionality of the user interface, including user interactions and visual components.

#### Purpose
- Test user workflows
- Verify UI controls behave correctly
- Ensure visual consistency
- Test accessibility features

#### Scope
- Main window functionality
- Dialog interactions
- Menu operations
- Control behavior

#### Framework
- **Test Framework**: NUnit
- **UI Testing**: FlaUI or TestStack.White

#### Example

```csharp
[TestFixture]
public class MainWindowTests
{
    private Application _application;
    private Window _mainWindow;
    
    [SetUp]
    public void SetUp()
    {
        // Launch the application
        _application = Application.Launch("BerryAIGen.Toolkit.exe");
        
        // Get the main window
        _mainWindow = _application.GetMainWindow(TimeSpan.FromSeconds(5));
    }
    
    [Test]
    public void SearchButton_Click_ExecutesSearch()
    {
        // Arrange
        var searchTextBox = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("SearchTextBox"));
        var searchButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("SearchButton"));
        
        // Act
        searchTextBox.AsTextBox().Enter("test");
        searchButton.AsButton().Click();
        
        // Assert
        var resultsListView = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ResultsListView"));
        Assert.IsNotNull(resultsListView);
        // Additional assertions for search results
    }
    
    [TearDown]
    public void TearDown()
    {
        _application.Close();
    }
}
```

## Test Organization

### 1. Project Structure

Tests are organized in a separate test project with a structure that mirrors the main application:

```
TestHarness/
鈹溾攢鈹€ UnitTests/
鈹?  鈹溾攢鈹€ Database/
鈹?  鈹溾攢鈹€ Services/
鈹?  鈹溾攢鈹€ Utils/
鈹?  鈹斺攢鈹€ ...
鈹溾攢鈹€ IntegrationTests/
鈹?  鈹溾攢鈹€ DataStoreTests/
鈹?  鈹溾攢鈹€ ServiceIntegrationTests/
鈹?  鈹斺攢鈹€ ...
鈹溾攢鈹€ UITests/
鈹?  鈹溾攢鈹€ MainWindowTests/
鈹?  鈹溾攢鈹€ DialogTests/
鈹?  鈹斺攢鈹€ ...
鈹斺攢鈹€ TestUtils/
    鈹溾攢鈹€ MockServices/
    鈹溾攢鈹€ TestData/
    鈹斺攢鈹€ ...
```

### 2. Naming Conventions

#### Test Files
- Follow the same naming convention as the source files they test
- Append "Tests" to the source file name
- Example: `QueryBuilderTests.cs` for `QueryBuilder.cs`

#### Test Methods
- Use descriptive names that follow the pattern: `MethodUnderTest_Condition_ExpectedResult`
- Use PascalCase
- Be specific about the scenario being tested

#### Example

```csharp
// Good
[Test]
public void Parse_ValidModelQuery_ReturnsCorrectWhereClause()
{
    // Implementation
}

// Bad
[Test]
public void TestParse()
{
    // Implementation
}
```

## Test Writing Guidelines

### 1. Arrange-Act-Assert Pattern

All tests should follow the Arrange-Act-Assert pattern for clarity and consistency:

```csharp
[Test]
public void MethodUnderTest_Condition_ExpectedResult()
{
    // Arrange: Set up test dependencies and state
    var service = new MyService();
    var input = "test";
    
    // Act: Execute the method under test
    var result = service.Process(input);
    
    // Assert: Verify the result
    Assert.AreEqual("expected", result);
}
```

### 2. Test Isolation

- Each test should be independent
- Tests should not share state
- Use fresh instances for each test
- Clean up after each test if necessary

### 3. Test Coverage

#### Target Coverage
- Aim for 80%+ code coverage for business logic
- Focus on critical paths and edge cases
- Test both successful and failure scenarios

#### Coverage Measurement
- Use dotCover or Coverlet for code coverage analysis
- Include coverage reports in CI/CD pipeline
- Review coverage reports regularly to identify gaps

### 4. Testing Edge Cases

- Test with null values
- Test with empty strings
- Test with invalid inputs
- Test with boundary values
- Test with maximum and minimum values

### 5. Mocking Best Practices

- Mock only external dependencies
- Don't mock internal implementation details
- Verify mock interactions sparingly
- Use strict mocks for critical dependencies
- Keep mock setup simple and focused

```csharp
// Good: Mock only what's needed
var mockDataStore = new Mock<IDataStore>();
mockDataStore.Setup(ds => ds.GetImageByIdAsync(It.IsAny<int>()))
    .ReturnsAsync(new Image { Id = 1, FilePath = "test.jpg" });

// Bad: Overly complex mock setup
var mockDataStore = new Mock<IDataStore>();
mockDataStore.Setup(ds => ds.GetImageByIdAsync(It.IsAny<int>()))
    .ReturnsAsync(new Image { Id = 1, FilePath = "test.jpg" });
mockDataStore.Setup(ds => ds.GetImagesAsync(It.IsAny<string>()))
    .ReturnsAsync(new List<Image>());
mockDataStore.Setup(ds => ds.AddImageAsync(It.IsAny<Image>()))
    .ReturnsAsync(1);
```

## Test Maintenance

### 1. Running Tests

#### Local Development
- Run tests frequently during development
- Use Visual Studio Test Explorer for interactive testing
- Run specific test categories based on changes

#### CI/CD Pipeline
- Run all tests on every push
- Run unit tests first for fast feedback
- Run integration and UI tests in parallel
- Fail the build if tests fail

### 2. Test Naming and Organization

- Keep test files organized by feature
- Use descriptive test names
- Group related tests with categories
- Remove obsolete tests

### 3. Test Data Management

- Use test data builders for consistent test data
- Avoid hard-coded test data
- Use realistic test data where possible
- Keep test data separate from test code

### 4. Test Updates

- Update tests when changing functionality
- Maintain backward compatibility when possible
- Refactor tests along with production code
- Ensure tests remain readable and maintainable

## Best Practices

### 1. Write Tests Early

- Follow Test-Driven Development (TDD) for new features
- Write tests before implementing functionality
- Use tests to design the API and behavior

### 2. Test Behavior, Not Implementation

- Focus on what the code does, not how it does it
- Avoid testing private methods directly
- Test through public APIs
- Refactoring should not break tests

### 3. Keep Tests Fast

- Unit tests should run in milliseconds
- Integration tests should run in seconds
- UI tests should be kept to a minimum
- Use parallel execution to speed up test runs

### 4. Use Meaningful Assertions

- Use specific assertions
- Include descriptive failure messages
- Test one thing per assertion
- Avoid overly complex assertions

```csharp
// Good: Specific assertion with descriptive message
Assert.IsTrue(result.IsSuccess, "Search should succeed with valid query");
Assert.AreEqual(expectedCount, result.Images.Count, "Search should return expected number of results");

// Bad: Vague assertion
Assert.IsNotNull(result);
```

### 5. Test Error Conditions

- Test exception handling
- Verify error messages
- Test validation logic
- Ensure graceful degradation

## Test Automation

### 1. CI/CD Integration

Tests are integrated into the CI/CD pipeline to ensure that every code change is validated:

- **Build Stage**: Compile code and run unit tests
- **Test Stage**: Run integration tests
- **UI Test Stage**: Run UI tests (optional, scheduled)
- **Deployment Stage**: Deploy only if all tests pass

### 2. Test Reporting

- Generate test reports in JUnit format
- Include code coverage metrics
- Publish reports to CI/CD dashboard
- Send notifications for failed tests

## Conclusion

A comprehensive testing strategy is essential for maintaining the quality and reliability of the BerryAIGen.Toolkit application. By following these guidelines and best practices, we ensure that our tests are effective, maintainable, and provide valuable feedback on the codebase. Unit tests form the foundation, while integration and UI tests provide broader verification of the application's functionality. Regularly running and maintaining tests helps catch bugs early, improves code quality, and enables confident refactoring and feature development.
