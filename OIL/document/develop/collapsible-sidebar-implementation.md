# Collapsible Left Sidebar Implementation

## Overview
This document describes the implementation of the collapsible left sidebar feature in the AIGenManager application. The feature allows users to expand and collapse the left sidebar, providing more screen real estate when collapsed while maintaining full functionality.

## Implementation Approach

### 1. Architecture
The implementation follows the MVVM (Model-View-ViewModel) pattern, with changes made to the following components:

- **Model**: Added `SidebarWidth` property to `MainModel` class
- **View**: Updated `MainWindow.xaml` to support dynamic sidebar width
- **ViewModel**: Enhanced `ToggleNavigationPane` command to control sidebar width

### 2. Key Changes

#### 2.1 Model Changes (`MainModel.cs`)
- Added `_sidebarWidth` private field initialized to 200px (default expanded width)
- Added public `SidebarWidth` property with `INotifyPropertyChanged` implementation
- The property controls the width of the sidebar column in the main window grid

#### 2.2 View Changes (`MainWindow.xaml`)
- Modified the sidebar column definition to use dynamic binding: `Width="{Binding SidebarWidth, Mode=TwoWay}"`
- Set minimum width to 50px and maximum width to 300px for optimal UX
- Added a toggle button at the top of the sidebar with FontAwesome icon
- Implemented responsive design for navigation buttons: text labels hide when sidebar is collapsed
- Added visual feedback: toggle button icon changes direction based on sidebar state

#### 2.3 Command Implementation (`MainWindow.xaml.cs`)
- Updated `ToggleNavigationPane` method to toggle between expanded (200px) and collapsed (50px) states
- The F3 key is already bound to this command for keyboard accessibility

### 3. Component Structure

```
┌─────────────────────────────────────────────────────────┐
│ MainWindow                                              │
│ ┌─────────────────────────────────────────────────────┐ │
│ │ Grid (Main Layout)                                  │ │
│ │ ┌──────────────────┐ ┌────────────────────────────┐ │ │
│ │ │ Sidebar Column   │ │ Main Content Column        │ │ │
│ │ │ Width={Binding    │ │ Width=*                    │ │ │
│ │ │ SidebarWidth}    │ │                            │ │ │
│ │ │                  │ │                            │ │ │
│ │ │ ┌──────────────┐ │ │                            │ │ │
│ │ │ │ Toggle Button│ │ │                            │ │ │
│ │ │ │ (Chevron Icon│ │ │                            │ │ │
│ │ │ │ Changes Based│ │ │                            │ │ │
│ │ │ │ on State)    │ │ │                            │ │ │
│ │ │ └──────────────┘ │ │                            │ │ │
│ │ │ ┌──────────────┐ │ │                            │ │ │
│ │ │ │ Section Label │ │ │                            │ │ │
│ │ │ │ (Hides when   │ │ │                            │ │ │
│ │ │ │ collapsed)    │ │ │                            │ │ │
│ │ │ └──────────────┘ │ │                            │ │ │
│ │ │ ┌──────────────┐ │ │                            │ │ │
│ │ │ │ Navigation   │ │ │                            │ │ │
│ │ │ │ Button 1     │ │ │                            │ │ │
│ │ │ │ (Icon + Text)│ │ │                            │ │ │
│ │ │ │ (Text hides  │ │ │                            │ │ │
│ │ │ │ when collapsed│ │ │                            │ │ │
│ │ │ └──────────────┘ │ │                            │ │ │
│ │ │ ┌──────────────┐ │ │                            │ │ │
│ │ │ │ Navigation   │ │ │                            │ │ │
│ │ │ │ Button 2     │ │ │                            │ │ │
│ │ │ │ (Icon + Text)│ │ │                            │ │ │
│ │ │ │ (Text hides  │ │ │                            │ │ │
│ │ │ │ when collapsed│ │ │                            │ │ │
│ │ │ └──────────────┘ │ │                            │ │ │
│ │ │                  │ │                            │ │ │
│ │ └──────────────────┘ └────────────────────────────┘ │ │
│ └─────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────┘
```

## Interaction Logic

### 1. Toggle Mechanism
- **Click Toggle Button**: User clicks the chevron icon button in the sidebar
- **Keyboard Shortcut**: User presses F3 key
- **Command Execution**: `ToggleNavigationPane` command is invoked
- **Width Update**: `_model.SidebarWidth` is toggled between 200px and 50px
- **UI Update**: The UI automatically updates due to data binding

### 2. Responsive Behavior

| Sidebar State | Width | Section Label | Navigation Buttons | Toggle Icon |
|---------------|-------|---------------|--------------------|-------------|
| Expanded      | 200px | Visible       | Icon + Text        | ChevronLeft |
| Collapsed     | 50px  | Hidden        | Icon Only          | ChevronRight|

### 3. Layout Adjustments
- When sidebar collapses, main content area automatically expands to fill available space
- All navigation functionality is preserved in collapsed state
- Tooltips remain accessible for all navigation icons

## Usage Instructions

### 1. Basic Usage
1. **Expand/Collapse Sidebar**: Click the chevron icon at the top of the left sidebar
2. **Keyboard Shortcut**: Press F3 to toggle the sidebar state
3. **Navigation**: Click on navigation icons (or text labels when expanded) to navigate between sections

### 2. Screen Resolution Considerations
- The sidebar adapts to different screen sizes while maintaining usability
- Minimum width of 50px ensures icons remain accessible on small screens
- Maximum width of 300px prevents the sidebar from dominating large screens

### 3. Best Practices
- Use collapsed state when working with large images or requiring maximum content space
- Use expanded state when first learning the application or needing text labels for navigation
- Keyboard users can quickly toggle between states using the F3 shortcut

## Code Snippets

### 1. Model Property Implementation
```csharp
private double _sidebarWidth = 200;

public double SidebarWidth
{
    get => _sidebarWidth;
    set => SetField(ref _sidebarWidth, value);
}
```

### 2. Toggle Command Implementation
```csharp
private void ToggleNavigationPane()
{
    _model.SidebarWidth = _model.SidebarWidth == 200 ? 50 : 200;
}
```

### 3. Dynamic Column Definition
```xaml
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="{Binding SidebarWidth, Mode=TwoWay}" MinWidth="50" MaxWidth="300"/>
    <ColumnDefinition Width="*"/>
</Grid.ColumnDefinitions>
```

### 4. Toggle Button with Dynamic Icon
```xaml
<Button Margin="0,5,0,10" Height="32" HorizontalAlignment="Right" Width="32"
        Style="{StaticResource BorderlessToolbarButton}" Command="{Binding ToggleNavigationPane}">
    <fa:ImageAwesome Icon="ChevronLeft" Width="16" Foreground="{DynamicResource ForegroundBrush}" VerticalAlignment="Center">
        <fa:ImageAwesome.Style>
            <Style TargetType="fa:ImageAwesome">
                <Style.Triggers>
                    <DataTrigger Binding="{Binding SidebarWidth}" Value="50">
                        <Setter Property="Icon" Value="ChevronRight"/>
                    </DataTrigger>
                </Style.Triggers>
            </Style>
        </fa:ImageAwesome.Style>
    </fa:ImageAwesome>
</Button>
```

### 5. Responsive Navigation Button
```xaml
<Button Margin="0,5,0,10" Height="32" HorizontalAlignment="Stretch" 
        behaviors:DTBehaviors.IsSelected="{Binding ActiveView, Converter={StaticResource StringMatchConverter}, ConverterParameter=Folders}"
        Style="{StaticResource BorderlessToolbarButton}" Command="{Binding GotoUrl}" CommandParameter="search/#folders">
    <StackPanel Orientation="Horizontal" HorizontalAlignment="Left" Width="Auto">
        <fa:ImageAwesome ToolTip="Folders (Ctrl+1)" Icon="FolderOutlinepenOutline" Width="16" Foreground="{DynamicResource ForegroundBrush}" VerticalAlignment="Center" Margin="0,0,10,0">
        </fa:ImageAwesome>
        <Label Content="{lex:Loc Navigation.Item.Folders}" Foreground="{DynamicResource ForegroundBrush}" VerticalAlignment="Center" HorizontalAlignment="Left">
            <Label.Style>
                <Style TargetType="Label">
                    <Setter Property="Visibility" Value="Visible"/>
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding SidebarWidth}" Value="50">
                            <Setter Property="Visibility" Value="Collapsed"/>
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </Label.Style>
        </Label>
    </StackPanel>
</Button>
```

## Visual References

### Expanded State
- Sidebar width: 200px
- All navigation items with text labels visible
- Toggle icon: ChevronLeft

### Collapsed State
- Sidebar width: 50px
- Only navigation icons visible
- Toggle icon: ChevronRight
- Main content area expanded to fill available space

## Testing

### 1. Manual Testing
- Verify toggle button functionality (click to expand/collapse)
- Test keyboard shortcut (F3) functionality
- Check that all navigation buttons work in both states
- Verify tooltip visibility in collapsed state
- Test responsiveness across different window sizes

### 2. Expected Behavior
- Sidebar should smoothly transition between expanded and collapsed states
- No visual glitches or layout issues during transitions
- All functionality should remain accessible in both states
- UI elements should properly adjust their positions and sizes

## Benefits

1. **Improved User Experience**: Users can customize their workspace based on their needs
2. **Increased Productivity**: More screen real estate available when needed
3. **Enhanced Accessibility**: Keyboard shortcut support and tooltips
4. **Modern UI Design**: Follows contemporary design trends with collapsible panels
5. **Maintained Functionality**: All navigation features remain accessible in collapsed state

## Future Enhancements

1. **Remember State**: Persist sidebar state between application sessions
2. **Custom Width**: Allow users to resize the sidebar to their preferred width
3. **Animation**: Add smooth transition animation when expanding/collapsing
4. **Auto-Collapse**: Option to automatically collapse sidebar on small screens

## Conclusion

The collapsible left sidebar implementation provides users with greater flexibility in managing their workspace while maintaining a clean, modern UI design. The implementation follows best practices and integrates seamlessly with the existing MVVM architecture, ensuring maintainability and scalability for future enhancements.