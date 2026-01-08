# BerryAIGen.Electron Module

## Overview

The BerryAIGen.Electron module implements a modern, web-based user interface using Electron.NET technology. It provides a responsive, cross-platform UI that replaces the traditional WPF interface, offering improved performance, better cross-platform support, and a more modern development stack.

## Architecture

### Core Technologies

| Technology | Version | Purpose |
|------------|---------|---------|
| Electron.NET | 23.6.2 | Bridge between .NET and Electron |
| ASP.NET Core | 10.0 | Web framework for UI rendering |
| CSS3 | - | Modern styling with flexbox and grid |
| JavaScript (ES6+) | - | Client-side functionality |

### Project Structure

```
BerryAIGen.Electron/
├── Pages/              # ASP.NET Razor Pages
│   ├── Index.cshtml    # Main application page
│   └── Shared/
│       └── _Layout.cshtml  # Layout template
├── wwwroot/            # Static assets
│   ├── css/            # Stylesheets
│   ├── js/             # JavaScript files
│   └── lang/           # Localization files
├── Program.cs          # Electron.NET configuration
└── appsettings.json    # Application settings
```

### Key Components

1. **Main Window**: Configured with 1280x800px default size, auto-hidden menu bar, and resizable grip handle
2. **Responsive Navigation**: Collapsible sidebar with 800px threshold (50px collapsed / 240px expanded)
3. **Localization System**: Client-side implementation with JSON language files
4. **Modern UI Components**: Styled with CSS3, featuring smooth animations and transitions

## UI/UX Features

### Navigation System

- **Auto-collapse**: Sidebar collapses when window width < 800px
- **Auto-expand**: Sidebar expands when window width > 800px
- **Manual toggle**: Button in sidebar header for user control
- **Smooth transitions**: 0.3s width animation

### Button Design

- **Zoom button**: 24x24px with blue background
- **Hover effects**: Subtle color changes and scaling animations
- **Consistent spacing**: Proper alignment with surrounding elements

### Window Management

- **Default size**: 1280x800px
- **Cross-platform consistency**: Same dimensions across operating systems
- **Auto-hidden menu bar**: More screen space for content
- **Resizable**: With grip handle for easy resizing

## Development

### Getting Started

1. Install Electron.NET CLI:
   ```bash
   dotnet tool install ElectronNET.CLI -g
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run in development mode:
   ```bash
   electronize start
   ```

### Build and Deployment

- **Development**: `electronize start`
- **Build**: `electronize build /target win`
- **Publish**: `electronize publish /target win`

### Development Guidelines

- Use English for all code, comments, and documentation
- Follow consistent naming conventions
- Keep CSS organized by component
- Use semantic HTML5 elements
- Write modular, maintainable JavaScript
- Test responsive behavior across different screen sizes

## Localization

### Implementation

- **Client-side manager**: `localization.js` handles translations
- **Language files**: JSON format in `/wwwroot/lang/`
- **Default language**: English (en)
- **Fallback mechanism**: Returns original key if translation not found

### Usage

```html
<h3 data-i18n="navigation.title">Navigation</h3>
```

### Adding New Languages

1. Create a new JSON file in `/wwwroot/lang/`
2. Copy the structure from `en.json`
3. Translate all values to the target language
4. No code changes required - the system automatically detects new languages

## Integration with Other Modules

The Electron.NET module interacts with other modules through the same service interfaces as the WPF implementation:

- **BerryAIGen.Common**: Core utilities and shared functionality
- **BerryAIGen.Database**: Data storage and retrieval
- **BerryAIGen.IO**: File system operations
- **BerryAIGen.Civitai**: Civitai API integration

## Performance Considerations

- **Rendering**: Uses Electron's Chromium engine for fast, modern UI rendering
- **Memory usage**: Optimized for low memory footprint
- **Startup time**: Fast initialization with pre-loaded resources
- **Responsiveness**: Smooth animations with 60fps rendering

## Migration from WPF

### Key Differences

| Feature | WPF | Electron.NET |
|---------|-----|--------------|
| Development Stack | XAML/C# | HTML/CSS/JavaScript/C# |
| Cross-Platform | Windows only | Windows, macOS, Linux |
| UI Performance | Good | Excellent |
| Modern UI Support | Limited | Excellent |
| Animation Support | Good | Excellent |
| Development Experience | Traditional .NET | Modern web + .NET |

### Benefits of Electron.NET

1. **Modern UI**: Access to latest web technologies and frameworks
2. **Cross-Platform**: Same codebase runs on Windows, macOS, and Linux
3. **Better Performance**: Hardware-accelerated rendering
4. **Larger Development Ecosystem**: Access to thousands of web libraries
5. **Easier Updates**: Hot reload support for faster development
6. **Better Responsive Design**: Built-in support for responsive layouts

## Future Enhancements

- [ ] Support for additional themes
- [ ] Enhanced keyboard shortcuts
- [ ] Better accessibility features
- [ ] Improved touch support
- [ ] Enhanced drag-and-drop functionality
- [ ] Support for custom UI themes

## Troubleshooting

### Common Issues

1. **Application not starting**: Check that all dependencies are installed
2. **Navigation not collapsing**: Verify CSS media queries in site.css
3. **Localization not working**: Check JSON file structure and key names
4. **Build errors**: Ensure Electron.NET CLI is installed globally
5. **Performance issues**: Check for memory leaks in JavaScript code

### Debugging

- **Browser DevTools**: Available through Electron's developer tools
- **Visual Studio**: Debugging support for .NET code
- **Console logs**: JavaScript console available for client-side debugging

## Conclusion

The BerryAIGen.Electron module represents a significant upgrade to the application's user interface, offering a modern, responsive design with better cross-platform support. Its web-based architecture allows for faster development, easier updates, and access to the latest UI technologies, while maintaining compatibility with the existing backend infrastructure.
