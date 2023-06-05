using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace LoLApp.Converters;

public class Base64ToImageSourceConverter: ByteArrayToImageSourceConverter, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string base64) return null;

        try
        {
            var bytes = System.Convert.FromBase64String(base64);

            return ConvertFrom(bytes);
        }
        catch
        {
            return ImageSource.FromFile(base64);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not ImageSource imageSource) return null;
        
        var bytes = ConvertBackTo(imageSource);

        return bytes != null ? System.Convert.ToBase64String(bytes) : null;
    }
}