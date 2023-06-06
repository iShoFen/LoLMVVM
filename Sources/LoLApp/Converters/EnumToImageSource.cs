using System.Globalization;
using ViewModel.Enums;

namespace LoLApp.Converters;

public class EnumToImageSource<TEnum> : IValueConverter
    where TEnum : struct, Enum
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        => value is not TEnum enumValue ? null : enumValue.GetImage();

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}
