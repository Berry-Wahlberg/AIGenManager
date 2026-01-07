# User Interactions

## Overview

The BerryAIGen.Toolkit application is designed with intuitive user interactions in mind, supporting multiple input methods including mouse, keyboard, and touch. This document describes the interaction patterns, keyboard shortcuts, and accessibility features implemented in the application.

## Interaction Design Principles

### 1. Consistency

- Consistent interaction patterns across all screens and components
- Standard Windows UI conventions for familiarity
- Predictable behavior for common operations

### 2. Efficiency

- Keyboard shortcuts for frequent actions
- Context menus for quick access to common commands
- Drag-and-drop support for intuitive file operations

### 3. Discoverability

- Clear visual cues for interactive elements
- Tooltips for additional information
- Helpful error messages and guidance

### 4. Accessibility

- Support for screen readers
- Keyboard navigation for all features
- High contrast themes
- Resizable UI elements

## Mouse Interactions

### 1. Basic Operations

| Interaction | Action |
|-------------|--------|
| Single click | Select item |
| Double click | Open item (view image in full size) |
| Right click | Open context menu |
| Drag | Select multiple items or initiate drag-and-drop |
| Scroll wheel | Navigate through lists and thumbnails |
| Ctrl + scroll | Zoom in/out of images |
| Shift + click | Select range of items |

### 2. Thumbnail View Interactions

- **Single click**: Select a thumbnail
- **Double click**: Open image in full-size viewer
- **Right click**: Open context menu with image-specific commands
- **Drag**: Select multiple thumbnails
- **Drag-and-drop**: Move images to folders or collections

### 3. Context Menus

Context menus provide quick access to common operations:

#### Image Context Menu
- Open with external viewer
- Copy file path
- Show in Explorer
- Edit metadata
- Add to collection
- Delete
- Rate image

#### Folder Context Menu
- Scan folder
- Rescan folder
- Add to watch list
- Remove from watch list

## Keyboard Shortcuts

### 1. Application Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl + O` | Open file/folder |
| `Ctrl + S` | Save changes |
| `Ctrl + Q` | Exit application |
| `Ctrl + N` | New collection |
| `Ctrl + F` | Focus search bar |
| `F1` | Open help |
| `F5` | Refresh view |
| `Ctrl + R` | Rescan selected folder |
| `Ctrl + Shift + R` | Rescan all folders |

### 2. Thumbnail View Shortcuts

| Shortcut | Action |
|----------|--------|
| `Arrow keys` | Navigate between thumbnails |
| `Enter` | Open selected image |
| `Delete` | Delete selected images |
| `Ctrl + A` | Select all images |
| `Ctrl + D` | Deselect all images |
| `Ctrl + C` | Copy selected images |
| `Ctrl + V` | Paste images |
| `Ctrl + X` | Cut selected images |
| `Ctrl + Z` | Undo last action |
| `Ctrl + Y` | Redo last action |

### 3. Image Viewer Shortcuts

| Shortcut | Action |
|----------|--------|
| `Arrow keys` | Navigate between images |
| `Escape` | Close viewer |
| `+` / `-` | Zoom in/out |
| `0` | Reset zoom |
| `F` | Toggle fullscreen |
| `Ctrl + M` | Toggle metadata panel |
| `Ctrl + R` | Rotate image |

### 4. Search Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl + F` | Focus search bar |
| `Enter` | Execute search |
| `Esc` | Clear search |
| `Ctrl + L` | Clear all filters |

## Touch Interactions

### 1. Basic Touch Gestures

| Gesture | Action |
|---------|--------|
| Tap | Select item |
| Double tap | Open item |
| Long press | Open context menu |
| Swipe | Navigate between items |
| Pinch | Zoom in/out |
| Pan | Scroll through content |

### 2. Touch-optimized Controls

- Large, easy-to-tap buttons
- Swipe-friendly navigation
- Touch gestures for common operations
- Responsive UI that adapts to touch input

## Drag-and-Drop Support

### 1. Supported Operations

- **Drag from Explorer to Thumbnail View**: Import images
- **Drag between folders in sidebar**: Move images
- **Drag from Thumbnail View to Explorer**: Export images
- **Drag from Thumbnail View to Collection**: Add to collection

### 2. Drag States

- **Hover**: Visual feedback when dragging over a drop target
- **Accept**: Green highlight for valid drop targets
- **Reject**: Red highlight for invalid drop targets
- **Feedback**: Progress indicators for large transfers

## Navigation Patterns

### 1. Sidebar Navigation

- **Folders**: Browse filesystem folders
- **Collections**: Access user-created collections
- **Recent**: View recently accessed images
- **Favorites**: Access favorite images

### 2. Breadcrumb Navigation

- Shows current location in the folder hierarchy
- Clickable segments to navigate up the hierarchy
- Supports keyboard navigation with arrow keys

### 3. Tab Navigation

- Different views available as tabs
- Keyboard shortcuts to switch between tabs
- Visual indication of active tab

## Command Pattern Implementation

The application uses the Command Pattern for handling user interactions, with two main command types:

### 1. RelayCommand

```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;
    
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    // ICommand implementation
}
```

### 2. AsyncCommand

```csharp
public class AsyncCommand : ICommand
{
    private readonly Func<object, Task> _execute;
    private readonly Func<object, bool> _canExecute;
    
    public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    // ICommand implementation
}
```

## Accessibility Features

### 1. Screen Reader Support

- All UI elements have accessible names
- Descriptive tooltips for non-obvious controls
- ARIA labels for custom controls
- Keyboard navigation for all features

### 2. Keyboard Accessibility

- Full keyboard navigation support
- Focus indicators for all interactive elements
- Keyboard shortcuts for frequent actions
- Tab order optimization

### 3. Visual Accessibility

- High contrast themes
- Adjustable font sizes
- Resizable UI elements
- Clear visual hierarchy
- Consistent color schemes

### 4. Cognitive Accessibility

- Simple, intuitive interface
- Consistent terminology
- Helpful error messages
- Clear feedback for all actions

## Interaction Feedback

### 1. Visual Feedback

- Hover effects on buttons and links
- Selection indicators for active elements
- Loading spinners for long-running operations
- Progress bars for batch operations
- Toast notifications for background tasks

### 2. Audio Feedback

- Optional audio cues for important actions
- Error sounds for invalid operations
- Success sounds for completed tasks

### 3. Confirmation Dialogs

- Confirmation before destructive actions
- Option to disable confirmations for certain actions
- Clear explanation of consequences

## Undo/Redo System

### 1. Supported Operations

- Image rating changes
- Metadata edits
- Collection modifications
- Folder organization changes

### 2. Implementation

```csharp
public class UndoManager
{
    private readonly Stack<IUndoableAction> _undoStack;
    private readonly Stack<IUndoableAction> _redoStack;
    
    public void ExecuteAction(IUndoableAction action)
    {
        // Execute action and add to undo stack
    }
    
    public void Undo()
    {
        // Undo last action
    }
    
    public void Redo()
    {
        // Redo last undone action
    }
}
```

## Multi-Select Operations

### 1. Selection Modes

- **Single selection**: Click on an item
- **Multiple selection**: 
  - Drag to select multiple items
  - Ctrl + click to select/deselect individual items
  - Shift + click to select a range
  - Ctrl + A to select all

### 2. Batch Operations

- Delete multiple images
- Add multiple images to a collection
- Rate multiple images
- Edit metadata for multiple images
- Export multiple images

## Search Interactions

### 1. Real-time Search

- Search results update as you type
- Predictive suggestions for search terms
- Auto-completion for known values (models, keywords, etc.)

### 2. Advanced Search

- Filter panel with multiple criteria
- AND/OR logic support
- Saved searches for frequent queries
- Search history for recent searches

## Conclusion

The user interaction design in BerryAIGen.Toolkit prioritizes consistency, efficiency, and accessibility. By supporting multiple input methods (mouse, keyboard, touch) and implementing intuitive interaction patterns, the application provides a seamless user experience for managing AI-generated images. The keyboard shortcuts, context menus, and accessibility features ensure that the application is usable by a wide range of users, including those with disabilities. The undo/redo system and batch operations further enhance productivity, allowing users to efficiently manage large image collections.
