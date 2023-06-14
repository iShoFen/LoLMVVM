using System.Globalization;

namespace Components.Converters;

public class EnumToValuesConverter<TEnum> : IValueConverter
    where TEnum : struct, Enum
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        IEnumerable<TEnum> result = Enum.GetValues<TEnum>();
        if (parameter is not string excluded) return result;
        
        var names = excluded.Split(',');
        var excludedValues = names
                             .Select(name => Enum.TryParse(name, out TEnum enumValue)
                                         ? (true, enumValue)
                                         : (false, default)
                             )
                             .Where(tuple => tuple.Item1)
                             .Select(tuple => tuple.Item2)
                             .Distinct();
            
        result = result.Except(excludedValues);

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
}
