using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BerryAIGen.Toolkit.Converters;

/// <summary>
/// Converts a boolean value to a Brush.
/// </summary>
public class BoolToBrushConverter : DependencyObject, IValueConverter
{
    /// <summary>
    /// Identifies the TrueBrush dependency property.
    /// </summary>
    public static readonly DependencyProperty TrueBrushProperty = DependencyProperty.Register(
        nameof(TrueBrush),
        typeof(Brush),
        typeof(BoolToBrushConverter),
        new PropertyMetadata(Brushes.Transparent)
    );

    /// <summary>
    /// Gets or sets the Brush to use when the value is true.
    /// </summary>
    public Brush TrueBrush
    {
        get { return (Brush)GetValue(TrueBrushProperty); }
        set { SetValue(TrueBrushProperty, value); }
    }

    /// <summary>
    /// Converts a boolean value to a Brush.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion (not used).</param>
    /// <param name="culture">The culture to use for the conversion.</param>
    /// <returns>The TrueBrush if the value is true; otherwise, a transparent Brush.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue && boolValue)
        {
            return TrueBrush;
        }
        
        return Brushes.Transparent;
    }

    /// <summary>
    /// Converts a Brush back to a boolean value.
    /// </summary>
    /// <param name="value">The Brush to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion (not used).</param>
    /// <param name="culture">The culture to use for the conversion.</param>
    /// <returns>True if the value is equal to TrueBrush; otherwise, false.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Brush valueBrush)
        {
            return valueBrush == TrueBrush;
        }
        
        return false;
    }
}
