using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows.Data;
using BerryAIGen.Toolkit.Models;

namespace BerryAIGen.Toolkit.Converters;

public class FolderStateConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return FolderState.Collapsed;

        switch ((FolderState)value)
        {
            case FolderState.Collapsed:
                return 0;
            case FolderState.Expanded:
                return 90;
        }

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}






