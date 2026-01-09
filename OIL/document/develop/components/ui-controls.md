# UI Controls Component

## Overview

The BerryAIGen.Toolkit application includes a variety of custom UI controls designed to provide an intuitive and efficient user experience for managing AI-generated images. These controls are built using WPF and follow the MVVM pattern, ensuring clean separation between UI and business logic.

## Core Controls

### 1. ThumbnailView

#### Purpose
The ThumbnailView control is the central UI component for displaying and interacting with image thumbnails. It provides a grid-based view of images with support for selection, navigation, and various image operations.

#### Location
`BerryAIGen.Toolkit/Controls/ThumbnailView.xaml.cs`

#### Key Features

- **Grid Layout**: Displays thumbnails in a responsive grid layout
- **Selection Support**: Multi-select, single-select, and keyboard navigation
- **Keyboard Shortcuts**: Comprehensive keyboard support for all operations
- **Drag and Drop**: Support for dragging images to other applications
- **Image Operations**: Favorite, rate, mark as NSFW, delete, and more
- **Pagination**: Support for large image collections with pagination
- **Thumbnail Sizes**: Configurable thumbnail sizes
- **View Modes**: Classic and Compact view modes

#### Usage

```xaml
<controls:ThumbnailView x:Name="ThumbnailView" />
```

#### Key Methods

- `SetThumbnailSize(int thumbnailSize)`: Sets the thumbnail size
- `FocusCurrentItem()`: Focuses the currently selected item
- `ShowItem(int index, bool focus)`: Shows and optionally focuses an item by index
- `ResetView(ReloadOptions? reloadOptions)`: Resets the view with optional reload options

### 2. Thumbnail

#### Purpose
The Thumbnail control represents a single image thumbnail, displaying the image, metadata, and interactive elements.

#### Location
`BerryAIGen.Toolkit/Controls/Thumbnail.cs`

#### Key Features

- **Image Display**: Shows the image thumbnail with loading state
- **Metadata Overlay**: Displays filename, size, and other metadata
- **Interactive Icons**: Favorite, NSFW, and rating icons
- **Hover Effects**: Visual feedback on hover
- **Context Menu**: Right-click menu with image operations

#### Usage

```xaml
<controls:Thumbnail DataContext="{Binding}" />
```

### 3. FilterControl

#### Purpose
The FilterControl provides a dynamic UI for creating and applying search filters to image collections.

#### Location
`BerryAIGen.Toolkit/Controls/FilterControl.xaml.cs`

#### Key Features

- **Dynamic Filter Creation**: Add, remove, and configure filters
- **Filter Types**: Support for various filter types (text, numeric, date, etc.)
- **Filter Combination**: AND/OR logic for combining filters
- **Search Integration**: Integrates with SearchService for immediate results
- **Save and Load**: Save and load filter configurations

#### Usage

```xaml
<controls:FilterControl Model="{Binding Filter}" />
```

### 4. MetadataPanel

#### Purpose
The MetadataPanel displays detailed metadata for the currently selected image, including generation parameters, prompts, and technical details.

#### Location
`BerryAIGen.Toolkit/Controls/MetadataPanel.xaml.cs`

#### Key Features

- **Generation Parameters**: Displays seed, steps, CFG scale, sampler, etc.
- **Prompt Display**: Shows positive and negative prompts
- **Technical Details**: File size, dimensions, hash, etc.
- **Copy Buttons**: Easy copying of individual metadata fields
- **Collapsible Sections**: Organized into collapsible sections for better readability

#### Usage

```xaml
<controls:MetadataPanel CurrentImage="{Binding CurrentImage}" />
```

### 5. StarRating

#### Purpose
The StarRating control allows users to rate images with a star-based rating system.

#### Location
`BerryAIGen.Toolkit/Controls/StarRating.xaml.cs`

#### Key Features

- **1-10 Rating Scale**: Supports ratings from 1 to 10 stars
- **Interactive**: Click to set rating, hover for preview
- **Visual Feedback**: Highlighted stars based on rating
- **Binding Support**: Two-way binding to rating property

#### Usage

```xaml
<controls:StarRating Rating="{Binding Rating}" />
```

### 6. MessagePopup

#### Purpose
The MessagePopup provides a customizable popup dialog for displaying messages to the user.

#### Location
`BerryAIGen.Toolkit/Controls/MessagePopup.xaml.cs`

#### Key Features

- **Customizable Buttons**: Support for different button combinations (OK, Yes/No, etc.)
- **Message Types**: Info, warning, error, confirmation
- **Modal and Non-Modal**: Support for both modal and non-modal dialogs
- **Async Support**: Async API for awaitable dialog results

#### Usage

```csharp
var result = await ServiceLocator.MessageService.Show("Message text", "Title", PopupButtons.YesNo);
```

### 7. AccordionControl

#### Purpose
The AccordionControl provides a collapsible panel with expandable/collapsible sections.

#### Location
`BerryAIGen.Toolkit/Controls/AccordionControl.xaml.cs`

#### Key Features

- **Expand/Collapse**: Click to expand or collapse sections
- **Multiple Sections**: Support for multiple accordion sections
- **Animation**: Smooth expand/collapse animations
- **Data Binding**: Bind to collections for dynamic sections

#### Usage

```xaml
<controls:AccordionControl>
    <controls:AccordionItem Header="Section 1">
        <!-- Content here -->
    </controls:AccordionItem>
    <controls:AccordionItem Header="Section 2">
        <!-- Content here -->
    </controls:AccordionItem>
</controls:AccordionControl>
```

### 8. ComfyWorkflow

#### Purpose
The ComfyWorkflow control displays and interacts with ComfyUI workflow graphs, allowing users to visualize and modify AI image generation workflows.

#### Location
`BerryAIGen.Toolkit/Controls/ComfyWorkflow.cs`

#### Key Features

- **Node-based UI**: Visual representation of workflow nodes
- **Node Connections**: Visual connections between nodes
- **Interactive Editing**: Drag nodes, create/remove connections
- **Workflow Import/Export**: Support for importing and exporting workflows
- **Parameter Editing**: Edit node parameters directly in the UI

#### Usage

```xaml
<controls:ComfyWorkflow Workflow="{Binding Workflow}" />
```

### 9. ProgressBar

#### Purpose
The ProgressBar control provides visual feedback for ongoing operations, such as scanning folders or generating thumbnails.

#### Location
Styled in `BerryAIGen.Toolkit/Themes/Common.xaml`

#### Key Features

- **Modern Design**: 4px height with 2px corner radius
- **Smooth Animations**: 300ms transition effects
- **Accent Color**: Matches application accent color #2196F3
- **Consistent Styling**: Works seamlessly in both light and dark themes
- **Responsive**: Automatically adjusts to container width

#### Usage

```xaml
<ProgressBar Value="{Binding Progress}" Maximum="100" />
```

#### Implementation

```xaml
<Style TargetType="ProgressBar">
    <Setter Property="Background" Value="{DynamicResource Gray2}"></Setter>
    <Setter Property="BorderBrush" Value="Transparent"></Setter>
    <Setter Property="BorderThickness" Value="0"></Setter>
    <Setter Property="Height" Value="4"></Setter>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="ProgressBar">
                <Grid x:Name="TemplateRoot">
                    <Border Background="{TemplateBinding Background}" CornerRadius="2" SnapsToDevicePixels="True"/>
                    <Border x:Name="PART_Track" CornerRadius="2"/>
                    <Border x:Name="PART_Indicator" HorizontalAlignment="Left" Background="{DynamicResource HighlightBrush}" CornerRadius="2" SnapsToDevicePixels="True">
                        <Border.Triggers>
                            <EventTrigger RoutedEvent="FrameworkElement.Loaded">
                                <BeginStoryboard>
                                    <Storyboard>
                                        <DoubleAnimation Storyboard.TargetProperty="Opacity" From="0" To="1" Duration="0:0:0.3"/>
                                    </Storyboard>
                                </BeginStoryboard>
                            </EventTrigger>
                        </Border.Triggers>
                    </Border>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 10. GitHub Icon

#### Purpose
The GitHub icon provides direct access to the project's GitHub repository from the main application interface.

#### Location
Implemented in `BerryAIGen.Toolkit/MainWindow.xaml`

#### Key Features

- **Prominent Placement**: Located in the top-right button stack
- **Optimal Size**: 24×24px for clear visibility
- **Hover Effects**: Changes color to accent color #2196F3 on hover
- **Informative Tooltip**: Displays "访问项目Github仓库" with 300ms delay
- **External Navigation**: Opens repository in new browser tab
- **Accessibility**: Keyboard accessible and screen reader compatible

#### Usage

```xaml
<Button Margin="0,0,0,0" Style="{DynamicResource BorderlessButton}" Background="Transparent" BorderBrush="Transparent" BorderThickness="0" Click="GitHubButton_OnClick">
    <fa:ImageAwesome ToolTip="访问项目Github仓库" Icon="Github" Width="24"  Foreground="{DynamicResource ForegroundBrush}" VerticalAlignment="Center" HorizontalAlignment="Center">
        <fa:ImageAwesome.Resources>
            <Style TargetType="fa:ImageAwesome">
                <Style.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter Property="Foreground" Value="{DynamicResource HighlightBrush}"></Setter>
                    </Trigger>
                </Style.Triggers>
            </Style>
        </fa:ImageAwesome.Resources>
    </fa:ImageAwesome>
</Button>
```

#### Implementation

```csharp
private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
{
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://github.com/Berry-Wahlberg/AIGenManager") { UseShellExecute = true });
}
```

## Control Architecture

### MVVM Pattern

All controls follow the MVVM (Model-View-ViewModel) pattern:
- **View**: XAML files defining the UI structure
- **ViewModel**: C# classes containing the control logic and state
- **Model**: Data models representing the underlying data

### Service Integration

Controls integrate with various services through the ServiceLocator:
- **SearchService**: For search and filtering functionality
- **ThumbnailService**: For thumbnail generation
- **DataStore**: For database operations
- **MessageService**: For displaying messages
- **FileService**: For file operations

### Theming Support

Controls support the application's theming system:
- **Light/Dark Themes**: Automatically adapt to the current theme
- **Resource Dictionary**: Use theme resources for consistent styling
- **Dynamic Theme Switching**: Support for runtime theme changes

## Usage Patterns

### 1. Data Binding

Controls use WPF data binding extensively to connect with their view models:

```xaml
<controls:ThumbnailView
    Model="{Binding ThumbnailViewModel}"
    SelectedImageEntry="{Binding SelectedImageEntry, Mode=TwoWay}" />
```

### 2. Event Handling

Controls raise events for user interactions:

```csharp
ThumbnailView.PageChangedCommand = new RelayCommand<PageChangedEventArgs>(OnPageChanged);
```

### 3. Command Binding

Controls use commands for actions, following the Command pattern:

```csharp
Model.CopyPathCommand = new RelayCommand<object>(ServiceLocator.ContextMenuService.CopyPath);
Model.CopyPromptCommand = new RelayCommand<object>(ServiceLocator.ContextMenuService.CopyPrompt);
```

## Best Practices

### 1. Control Design

- **Keep Controls Focused**: Each control should have a single responsibility
- **Follow MVVM**: Separate UI from business logic
- **Use Dependency Properties**: For binding support
- **Support Theming**: Use theme resources for styling

### 2. Performance

- **Virtualization**: Use UI virtualization for large collections
- **Lazy Loading**: Load data on demand
- **Asynchronous Operations**: Avoid blocking the UI thread
- **Efficient Data Binding**: Use lightweight data objects

### 3. Accessibility

- **Keyboard Support**: Ensure all functionality is accessible via keyboard
- **Screen Reader Support**: Use appropriate UI elements and properties
- **Visual Feedback**: Provide clear visual feedback for all actions
- **Contrast**: Ensure sufficient contrast for readability

## Conclusion

The UI controls in BerryAIGen.Toolkit form the foundation of the application's user interface, providing a rich set of components for managing and interacting with AI-generated images. By following the MVVM pattern and integrating with the application's services, these controls deliver a consistent, performant, and user-friendly experience.

Understanding the design and usage of these controls is essential for developing new features and maintaining the application. The controls are designed to be flexible and extensible, allowing for easy customization and addition of new functionality.

## Related Documentation

- [MVVM Pattern](./../ui-ux/mvvm-pattern.md)
- [Theming System](./../ui-ux/theming.md)
- [Search Service Component](./search-service.md)
- [Thumbnail Service Component](./thumbnail-service.md)
