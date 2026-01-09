# Project Context: BerryAIGen.Toolkit

## Project Structure

```
AIGenManager/
├── src/
│   ├── Common/                    # Common utilities and helper classes
│   ├── Data/
│   │   ├── Database/              # Database-related functionality
│   │   └── IO/                    # Input/output operations
│   ├── Infrastructure/
│   │   ├── Civitai/               # Civitai API integration
│   │   ├── Github/                # GitHub API integration
│   │   └── Video/                 # Video processing capabilities
│   └── Presentation/
│       ├── Electron/              # Electron.NET GUI implementation
│       └── Wpf/                   # WPF GUI implementation
├── TestBed/                       # Test project for manual testing
├── TestHarness/                   # Automated test harness
├── BerryAIGen.Updater/            # Application updater component
├── BerryAIGen.Scripting/          # Scripting capabilities
├── .trae/                         # Trae AI assistant files and documentation
├── version.txt                    # Central version file
├── VERSIONING.md                  # Version management rules
├── build-electron-release.ps1     # Electron release build script
└── .gitignore                     # Git ignore rules
```

## Key Dependencies

### Core Frameworks
- **.NET 8.0**: Primary development framework
- **Electron.NET**: Cross-platform GUI framework for Electron implementation
- **WPF**: Windows Presentation Foundation for traditional Windows GUI

### Build Tools
- **PowerShell**: Scripting language for build automation
- **Git**: Version control system

### External Libraries
- **Civitai API**: For accessing AI models and resources
- **GitHub API**: For repository management and updates

## Development Environment

### Operating System
- Primary: Windows 10/11
- Cross-platform support: Linux, macOS via Electron.NET

### Development Tools
- **Visual Studio** or **Visual Studio Code**: Primary IDEs
- **PowerShell 5+**: For running build scripts
- **Node.js**: Required for Electron.NET development

## Versioning Strategy

### Semantic Versioning
The project follows strict Semantic Versioning (Major.Minor.Patch):
- **Major**: Breaking changes, major feature additions
- **Minor**: New features, non-breaking changes
- **Patch**: Bug fixes, minor improvements

### Version File Location
- Central version file: `version.txt` (single line containing version number)
- This file is read by the build script and referenced by other components

### Git Tagging Convention
- Tags follow the format: `v<Major>.<Minor>.<Patch>` (e.g., `v2.0.0`)
- Tags are created when a release is ready for deployment

## Build System

### Build Scripts
- **`build-electron-release.ps1`**: Main build script for Electron release packages
  - Performs dependency checking
  - Verifies version consistency
  - Manages flag files
  - Builds and packages the Electron application

### Compilation Output
- All compilation outputs are stored in respective `bin` directories within each project
- The build script creates a final release package in the `build` directory

## Flag File Management

### Required Flag Files
- **`Berry_ICO.ico`**: Application icon
- **`Berry_LOGO.png`**: Application logo

### Location
These files are stored in the `.trae` directory and are managed by the build script.

## Branch Management

### Main Branches
- **`main`**: Stable release branch
- **`feature/*`**: Feature development branches
- **`debug/*`**: Debug and fix branches

### Merge Strategy
- Feature branches are merged into `main` after completion
- Debug branches are merged into `main` after fixes are verified

## Key Features

### GUI Implementation
- **Electron.NET**: Modern, cross-platform GUI with web technologies
- **WPF**: Traditional Windows desktop GUI

### Core Functionality
- AI model management via Civitai integration
- Video processing capabilities
- GitHub repository management
- Application auto-updater

## Testing Strategy

### Test Projects
- **`TestBed`**: Manual testing project for interactive testing
- **`TestHarness`**: Automated testing framework

### Testing Approach
- Unit tests for core functionality
- Integration tests for API calls and external dependencies
- Manual testing for GUI components

## Release Process

1. Update version in `version.txt`
2. Verify version consistency across all files
3. Run full build and test suite
4. Create Git tag for the release
5. Package the application using the build script
6. Deploy the release package

## Documentation

### Technical Documentation
- Stored in `.trae/documents/` directory
- Includes implementation plans, fix documentation, and enhancement proposals

### User Documentation
- To be created for end-users
- Will include installation guides, usage instructions, and troubleshooting

---

**Date**: 2026-01-09
**Project**: BerryAIGen.Toolkit
**Purpose**: Provide context and background for development workhaha