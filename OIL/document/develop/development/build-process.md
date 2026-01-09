# Build Process

## Overview

The BerryAIGen.Toolkit project uses a modern build process with CI/CD integration to ensure consistent, high-quality builds. This document describes the build configuration, CI/CD pipeline, and deployment process.

## Build Configuration

### 1. Project Structure

The project uses a multi-project solution structure with the following key components:

- **Solution File**: `BerryAIGen.sln` - Main solution containing all projects
- **Main Application**: `BerryAIGen.Toolkit` - WPF application
- **Library Projects**: 
  - `BerryAIGen.Database` - Database access layer
  - `BerryAIGen.Civitai` - Civitai API integration
  - `BerryAIGen.Github` - GitHub API integration
  - `BerryAIGen.Common` - Common utilities
- **Test Projects**: `TestHarness` - Unit and integration tests

### 2. Build System

The project uses MSBuild for building, with configuration managed through:

- **.csproj Files**: Project-specific build configuration
- **Directory.Build.props**: Solution-wide build settings
- **Directory.Build.targets**: Custom build targets

### 3. Target Frameworks

| Project | Target Framework |
|---------|------------------|
| BerryAIGen.Toolkit | .NET 8.0 |
| BerryAIGen.Database | .NET 8.0 |
| BerryAIGen.Civitai | .NET 8.0 |
| BerryAIGen.Github | .NET 8.0 |
| BerryAIGen.Common | .NET 8.0 |
| TestHarness | .NET 8.0 |

### 4. Build Configurations

#### Debug
- Optimized for development
- Includes debug symbols
- Enables logging and debugging features
- Disables code optimization

#### Release
- Optimized for production
- No debug symbols
- Disables logging and debugging features
- Enables code optimization
- Creates single-file executable for deployment

## Building the Project

### 1. Using Visual Studio

1. Open `BerryAIGen.sln` in Visual Studio
2. Select the desired configuration (Debug/Release)
3. Select the target platform (x64/x86/Any CPU)
4. Build the solution using:
   - Menu: Build > Build Solution
   - Shortcut: `Ctrl+Shift+B`

### 2. Using Command Line

#### Prerequisites
- .NET 8.0 SDK installed

#### Build Commands

```powershell
# Build solution in Debug configuration
msbuild BerryAIGen.sln /p:Configuration=Debug

# Build solution in Release configuration
msbuild BerryAIGen.sln /p:Configuration=Release

# Build specific project
msbuild BerryAIGen.Toolkit/BerryAIGen.Toolkit.csproj /p:Configuration=Release

# Clean and build
msbuild BerryAIGen.sln /t:Clean,Build /p:Configuration=Release

# Build with dotnet CLI (recommended)
dotnet build BerryAIGen.sln --configuration Release
dotnet build BerryAIGen.Toolkit --configuration Release
```

### 3. Build Output

Build artifacts are generated in the `bin` directory of each project:

```
ProjectName/
鈹斺攢鈹€ bin/
    鈹溾攢鈹€ Debug/
    鈹?  鈹斺攢鈹€ net8.0-windows/  # Debug build artifacts
    鈹斺攢鈹€ Release/
        鈹斺攢鈹€ net8.0-windows/  # Release build artifacts
            鈹溾攢鈹€ ProjectName.dll
            鈹溾攢鈹€ ProjectName.exe (for executable projects)
            鈹溾攢鈹€ dependencies/  # Runtime dependencies
            鈹斺攢鈹€ publish/  # Published application (when using dotnet publish)
```

## Publishing the Application

### 1. Using Visual Studio

1. Right-click on the `BerryAIGen.Toolkit` project
2. Select "Publish..."
3. Choose the publish target (Folder, MSIX, etc.)
4. Configure publish settings
5. Click "Publish"

### 2. Using Command Line

```powershell
# Publish for Windows x64
dotnet publish BerryAIGen.Toolkit --configuration Release --runtime win-x64 --self-contained true

# Publish for Windows x86
dotnet publish BerryAIGen.Toolkit --configuration Release --runtime win-x86 --self-contained true

# Publish with single-file deployment
dotnet publish BerryAIGen.Toolkit --configuration Release --runtime win-x64 --self-contained true /p:PublishSingleFile=true

# Publish with trimmed dependencies (reduced size)
dotnet publish BerryAIGen.Toolkit --configuration Release --runtime win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
```

### 3. Publish Output

Published artifacts are generated in the `bin/Release/net8.0-windows/{runtime}/publish/` directory:

- **Single-file executable**: `BerryAIGen.Toolkit.exe`
- **Dependencies**: All required libraries and resources
- **Configuration files**: App settings and configuration
- **Localization files**: Satellite assemblies for different languages

## CI/CD Pipeline

### 1. Pipeline Overview

The project uses GitHub Actions for CI/CD, with the following workflow:

1. **Trigger**: Push to main branch or pull request creation
2. **Build**: Compile code and run tests
3. **Test**: Execute unit and integration tests
4. **Package**: Create deployment packages
5. **Deploy**: Publish releases (for tagged commits)

### 2. Workflow Configuration

The CI/CD pipeline is defined in `.github/workflows/build.yml`:

```yaml
name: Build and Test

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
    
    - name: Restore dependencies
      run: dotnet restore BerryAIGen.sln
    
    - name: Build
      run: dotnet build BerryAIGen.sln --configuration Release --no-restore
    
    - name: Test
      run: dotnet test TestHarness/TestHarness.csproj --configuration Release --no-build --verbosity normal
    
    - name: Publish
      run: dotnet publish BerryAIGen.Toolkit/BerryAIGen.Toolkit.csproj --configuration Release --runtime win-x64 --self-contained true /p:PublishSingleFile=true
    
    - name: Upload artifact
      uses: actions/upload-artifact@v3
      with:
        name: BerryAIGen-Toolkit
        path: BerryAIGen.Toolkit/bin/Release/net8.0-windows/win-x64/publish/
```

### 3. Release Management

For tagged commits, an additional workflow creates GitHub Releases:

```yaml
name: Create Release

on:
  push:
    tags: [ 'v*.*.*' ]

jobs:
  release:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
    
    - name: Build and Publish
      run: |
        dotnet restore BerryAIGen.sln
        dotnet build BerryAIGen.sln --configuration Release
        dotnet publish BerryAIGen.Toolkit/BerryAIGen.Toolkit.csproj --configuration Release --runtime win-x64 --self-contained true /p:PublishSingleFile=true
    
    - name: Create ZIP archive
      run: Compress-Archive -Path BerryAIGen.Toolkit/bin/Release/net8.0-windows/win-x64/publish/* -DestinationPath BerryAIGen-Toolkit.zip
    
    - name: Create Release
      uses: actions/create-release@v1
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        tag_name: ${{ github.ref }}
        release_name: Release ${{ github.ref }}
        draft: false
        prerelease: false
    
    - name: Upload Release Asset
      uses: actions/upload-release-asset@v1
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      with:
        upload_url: ${{ steps.create_release.outputs.upload_url }}
        asset_path: ./BerryAIGen-Toolkit.zip
        asset_name: BerryAIGen-Toolkit.zip
        asset_content_type: application/zip
```

## Code Quality Checks

### 1. Static Analysis

The project uses the following static analysis tools:

- **Roslyn Analyzers**: Built-in .NET analyzers for code quality
- **StyleCop**: Enforces coding style and conventions
- **SonarQube**: Continuous code quality monitoring (optional)

### 2. Code Coverage

Code coverage is measured using Coverlet and reported in the CI/CD pipeline:

```powershell
# Run tests with coverage
dotnet test TestHarness/TestHarness.csproj --configuration Release /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

# Generate coverage report
reportgenerator -reports:TestHarness/coverage.info -targetdir:coverage-report
```

## Dependencies Management

### 1. Package Management

The project uses NuGet for dependency management with the following practices:

- **Central Package Management**: Dependencies are managed in a central `Directory.Packages.props` file
- **Locked Dependencies**: Use `packages.lock.json` to ensure reproducible builds
- **Version Pinning**: Explicit version pinning for all dependencies
- **Regular Updates**: Dependencies are updated regularly to ensure security and compatibility

### 2. Key Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| Dapper | 2.0.123 | Database access |
| SQLitePCLRaw.bundle_green | 2.1.6 | SQLite database |
| Newtonsoft.Json | 13.0.3 | JSON serialization |
| Moq | 4.18.4 | Mocking for testing |
| NUnit | 3.13.3 | Test framework |
| FontAwesome.WPF | 4.7.0.9 | Icon library |
| WPFLocalizeExtension | 4.0.0 | Localization support |

## Build Optimization

### 1. Incremental Builds

MSBuild uses incremental builds to speed up the build process by only rebuilding changed files and their dependencies.

### 2. Parallel Builds

The solution is configured to build projects in parallel:

```xml
<!-- In Directory.Build.props -->
<PropertyGroup>
  <BuildInParallel>true</BuildInParallel>
  <MaxCpuCount>0</MaxCpuCount> <!-- Use all available cores -->
</PropertyGroup>
```

### 3. Build Caching

The CI/CD pipeline uses caching for NuGet packages and build outputs:

```yaml
- name: Cache NuGet packages
  uses: actions/cache@v3
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/packages.lock.json') }}
    restore-keys: |
      ${{ runner.os }}-nuget-

- name: Cache build outputs
  uses: actions/cache@v3
  with:
    path: |
      **/bin
      **/obj
    key: ${{ runner.os }}-build-${{ github.sha }}
    restore-keys: |
      ${{ runner.os }}-build-
```

## Troubleshooting Build Issues

### 1. Common Build Errors

| Error | Cause | Solution |
|-------|-------|----------|
| NuGet package restore failed | Network issues or missing packages | Run `dotnet restore` manually |
| Build timeout | Complex build or CI/CD resource constraints | Simplify build process or increase timeout |
| Missing dependencies | Package version conflicts or missing references | Check package versions and references |
| Code quality violations | StyleCop or Roslyn analyzer errors | Fix the reported code issues |
| Test failures | Breaking changes or failing tests | Fix the failing tests |

### 2. Build Logs

Build logs provide detailed information about the build process:

- **Visual Studio**: View build output in the Output window
- **Command Line**: Use `-v d` (detailed) or `-v diag` (diagnostic) flags
- **CI/CD**: Check the build logs in GitHub Actions

## Best Practices

### 1. Build Configuration

- Keep build configurations simple and consistent
- Use separate configurations for debug and release
- Avoid hard-coded paths and values
- Use environment variables for configuration

### 2. CI/CD Pipeline

- Run tests on every commit
- Build and test on multiple platforms if supported
- Automate releases for tagged commits
- Monitor build status and fix failures promptly

### 3. Dependencies

- Keep dependencies to a minimum
- Use trusted, well-maintained packages
- Update dependencies regularly
- Lock dependencies for reproducible builds

### 4. Code Quality

- Enforce code quality rules
- Measure and track code coverage
- Fix issues promptly
- Use static analysis tools

## Conclusion

The BerryAIGen.Toolkit project uses a modern, automated build process that ensures consistent, high-quality builds. By following the practices outlined in this document, developers can maintain a reliable build system that supports efficient development and deployment. The CI/CD pipeline provides automatic validation of all code changes, helping to catch issues early and ensure a stable, production-ready application.
