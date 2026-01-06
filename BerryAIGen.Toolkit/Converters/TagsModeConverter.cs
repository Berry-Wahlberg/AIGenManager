using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows.Data;
using BerryAIGen.Common.Query;

namespace BerryAIGen.Toolkit.Converters;

public class TagsModeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((TagsMode)value).ToString() == (string)parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)parameter == "AND" ? TagsMode.AND : TagsMode.OR;
    }
}






