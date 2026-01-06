# Theming System

## Overview
The BerryAIGen.Toolkit application implements a flexible theming system that supports both light and dark themes, with automatic detection based on system settings. This document describes the theming architecture, how themes are implemented, and how to extend or modify themes.

## Core Implementation

### Location
The theming system implementation is located in `BerryAIGen.Toolkit/Themes/` directory.

### Key Components

#### 1. Theme Files
- **Common.xaml**: Shared theme resources across all themes
- **Dark.xaml**: Dark theme definition
- **Light.xaml**: Light theme definition
- **Menu.xaml**: Menu-specific theme resources
- **SWStyles.xaml**: Additional styles
- **Scrollbars.xaml**: Scrollbar styles
- **ToolTips.xaml**: Tooltip styles
- **Window.xaml**: Window-specific styles

#### 2. ThemeManager
The `ThemeManager` class manages theme switching and application:

```csharp
public static class ThemeManager
{
    public static void ChangeTheme(string theme)
    {
        // Implementation for changing themes
    }
    
    public static string GetSystemTheme()
    {
        // Implementation for detecting system theme
    }
    
    // Additional theme management methods
}
```

## Theme Architecture

### Theme Structure
Each theme file follows a consistent structure:

1. **Resource Dictionary**: Root element containing all theme resources
2. **Brushes**: Color definitions for various UI elements
3. **Styles**: Control-specific styles
4. **Templates**: Control templates
5. **Animations**: Theme-specific animations

### Common Resources
The `Common.xaml` file contains shared resources that are used across all themes:

- **Font Definitions**: Font families, sizes, and weights
- **Common Brushes**: Brushes used in multiple themes
- **Base Styles**: Base styles that are inherited by theme-specific styles
- **Control Templates**: Shared control templates

### Theme Files

#### Dark Theme (Dark.xaml)
- **Color Palette**: Dark backgrounds with light text
- **Accent Colors**: Blue accents for interactive elements
- **Contrast**: High contrast for improved readability
- **Visual Style**: Modern, flat design

#### Light Theme (Light.xaml)
- **Color Palette**: Light backgrounds with dark text
- **Accent Colors**: Blue accents for consistency with dark theme
- **Contrast**: Balanced contrast for comfortable viewing
- **Visual Style**: Clean, bright design

## Theme Switching

### Automatic Theme Detection
- The application automatically detects the system theme on startup
- It listens for system theme changes and updates the application theme accordingly
- This behavior can be overridden by the user in settings

### Manual Theme Selection
- Users can manually select a theme in the settings
- Available options: System, Light, Dark
- Theme changes are applied immediately without restarting the application

### Theme Application Process
1. **Theme Detection**: Determine the current theme (system or user-selected)
2. **Resource Dictionary Management**: Load the appropriate theme files
3. **Resource Merging**: Merge theme resources with application resources
4. **Style Application**: Apply styles to all UI elements
5. **Dynamic Updates**: Handle runtime theme changes

## Extending Themes

### Adding New Theme Resources

1. **Identify the Resource Type**: Determine if you need to add a brush, style, or template
2. **Choose the Appropriate File**: Add the resource to the appropriate theme file
3. **Follow Naming Conventions**: Use consistent naming for resources
4. **Test Across Themes**: Ensure the resource works correctly in all themes

### Adding a New Theme

1. **Create Theme File**: Create a new XAML file for the theme
2. **Define Color Palette**: Define the color palette for the new theme
3. **Inherit from Common**: Include the Common.xaml resource dictionary
4. **Define Styles**: Define styles for all UI elements
5. **Update ThemeManager**: Add support for the new theme in ThemeManager
6. **Update Settings**: Add the new theme to the settings options

## Theme Resources

### 5-Level Grayscale System
The application implements a 5-level grayscale system that provides consistent visual hierarchy across all UI elements:

#### Light Theme Grayscale
```xaml
<Color x:Key="White">#FFFFFF</Color>
<Color x:Key="Gray1">#F5F5F5</Color>
<Color x:Key="Gray2">#E0E0E0</Color>
<Color x:Key="Gray3">#9E9E9E</Color>
<Color x:Key="Gray4">#616161</Color>
```

#### Dark Theme Grayscale
```xaml
<Color x:Key="White">#FFFFFF</Color>
<Color x:Key="Gray1">#3C3C3C</Color>
<Color x:Key="Gray2">#2D2D30</Color>
<Color x:Key="Gray3">#666666</Color>
<Color x:Key="Gray4">#CCCCCC</Color>
```

### Core Colors
All themes use a consistent accent color system:

```xaml
<!-- Accent Colors -->
<Color x:Key="Accent">#2196F3</Color>
<Color x:Key="AccentLight">#64B5F6</Color>
<Color x:Key="AccentDark">#1976D2</Color>
```

### Brushes
Brushes are defined using XAML resources and referenced by name throughout the application:

```xaml
<!-- Example brush definition -->
<SolidColorBrush x:Key="BackgroundBrush" Color="{DynamicResource White}" />
<SolidColorBrush x:Key="ForegroundBrush" Color="{DynamicResource Gray4}" />
<SolidColorBrush x:Key="AccentBrush" Color="{DynamicResource Accent}" />
```

### Styles
Styles are defined for all major UI controls:

```xaml
<!-- Example button style -->
<Style TargetType="Button">
    <Setter Property="Background" Value="{StaticResource ButtonBackgroundBrush}" />
    <Setter Property="Foreground" Value="{StaticResource ButtonForegroundBrush}" />
    <Setter Property="BorderBrush" Value="{StaticResource ButtonBorderBrush}" />
    <Setter Property="Padding" Value="8,4" />
    <Setter Property="Margin" Value="2" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <!-- Button template implementation -->
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### Control Templates
Custom control templates are defined for many UI controls to ensure consistent styling across themes:

```xaml
<!-- Example scrollbar template -->
<ControlTemplate TargetType="ScrollBar">
    <Grid>
        <Border Background="{StaticResource ScrollBarBackgroundBrush}" />
        <Track x:Name="PART_Track">
            <Track.Thumb>
                <Thumb Style="{StaticResource ScrollBarThumbStyle}" />
            </Track.Thumb>
            <Track.IncreaseRepeatButton>
                <RepeatButton Style="{StaticResource ScrollBarIncreaseButtonStyle}" />
            </Track.IncreaseRepeatButton>
            <Track.DecreaseRepeatButton>
                <RepeatButton Style="{StaticResource ScrollBarDecreaseButtonStyle}" />
            </Track.DecreaseRepeatButton>
        </Track>
    </Grid>
</ControlTemplate>
```

## Best Practices for Theming

### 1. Use Theme Resources
- Always use theme resources instead of hard-coded values
- Reference brushes and styles by their resource keys
- Avoid inline styles that bypass the theming system

### 2. Maintain Consistency
- Keep styles consistent across all controls
- Use the same naming conventions for resources
- Ensure similar controls have similar visual styles

### 3. Support Both Themes
- Test all UI elements in both light and dark themes
- Ensure contrast is appropriate for both themes
- Avoid theme-specific assumptions in code

### 4. Follow WPF Best Practices
- Use dynamic resources for theme-dependent values
- Use static resources for theme-independent values
- Implement proper inheritance for styles
- Use control templates appropriately

### 5. Optimize Performance
- Avoid overly complex styles and templates
- Use lightweight animations
- Minimize the number of resources in each theme file

## Theme Detection

### System Theme Detection
The application uses Windows API calls to detect the system theme:

```csharp
public static string GetSystemTheme()
{
    // Use Windows API to detect system theme
    var theme = NativeMethods.GetCurrentTheme();
    return theme == "dark" ? "Dark" : "Light";
}
```

### Theme Change Events
The application listens for system theme changes and updates the UI accordingly:

```csharp
SystemEvents.UserPreferenceChanged += (sender, e) =>
{
    if (e.Category == UserPreferenceCategory.Color)
    {
        UpdateTheme(_settings.Theme);
    }
};
```

## Theme Customization

### User Theme Settings
Users can customize the theme in the settings menu:

- **Theme Option**: Dropdown to select System, Light, or Dark theme
- **Automatic Theme Switching**: Toggle for enabling/disabling automatic theme detection
- **Instant Apply**: Theme changes are applied immediately

### Runtime Theme Changes
When a theme is changed at runtime:

1. The old theme resources are removed from the application
2. The new theme resources are added
3. UI elements automatically update to use the new resources
4. The theme setting is saved to persistent storage

## Conclusion

The theming system in BerryAIGen.Toolkit provides a flexible and extensible architecture for supporting multiple themes. It follows modern UI design principles with support for both light and dark themes, automatic system theme detection, and runtime theme switching. The consistent structure and clear separation of concerns make it easy to extend or modify themes as needed.

