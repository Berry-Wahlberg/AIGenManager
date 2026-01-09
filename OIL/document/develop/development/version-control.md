# Version Control

## Overview

The BerryAIGen.Toolkit project uses Git for version control, with GitHub as the remote repository hosting service. This document outlines the version control workflow, branching strategy, commit conventions, and best practices for collaborating on the project.

## Repository Structure

### 1. Remote Repository

The main remote repository is located at:
- **GitHub URL**: `https://github.com/Berry-Wahlberg/AIGenManager.git`

### 2. Repository Organization

```
.github/
鈹溾攢鈹€ workflows/        # CI/CD workflows
鈹溾攢鈹€ ISSUE_TEMPLATE/   # Issue templates
鈹斺攢鈹€ PULL_REQUEST_TEMPLATE.md # Pull request template

BerryAIGen.Civitai/   # Civitai API integration
BerryAIGen.Common/    # Common utilities
BerryAIGen.Database/  # Database access layer
BerryAIGen.Github/    # GitHub API integration
BerryAIGen.IO/        # File I/O operations
BerryAIGen.Scripting/ # Scripting support
BerryAIGen.Toolkit/   # Main WPF application
BerryAIGen.Updater/   # Application updater
BerryAIGen.Video/     # Video support

TestBed/              # Test application
TestHarness/          # Unit and integration tests

BerryAIGen.sln        # Main solution file
.gitignore            # Git ignore rules
```

## Branching Strategy

The project uses a simplified Git Flow branching strategy:

### 1. Main Branches

#### `main`
- **Purpose**: Production-ready code
- **Status**: Protected branch (requires pull request for changes)
- **CI/CD**: Automatically builds and deploys releases
- **Stability**: Always deployable

#### `develop`
- **Purpose**: Integration branch for features
- **Status**: Protected branch (requires pull request for changes)
- **CI/CD**: Automatically builds and runs tests
- **Stability**: Should be stable but not necessarily production-ready

### 2. Supporting Branches

#### Feature Branches
- **Purpose**: Development of new features
- **Naming**: `feature/feature-name` (e.g., `feature/metadata-extraction`)
- **Source**: `develop`
- **Target**: `develop`
- **Lifecycle**: Created when work starts, deleted after merging

#### Bugfix Branches
- **Purpose**: Fixing bugs in production code
- **Naming**: `bugfix/bug-description` (e.g., `bugfix/search-issue`)
- **Source**: `main` or `develop` depending on bug location
- **Target**: `main` and `develop`
- **Lifecycle**: Created when bug is identified, deleted after merging

#### Hotfix Branches
- **Purpose**: Urgent fixes for production issues
- **Naming**: `hotfix/hotfix-description` (e.g., `hotfix/security-vulnerability`)
- **Source**: `main`
- **Target**: `main` and `develop`
- **Lifecycle**: Created for urgent issues, deleted after merging

#### Release Branches
- **Purpose**: Preparing for release
- **Naming**: `release/vX.Y.Z` (e.g., `release/v1.2.0`)
- **Source**: `develop`
- **Target**: `main` and `develop`
- **Lifecycle**: Created when preparing for release, deleted after merging

## Branching Workflow

### 1. Feature Development

1. **Create Feature Branch**: From `develop`
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/feature-name
   ```

2. **Implement Feature**: Make changes and commit regularly

3. **Sync with Develop**: Regularly merge `develop` into feature branch to stay up-to-date
   ```bash
   git checkout feature/feature-name
   git merge develop
   ```

4. **Create Pull Request**: When feature is complete, create PR from feature branch to `develop`

5. **Code Review**: Team reviews the PR

6. **Merge**: After approval, merge PR into `develop` and delete feature branch

### 2. Bugfix Workflow

1. **Create Bugfix Branch**: From `main` or `develop`
   ```bash
   git checkout main
   git pull origin main
   git checkout -b bugfix/bug-description
   ```

2. **Fix Bug**: Make changes and commit

3. **Test**: Verify bug is fixed

4. **Create Pull Request**: Create PR to `main` and `develop`

5. **Code Review**: Team reviews the PR

6. **Merge**: After approval, merge PR into both branches and delete bugfix branch

### 3. Release Workflow

1. **Create Release Branch**: From `develop`
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b release/vX.Y.Z
   ```

2. **Finalize Release**: 
   - Update version numbers
   - Update CHANGELOG.md
   - Run final tests

3. **Create Pull Requests**: Create PRs to both `main` and `develop`

4. **Code Review**: Team reviews the release PRs

5. **Merge**: After approval, merge PRs into both branches

6. **Tag Release**: Create a tag on `main` for the new release
   ```bash
   git checkout main
   git pull origin main
   git tag -a vX.Y.Z -m "Release vX.Y.Z"
   git push origin vX.Y.Z
   ```

7. **Delete Release Branch**: Release branch is no longer needed

## Commit Conventions

### 1. Commit Message Structure

```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

### 2. Type

The commit type must be one of the following:

| Type | Description |
|------|-------------|
| **feat** | New feature |
| **fix** | Bug fix |
| **docs** | Documentation changes |
| **style** | Code style changes (formatting, etc.) |
| **refactor** | Code refactoring (no functional changes) |
| **perf** | Performance improvements |
| **test** | Testing changes |
| **build** | Build system or dependency changes |
| **ci** | CI/CD configuration changes |
| **chore** | Other changes (e.g., maintenance tasks) |

### 3. Scope

The scope should describe the affected component or module:

```
feat(thumbnail-service): add batch thumbnail generation
fix(search-service): correct model search logic
docs(ui-ux): update theming documentation
```

### 4. Description

- Short summary of the change (max 50 characters)
- Written in imperative mood (e.g., "Add feature" not "Added feature")
- First letter lowercase
- No trailing period

### 5. Body (Optional)

- More detailed explanation of the change
- Wrap at 72 characters
- Explain what and why, not how

### 6. Footer (Optional)

- Used for breaking changes and issue references
- Breaking changes: `BREAKING CHANGE: description`
- Issue references: `Closes #123`, `Fixes #456`, `Resolves #789`

### 7. Examples

```
# Simple commit
feat(metadata-scanner): add continuous scanning mode

# Commit with body and footer
fix(database): resolve deadlock in query execution

The fix ensures proper transaction handling when multiple queries are 
executed simultaneously, preventing deadlocks that were occurring 
during high-load scenarios.

Closes #246
```

## Pull Request Process

### 1. Creating a Pull Request

1. **Create a Draft PR**: For early feedback on work in progress
2. **Complete the PR Template**: Fill in all required sections
3. **Provide Description**: Explain what the PR does, why it's needed, and how to test it
4. **Add Reviewers**: Assign appropriate team members for review
5. **Link Issues**: Reference any related issues

### 2. PR Template

The PR template includes sections for:
- **Description**: What the PR does
- **Related Issues**: Links to GitHub issues
- **Testing**: How the changes were tested
- **Breaking Changes**: Any breaking changes introduced
- **Screenshots**: For UI changes

### 3. Code Review Process

1. **Reviewers Assigned**: PR is assigned to 1-2 reviewers
2. **Review Comments**: Reviewers provide feedback
3. **Address Comments**: Author addresses all comments
4. **Approval**: PR is approved by reviewers
5. **Merge**: PR is merged into target branch

### 4. Merge Strategies

- **Squash Merge**: Used for feature branches to keep commit history clean
- **Rebase Merge**: Used for hotfixes and release branches to maintain linear history
- **Merge Commit**: Used sparingly for complex merges

## Best Practices

### 1. Commit Best Practices

- **Small Commits**: Each commit should address a single issue or feature
- **Atomic Commits**: Each commit should be a complete, working change
- **Meaningful Messages**: Use clear, descriptive commit messages
- **Test Before Committing**: Ensure all tests pass before committing
- **Keep Commit History Clean**: Avoid unnecessary commits and merge conflicts

### 2. Branch Management

- **Delete Stale Branches**: Delete branches after they're merged
- **Keep Branches Short-Lived**: Merge branches into `develop` regularly
- **Sync Regularly**: Keep feature branches up-to-date with `develop`
- **Use Descriptive Branch Names**: Clear, concise branch names

### 3. Collaboration

- **Communicate**: Discuss major changes with the team before implementing
- **Review Code**: Review PRs promptly and provide constructive feedback
- **Respect Protected Branches**: Follow the pull request process for protected branches
- **Document Changes**: Update documentation for any changes

### 4. Conflict Resolution

- **Pull Frequently**: Pull changes from the target branch regularly to minimize conflicts
- **Resolve Conflicts Locally**: Resolve conflicts on your local branch before pushing
- **Test After Resolving**: Ensure all tests pass after resolving conflicts
- **Use Merge Tools**: Use Git merge tools to resolve complex conflicts

## Versioning

### 1. Semantic Versioning

The project uses Semantic Versioning (SemVer) with the format:
```
MAJOR.MINOR.PATCH
```

- **MAJOR**: Breaking changes
- **MINOR**: New features, backwards compatible
- **PATCH**: Bug fixes, backwards compatible

### 2. Version File Location

Version information is stored in:
- **Project Files**: `.csproj` files contain the assembly version
- **CHANGELOG.md**: Contains release notes and version history

### 3. Version Update Process

1. Update version numbers in all `.csproj` files
2. Update `CHANGELOG.md` with release notes
3. Commit changes with message `build: bump version to vX.Y.Z`
4. Create a tag for the release

## CHANGELOG Management

### 1. CHANGELOG Format

The CHANGELOG.md file follows the Keep a Changelog format:

```markdown
# Changelog

## [Unreleased]
- [List of pending changes]

## [1.2.0] - 2026-01-06
### Added
- Feature 1
- Feature 2

### Changed
- Change 1

### Fixed
- Bug 1
- Bug 2

## [1.1.0] - 2025-12-20
### Added
- Feature A

### Fixed
- Bug X
```

### 2. Updating CHANGELOG

- Add changes to the "Unreleased" section as they are implemented
- When creating a release, move "Unreleased" changes to a new version section
- Include the release date
- Link to GitHub release for each version

## Git Ignore Rules

The project uses a comprehensive `.gitignore` file that excludes:
- Build outputs (`bin/`, `obj/`)
- IDE-specific files (`*.suo`, `*.user`, `.vs/`)
- OS-specific files (`Thumbs.db`, `.DS_Store`)
- Packages directory (`packages/`)
- Log files (`*.log`)
- Test outputs (`TestResults/`)

## Git Hooks

The project may use Git hooks for:
- **Pre-commit**: Running linters and basic checks
- **Pre-push**: Running tests before pushing
- **Commit-msg**: Validating commit messages

## Tools and Utilities

### 1. Git Clients

Recommended Git clients:
- **Visual Studio Git Integration**: Built-in Git support in Visual Studio
- **GitHub Desktop**: User-friendly Git client
- **GitKraken**: Advanced Git client with visualization
- **Command Line**: Git CLI for full control

### 2. GitHub Features

- **Issues**: For bug tracking and feature requests
- **Projects**: For project management and tracking
- **Discussions**: For open discussions and Q&A
- **Actions**: For CI/CD workflows
- **Security**: For vulnerability scanning and alerts

## Conclusion

Following these version control practices ensures a clean, organized codebase and smooth collaboration between team members. By using a consistent branching strategy, commit conventions, and pull request process, the project can maintain a high level of code quality and stability while supporting efficient development workflows.
