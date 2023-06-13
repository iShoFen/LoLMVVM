using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace Components.Converters;

public class Base64ToImageSourceConverter: ByteArrayToImageSourceConverter, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string base64 || string.IsNullOrWhiteSpace(base64))
            if (parameter is not string parameterString || string.IsNullOrWhiteSpace(parameterString)) return null;
            else base64 = parameterString;
        
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