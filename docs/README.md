# AIGenManager Electron.NET Implementation

## Overview

This documentation provides comprehensive information about the AIGenManager Electron.NET implementation, including architecture, UI design, localization, and development guidelines.

## Features

- **Responsive Navigation Bar**: Collapses automatically based on window width (800px threshold)
- **Modern UI Design**: Clean, professional interface with smooth transitions
- **Localization Support**: Multi-language support with client-side implementation
- **Configurable Window Size**: Defaults to 1280x800px for consistent user experience
- **24x24px Zoom Button**: Standardized button size with proper spacing

## Project Structure

```
BerryAIGen.Electron/
├── Pages/
│   ├── Index.cshtml          # Main application page
│   └── Shared/
│       └── _Layout.cshtml    # Main layout file
├── wwwroot/
│   ├── css/
│   │   └── site.css         # Main stylesheet
│   ├── js/
│   │   ├── navigation.js    # Navigation collapse logic
│   │   └── localization.js  # Localization manager
│   └── lang/
│       └── en.json          # English translations
├── Program.cs               # Electron.NET configuration
└── BerryAIGen.Electron.csproj # Project file
```

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- Node.js and npm (for Electron)
- Electron.NET CLI

### Installation

1. Install the Electron.NET CLI globally:
   ```bash
   dotnet tool install ElectronNET.CLI -g
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Start the application in development mode:
   ```bash
   electronize start
   ```

## Architecture

### Main Components

1. **Electron Main Process**: Handles window management, app lifecycle, and native OS interactions
2. **Electron Renderer Process**: Renders the web-based UI using ASP.NET Core Razor Pages
3. **Navigation System**: Responsive sidebar with automatic collapse/expand logic
4. **Localization System**: Client-side internationalization with JSON language files

### Core Technologies

- **Electron.NET**: Bridge between .NET and Electron
- **ASP.NET Core**: Web framework for UI rendering
- **CSS3**: Modern styling with flexbox and grid layouts
- **JavaScript (ES6+)**: Client-side functionality

## UI/UX Design

### Navigation Bar

- **Default Width**: 240px (expanded), 50px (collapsed)
- **Collapse Threshold**: 800px window width
- **Transition Effect**: Smooth 0.3s width transition
- **Toggle Button**: Located in sidebar header for manual control

### Window Dimensions

- **Default Size**: 1280x800px
- **Resizable**: Yes, with grip handle
- **Menu Bar**: Auto-hidden for more screen space

### Button Design

- **Zoom Button**: 24x24px with blue background
- **Toolbar Buttons**: 36x36px with light gray border
- **Hover Effects**: Subtle color changes and scaling
- **Active States**: Clear visual feedback for active navigation items

## Localization

### Implementation

- **Client-side**: JavaScript-based localization manager
- **Language Files**: JSON format stored in `/wwwroot/lang/`
- **Default Language**: English (en)
- **Key Format**: Dot-separated paths (e.g., `navigation.title`)
- **Fallback**: Returns original key if translation not found

### Adding New Languages

1. Create a new JSON file in `/wwwroot/lang/` (e.g., `es.json` for Spanish)
2. Copy the structure from `en.json`
3. Translate all values to the target language
4. Update the localization manager to support the new language

### Using Translations

Add the `data-i18n` attribute to HTML elements:

```html
<h3 data-i18n="navigation.title">Navigation</h3>
```

## Development Guidelines

### Coding Standards

- Use English for all code, comments, and documentation
- Follow consistent naming conventions
- Keep CSS organized by component
- Use semantic HTML5 elements
- Write modular, maintainable JavaScript

### Testing

- Test responsive behavior across different screen sizes
- Verify localization works correctly
- Test window resizing and navigation collapse
- Ensure all buttons and controls function properly

### Build and Deployment

- **Development**: `electronize start`
- **Build**: `electronize build /target win`
- **Publish**: `electronize publish /target win`

## Troubleshooting

### Common Issues

1. **Application not starting**: Check that all dependencies are installed
2. **Navigation not collapsing**: Verify window width thresholds in CSS
3. **Localization not working**: Check JSON file structure and key names
4. **UI elements misaligned**: Review CSS flexbox/grid layouts

## Contributing

Please follow the development guidelines when contributing to this project. Ensure all changes are thoroughly tested and documented.

## License

This project is licensed under the MIT License.
