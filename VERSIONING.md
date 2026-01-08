# Version Number Management Rules

## Semantic Versioning Standard

The software follows **Semantic Versioning (SemVer)** with the format:
```
v<Major>.<Minor>.<Patch>
```

### Version Components

| Component | Description | Update Rule | Example |
|-----------|-------------|-------------|---------|
| **Major** | Major functionality changes, breaking API changes, or major architectural changes | When introducing significant new features, breaking changes, or major rewrites | `v2.0.0` (new Electron.NET GUI replacing WPF) |
| **Minor** | New features, enhancements, or significant improvements that are backward compatible | When adding new features or enhancements without breaking changes | `v2.1.0` (added new model management features) |
| **Patch** | Bug fixes, small improvements, or minor updates | When fixing bugs, improving performance, or making minor changes | `v2.0.1` (fixed crash on startup) |

## Version Update Rules

### Update Flow

1. **Initial Development**: Start with `v0.1.0` for initial development
2. **Alpha/Beta Releases**: Use `-alpha` or `-beta` suffixes for pre-release versions (e.g., `v1.0.0-alpha.1`)
3. **Stable Release**: Remove pre-release suffix for stable versions (e.g., `v1.0.0`)
4. **Phase Completion**: Update the **Patch** version after each development phase is completed
5. **Feature Addition**: Update the **Minor** version when adding new features
6. **Major Changes**: Update the **Major** version when making breaking changes

### Version Update Process

1. **Update `version.txt`**: Update the version in the root `version.txt` file first
2. **Update Project Files**: Update version attributes in all `.csproj` files
3. **Update Documentation**: Update version references in documentation files
4. **Create Release Notes**: Create release notes for each version update
5. **Git Tag**: Create a Git tag with the new version

## Version Consistency Guidelines

### All Release Packages Must Have Consistent Versioning

- **version.txt**: Root file containing the canonical version
- **.csproj files**: AssemblyVersion, FileVersion, and Version properties must match
- **Git Tags**: Must follow the format `vX.Y.Z` (e.g., `v2.0.0`)
- **Release Notes**: Must be created for each version update
- **Build Scripts**: Must reference the correct version

### Version Check Points

- [ ] `version.txt` file exists and contains valid semantic version
- [ ] All `.csproj` files have matching version attributes
- [ ] Documentation references the current version
- [ ] Release notes exist for the current version
- [ ] Git tag exists for the current version

## Versioning in Code

### Version Retrieval

The software retrieves its version from the `version.txt` file using the `SemanticVersionHelper.GetLocalVersion()` method. This ensures a single source of truth for the version number.

### Version Display

- Display the full version string in the application title bar
- Include version information in the About dialog
- Log the version at application startup
- Include version in error reports

## Release Package Versioning

### Release Package Naming

Use the format:
```
BerryAIGen.Toolkit-v<Major>.<Minor>.<Patch>-<Platform>.<Extension>
```

Example:
```
BerryAIGen.Toolkit-v2.0.0-win-x64.zip
BerryAIGen.Toolkit-v2.0.0-linux-x64.tar.gz
BerryAIGen.Toolkit-v2.0.0-macos-x64.dmg
```

### Package Contents

Each release package must include:
- The main executable with embedded version information
- `version.txt` file with the exact version
- Release notes for the current version
- Documentation referencing the current version

## Git Branch and Tag Management

### Branch Naming for Version Updates

Use descriptive branch names for version-related changes:
```
feature/version-update-v<Major>.<Minor>
```

Example:
```
feature/version-update-v2.0
```

### Git Tags

Create an annotated Git tag for each release:
```bash
git tag -a vX.Y.Z -m "Release vX.Y.Z"
git push origin vX.Y.Z
```

## Versioning Best Practices

1. **Single Source of Truth**: Maintain version in one place (`version.txt`)
2. **Automate Where Possible**: Use build scripts to update version information
3. **Be Consistent**: Follow the same versioning scheme across all components
4. **Document Changes**: Always create release notes for version updates
5. **Test Thoroughly**: Test version-related functionality before release
6. **Follow SemVer**: Strictly adhere to semantic versioning rules
7. **Communicate Changes**: Clearly communicate version changes to users

## Version Rollback Policy

In case of critical issues:
1. Revert to the previous stable version
2. Create a new patch version for the previous major.minor release
3. Document the rollback in release notes
4. Communicate the rollback to users

## Example Version Lifecycle

```
v0.1.0 - Initial development
v0.2.0 - Added basic model management
v0.3.0 - Added image generation support
v1.0.0-alpha.1 - First alpha release
v1.0.0-beta.1 - First beta release
v1.0.0 - Stable release
v1.1.0 - Added new features
v1.1.1 - Fixed bug
v2.0.0 - Major rewrite with new GUI
```

## Versioning Tools

- **Build Script**: `build-electron-release.ps1` includes version validation
- **Version Helper**: `SemanticVersionHelper` class for version parsing and comparison
- **Git**: For branch management and tagging

## Compliance Check

Run the following command to verify version consistency:
```powershell
.uild-electron-release.ps1 -VerifyOnly
```

This will check:
- `version.txt` exists and is valid
- All `.csproj` files have matching versions
- Release notes exist for the current version

---

**Last Updated**: 2026-01-09
**Version**: v2.0.0