# BerryAIGen.Toolkit

BerryAIGen.Toolkit is a comprehensive image metadata indexer and viewer designed specifically for AI-generated images. It provides powerful tools to help you organize, search, and manage your growing collection of AI-generated media.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Installation](#installation)
- [Building from Source](#building-from-source)
- [Usage](#usage)
- [Supported Formats](#supported-formats)
- [Supported Metadata Formats](#supported-metadata-formats)
- [Screenshots](#screenshots)
- [FAQ](#faq)
- [Development](#development)

## Overview

BerryAIGen.Toolkit enables users to:
- Index and search metadata from AI-generated images
- Organize images with ratings, tags, and albums
- View detailed metadata (PNGInfo) in a user-friendly interface
- Search and analyze prompts across your collection
- Manage your image library with drag-and-drop functionality

## Features

### Metadata Management
- **Automated Scanning**: Scan images and videos to extract and index prompts and other metadata
- **Metadata Viewing**: Easily view detailed metadata in the Preview Pane
- **Search Capabilities**: Search through image metadata with powerful search filters

### Image Organization
- **Rating System**: Rate images from 1-10
- **Favorite Tagging**: Mark images as favorites
- **NSFW Support**: Auto-tag NSFW content by keywords and blur NSFW images
- **Custom Tags**: Create and apply custom tags to images
- **Albums**: Organize images into albums with support for drag-and-drop

### Prompt Analysis
- **Prompt Library**: View and search all prompts used in your collection
- **Usage Statistics**: See how often specific prompts are used
- **Negative Prompt Analysis**: Analyze negative prompts across your collection
- **Prompt-to-Image Mapping**: View all images associated with a specific prompt

### File Management
- **Folder View**: Navigate your image library with a familiar folder structure
- **Drag-and-Drop Support**: Drag images to move them (CTRL-drag to copy)
- **Right-Click Commands**: Move, copy, and manage images with context menus

## Installation

### System Requirements
- **Operating System**: Windows 10/11
- **Runtime**: [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (includes the .NET 8.0 Runtime)

### Download and Install
1. Download the latest release from the [GitHub Releases](https://github.com/RupertAvery/BerryAIGen.Toolkit/releases/latest) page
2. Expand the **Assets** section and download the zip file (e.g., `BerryAIGen.Toolkit.v1.x.zip`)
3. Extract all files to a folder of your choice
4. Run `BerryAIGen.Toolkit.exe` to launch the application

## Building from Source

### Prerequisites
- **IDE**: Visual Studio 2022 or later
- **SDK**: [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (includes the runtime)

### Build Instructions
1. Clone this repository
2. Open the solution file `BerryAIGen.sln` in Visual Studio
3. Build the solution using the **Build** menu or by pressing `Ctrl+Shift+B`
4. Alternatively, run `dotnet build` from the command line in the project root

### Publishing
- Run `publish.cmd` to create a release build in the `build` folder

## Usage

### Getting Started
- Upon first launch, you'll be prompted to select your preferred language
- Add folders containing your AI-generated images using the settings menu
- The application will automatically scan and index your images
- Use the search bar to find images by metadata, tags, or prompts

### Keyboard Shortcuts
- `I`: Toggle metadata visibility in the Preview Pane
- `Ctrl+F`: Focus the search bar
- `Arrow Keys`: Navigate between images

### Tips and Tricks
For more detailed usage information, see the [Development Documentation](document/develop/README.md) in the `document/develop` folder.

## Supported Formats

### Image Formats
- JPG/JPEG + EXIF
- PNG
- WebP
- MP4 (video)

### Metadata Formats
- **AUTOMATIC1111 and compatible**: Tensor.Art, SDNext
- **InvokeAI**: Dream/sd-metadata/invokeai_metadata
- **NovelAI**
- **Stable Diffusion**
- **EasyDiffusion**
- **Fooocus family**: RuinedFooocus, Fooocus, FooocusMRE
- **Stable Swarm**

### Text Metadata
- .TXT metadata files

## Supported Metadata Formats

BerryAIGen.Toolkit can extract metadata from various AI image generation platforms. Even images without metadata can be used with features like rating and albums!

## Screenshots

![Main Interface](https://github.com/Berry-Wahlberg/AIGenManager/assets/screenshots/main-interface.png)

![Metadata View](https://github.com/Berry-Wahlberg/AIGenManager/assets/screenshots/metadata-view.png)

## FAQ

### How do I view my image's metadata (PNGInfo)?
With the Preview Pane visible, press `I` in the thumbnail view or when the Preview Pane is in focus to show or hide the metadata. You can also click the eye icon at the bottom right of the Preview Pane.

### What is Rebuild Metadata and when should I use it?
Rebuild Metadata rescans all your images and updates the database with any new or updated metadata. It doesn't affect your custom tags (rating, favorite, NSFW). Use this feature when a new version of BerryAIGen.Toolkit adds support for metadata formats that exist in your images.

### Can I move my images to a different folder?
To move images within your BerryAIGen folders while preserving metadata, use the **right-click > Move** command. This ensures all Toolkit-specific metadata (Favorites, Ratings, NSFW tags) remains intact. Using Explorer or other applications to move files will result in lost metadata.

## Development

Comprehensive development documentation is available in the `document/develop` folder. This documentation includes:
- Technical specifications
- Architecture diagrams
- Implementation details
- Development workflows
- API documentation

