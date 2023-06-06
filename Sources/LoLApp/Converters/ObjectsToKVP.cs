using System.Globalization;

namespace LoLApp.Converters;

public class ObjectsToKVP<TKey, TValue>: IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length != 2) return null;

        if (values[0] is TKey key && values[1] is TValue value) return new KeyValuePair<TKey, TValue>(key, value);
        
        return KeyValuePair.Create(default(TKey), default(TValue));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (value is KeyValuePair<TKey, TValue> kvp) return new object[] { kvp.Key, kvp.Value };
        
        return new object[] { default(TKey), default(TValue) };
    }
}
