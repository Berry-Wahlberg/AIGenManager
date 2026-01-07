# BerryAIGen.Civitai Module

## Overview

The BerryAIGen.Civitai module is a client library for interacting with the Civitai API, which provides access to AI models, their metadata, and related information. This module enables the BerryAIGen.Toolkit application to integrate with the Civitai platform, allowing users to search for models, retrieve model details, and associate models with generated images.

## Core Implementation

### Location
The BerryAIGen.Civitai module is located in the `BerryAIGen.Civitai` directory.

### Key Components

#### 1. CivitaiClient

##### Purpose
The `CivitaiClient` class is the main entry point for interacting with the Civitai API. It provides methods to search for models, retrieve model details, and get model versions by hash.

##### Location
`BerryAIGen.Civitai/CivitaiClient.cs`

##### Class Structure

```csharp
public class CivitaiClient : IDisposable
{
    private readonly string _baseUrl = "https://civitai.com/api/v1";
    private readonly HttpClient _httpClient;
    
    // Constructor
    public CivitaiClient();
    
    // Methods
    public async Task<Results<LiteModel>?> GetLiteModelsAsync(ModelSearchParameters searchParameters, CancellationToken token);
    public async Task<Results<LiteModel>?> GetLiteModels(string url, CancellationToken token);
    public async Task<Results<Model>?> GetModels(string url, CancellationToken token);
    public async Task<Results<Model>?> GetModelsAsync(ModelSearchParameters searchParameters, CancellationToken token);
    public async Task<ModelVersion2> GetModelVersionsByHashAsync(string hash, CancellationToken token);
    
    // Properties
    public string BaseUrl { get; };
    
    // IDisposable implementation
    public void Dispose();
}
```

##### Key Features

- **Model Search**: Search for models with various parameters
- **Model Retrieval**: Get detailed model information
- **Model Version Lookup**: Find model versions by hash
- **Async Operations**: All API calls are asynchronous
- **Error Handling**: Comprehensive exception handling
- **IDisposable**: Proper resource management

#### 2. Model Classes

The module includes a comprehensive set of model classes to represent Civitai API responses:

- **Model**: Complete model information
- **LiteModel**: Simplified model information for list views
- **ModelVersion**: Model version details
- **ModelFile**: Model file information
- **ModelImage**: Model preview image
- **Hashes**: Model file hashes for identification
- **Creator**: Model creator information
- **Stats**: Model statistics

#### 3. Search Parameters

The `ModelSearchParameters` class allows for flexible searching of models:

- **ModelType**: Filter by model type (Checkpoint, LoRA, Embedding, etc.)
- **SortOrder**: Sort results by popularity, downloads, likes, etc.
- **Limit**: Number of results to return
- **Page**: Page number for pagination
- **Query**: Search query string
- **Tag**: Filter by tag
- **Favorite**: Filter by favorite status

## API Endpoints

The module interacts with the following Civitai API endpoints:

| Endpoint | Method | Purpose |
|----------|--------|---------|
| `/api/v1/models` | GET | Search and retrieve models |
| `/api/v1/model-versions/by-hash/{hash}` | GET | Get model version by hash |

## Usage Examples

### Creating a Client

```csharp
using var client = new CivitaiClient();
```

### Searching for Models

```csharp
var searchParams = new ModelSearchParameters
{
    ModelType = ModelType.Checkpoint,
    SortOrder = SortOrder.MostDownloaded,
    Limit = 20,
    Query = "realistic"
};

var results = await client.GetModelsAsync(searchParams, CancellationToken.None);
```

### Getting Model Version by Hash

```csharp
var modelVersion = await client.GetModelVersionsByHashAsync("abc123def456", CancellationToken.None);
```

### Using Lite Models for Performance

```csharp
var results = await client.GetLiteModelsAsync(searchParams, CancellationToken.None);
```

## Error Handling

The module includes a custom exception class `CivitaiRequestException` for handling API errors:

```csharp
try
{
    var results = await client.GetModelsAsync(searchParams, CancellationToken.None);
}
catch (CivitaiRequestException ex)
{
    // Handle API-specific errors
    Console.WriteLine($"Civitai API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
catch (Exception ex)
{
    // Handle general errors
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Integration with BerryAIGen.Toolkit

The BerryAIGen.Civitai module is used in the BerryAIGen.Toolkit application to:

1. **Model Identification**: Identify models used in generated images by their hash
2. **Model Metadata**: Retrieve detailed metadata for identified models
3. **Model Search**: Allow users to search for models within the application
4. **Model Association**: Associate models with generated images for better organization
5. **Model Updates**: Check for model updates

## Performance Considerations

### API Rate Limits

- Civitai API has rate limits that should be respected
- Implement proper error handling for rate limit responses
- Consider adding caching to reduce API calls

### Asynchronous Operations

- All API calls are asynchronous to avoid blocking the UI
- Use cancellation tokens to allow users to cancel long-running operations

### Lite Models

- Use `GetLiteModelsAsync()` for list views to reduce data transfer
- Only retrieve full model details when needed

## Security

### API Key Considerations

- The current implementation does not support API keys, but the design allows for future extension
- Consider adding API key support for increased rate limits and access to private models

### Data Privacy

- Be mindful of user data when making API calls
- Implement proper data handling practices
- Respect user privacy settings

## Future Enhancements

Potential future enhancements for the BerryAIGen.Civitai module:

1. **API Key Support**: Add support for Civitai API keys
2. **Model Download**: Allow downloading models directly from the application
3. **Model Updates**: Implement automatic model update checks
4. **Enhanced Search**: Add more search filters and options
5. **Caching**: Add local caching of model information
6. **Batch Operations**: Support batch model lookups
7. **Webhook Support**: Add support for Civitai webhooks

## Conclusion

The BerryAIGen.Civitai module provides a robust interface for interacting with the Civitai API, enabling the BerryAIGen.Toolkit application to integrate with the Civitai platform. With its comprehensive model classes, flexible search options, and asynchronous design, it offers a solid foundation for building AI model management features.

By leveraging this module, the BerryAIGen.Toolkit application can provide users with enhanced functionality for managing AI-generated images, including model identification, metadata retrieval, and model search capabilities.

## Related Documentation

- [Civitai API Documentation](https://docs.civitai.com/api-reference)
- [BerryAIGen.Toolkit Module](./berryaigen-toolkit.md)
- [BerryAIGen.IO Module](./berryaigen-io.md)
