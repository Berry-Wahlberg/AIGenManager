using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BerryAIGen.Toolkit.Common;

namespace BerryAIGen.Toolkit.Converters;

public class FolderModeVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (ViewMode)value == ViewMode.Folder ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}







