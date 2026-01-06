using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BerryAIGen.Toolkit.Models;

namespace BerryAIGen.Toolkit.Converters;

public class FilenameVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        var showFilenames = (bool)value[0];
        var entryType = (EntryType)value[1];

        var visible = (entryType == EntryType.Folder) || (entryType == EntryType.File && showFilenames);

        return visible ? Visibility.Visible : Visibility.Hidden;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}






