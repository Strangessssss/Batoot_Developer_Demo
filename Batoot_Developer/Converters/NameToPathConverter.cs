using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Batoot_Developer.Converters;

public class NameToPathConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var imageName = value as string;
        if (string.IsNullOrEmpty(imageName))
            return null;

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", $"{imageName}.png");

        return new BitmapImage(new Uri(path, UriKind.Absolute));
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}