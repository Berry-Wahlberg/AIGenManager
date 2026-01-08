# Development Log

## 2026-01-07

### Fixed Runtime Error: "#FFE0E0E0' is not a valid value for property 'Background'"

**Root Cause**: The ProgressBar style in `BerryAIGen.Toolkit/Themes/Common.xaml` was directly using the `Gray2` Color resource for the Background property, which expects a Brush resource. This caused a runtime XAML ParseException because WPF couldn't convert the Color value to a Brush correctly.

**Fix**: Updated the ProgressBar style to use the `ControlBackgroundBrush` resource instead, which is a SolidColorBrush that correctly references the ControlBackground Color resource:

```xaml
<!-- Before -->
<Style TargetType="ProgressBar">
    <Setter Property="Background" Value="{DynamicResource Gray2}"></Setter>
    <!-- Other properties -->
</Style>

<!-- After -->
<Style TargetType="ProgressBar">
    <Setter Property="Background" Value="{DynamicResource ControlBackgroundBrush}"></Setter>
    <!-- Other properties -->
</Style>
```

**Verification**: 
- Built the project successfully with no compilation errors
- Ran the application without runtime errors
- Confirmed the main program launches correctly

**Lesson Learned**: In WPF, always use Brush resources (not Color resources) for properties like Background, BorderBrush, Foreground, etc. While WPF can sometimes implicitly convert Color to Brush, it's not reliable and can cause runtime errors, especially with DynamicResource usage.