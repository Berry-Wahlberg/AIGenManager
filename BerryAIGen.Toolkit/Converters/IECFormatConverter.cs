using System;
using BerryAIGen.Common;
using System.Globalization;
using System.Windows.Data;
using BerryAIGen.Toolkit.Common;

namespace BerryAIGen.Toolkit.Converters;

public class IECFormatConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return "";

        return FileUtility.ToIECPrefix((long)value).FormattedSize;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}







