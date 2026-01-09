# BerryAIGen.Github Module

## Overview

The BerryAIGen.Github module is a client library for interacting with the GitHub API, primarily focused on accessing repository releases and tags. This module enables the BerryAIGen.Toolkit application to check for updates, download release assets, and retrieve information about releases and tags from GitHub.

## Core Implementation

### Location
The BerryAIGen.Github module is located in the `BerryAIGen.Github` directory.

### Key Components

#### 1. GithubClient

##### Purpose
The `GithubClient` class is the main entry point for interacting with the GitHub API. It provides methods to retrieve releases, tags, and download release assets.

##### Location
`BerryAIGen.Github/GithubClient.cs`

##### Class Structure

```csharp
public class GithubClient : IDisposable
{
    private readonly HttpClient _client;
    private readonly string _user;
    private readonly string _repo;
    private const string _userAgent = "GithubClient/1.0";
    
    // Constructor
    public GithubClient(string user, string repo);
    
    // Methods
    public Task<IEnumerable<Release>?> GetReleases();
    public Task<IEnumerable<Tag>?> GetTags();
    public Task<Stream> DownloadAsync(string url);
    public async Task<IEnumerable<Release>?> GetReleases(CancellationToken token);
    public async Task<IEnumerable<Tag>?> GetTags(CancellationToken token);
    public async Task<Stream> DownloadAsync(string url, CancellationToken token);
    
    // IDisposable implementation
    public void Dispose();
}
```

##### Key Features

- **Release Retrieval**: Get all releases for a repository
- **Tag Retrieval**: Get all tags for a repository
- **Asset Download**: Download release assets with redirect handling
- **Async Operations**: All API calls are asynchronous
- **CancellationToken Support**: Support for cancelling operations
- **IDisposable**: Proper resource management

#### 2. Model Classes

The module includes model classes to represent GitHub API responses:

- **Release**: Represents a GitHub release with assets, author, and release information
- **Asset**: Represents a release asset (downloadable files)
- **Author**: Represents the author of a release
- **Tag**: Represents a GitHub tag
- **Uploader**: Represents the uploader of a release asset

## API Endpoints

The module interacts with the following GitHub API endpoints:

| Endpoint | Method | Purpose |
|----------|--------|---------|
| `/repos/{owner}/{repo}/releases` | GET | Retrieve all releases for a repository |
| `/repos/{owner}/{repo}/tags` | GET | Retrieve all tags for a repository |
| Release asset URLs | GET | Download release assets |

## Usage Examples

### Creating a Client

```csharp
using var client = new GithubClient("RupertAvery", "BerryAIGen.Toolkit");
```

### Getting Releases

```csharp
var releases = await client.GetReleases();
```

### Getting Tags

```csharp
var tags = await client.GetTags();
```

### Downloading a Release Asset

```csharp
var latestRelease = releases.First();
var asset = latestRelease.Assets.First(a => a.Name.EndsWith(".exe"));
using var stream = await client.DownloadAsync(asset.BrowserDownloadUrl);
```

### Using CancellationToken

```csharp
using var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

var releases = await client.GetReleases(cancellationTokenSource.Token);
```

## Integration with BerryAIGen.Toolkit

The BerryAIGen.Github module is used in the BerryAIGen.Toolkit application to:

1. **Update Checking**: Check for the latest releases on GitHub
2. **Release Information**: Retrieve release notes and version information
3. **Asset Download**: Download update packages
4. **Tag Information**: Get tag information for versioning

## Performance Considerations

### API Rate Limits

- GitHub API has rate limits (60 requests per hour for anonymous requests)
- The client should respect these limits
- Consider implementing exponential backoff for retries
- Add caching to reduce API calls

### Async Operations

- All API calls are asynchronous to avoid blocking the UI
- Use cancellation tokens for long-running operations

### Redirect Handling

- The DownloadAsync method properly handles redirects
- Follows HTTP 301 (Moved Permanently) and 302 (Found) redirects

## Security

### HTTPS

- All API calls use HTTPS for secure communication
- No sensitive data is transmitted without encryption

### Authentication

- The current implementation doesn't support authenticated requests
- The design allows for future extension to support GitHub API tokens
- API tokens would increase rate limits and enable access to private repositories

## Future Enhancements

Potential future enhancements for the BerryAIGen.Github module:

1. **Authentication Support**: Add support for GitHub API tokens
2. **Release Creation**: Add ability to create releases
3. **Asset Upload**: Add ability to upload release assets
4. **Commit Information**: Add ability to retrieve commit information
5. **Pull Request Support**: Add support for pull requests
6. **Issue Tracking**: Add support for issues
7. **Branch Information**: Add ability to retrieve branch information

## Conclusion

The BerryAIGen.Github module provides a simple and efficient interface for interacting with the GitHub API, enabling the BerryAIGen.Toolkit application to integrate with GitHub for update checking and release management. With its clean design, asynchronous operations, and comprehensive model classes, it offers a solid foundation for GitHub integration.

By leveraging this module, the BerryAIGen.Toolkit application can provide users with automatic update checking, easy access to release notes, and seamless updates to the latest version.

## Related Documentation

- [GitHub API Documentation](https://docs.github.com/en/rest)
- [BerryAIGen.Toolkit Module](./berryaigen-toolkit.md)
- [BerryAIGen.Civitai Module](./berryaigen-civitai.md)
