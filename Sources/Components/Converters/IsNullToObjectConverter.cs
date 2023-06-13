#nullable enable
using System.Globalization;

namespace Components.Converters;

public class IsNullToObjectConverter : IsNullToObjectConverter<object> { }

public class IsNullToObjectConverter<TObject>: IValueConverter
{
    public TObject? TrueObject { get; set; }
    
    public TObject? FalseObject { get; set; }

    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        => value is null ? TrueObject : FalseObject;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}
