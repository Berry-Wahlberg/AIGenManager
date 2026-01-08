using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows.Data;

namespace BerryAIGen.Toolkit.Converters;

public class IsStrValueBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value == (string)parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)parameter;
    }
}







