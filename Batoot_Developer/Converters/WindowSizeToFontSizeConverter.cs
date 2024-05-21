using System.Globalization;
using System.Windows.Data;

namespace Batoot_Developer.Converters;

public class WindowSizeToFontSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double windowSize)
        {
            return windowSize * 0.017;
        }
        return 10.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}