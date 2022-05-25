namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;

[ValueConversion(typeof(int), typeof(object))]
public class PositiveIntToObjectConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value == BindingOperations.DisconnectedSource)
            return string.Empty;

        int IntValue = (int)value;
        CompositeCollection CollectionOfItems = (CompositeCollection)parameter;

        return (IntValue >= 0 ? CollectionOfItems[1] : CollectionOfItems[0])!;
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return null!;
    }
}
