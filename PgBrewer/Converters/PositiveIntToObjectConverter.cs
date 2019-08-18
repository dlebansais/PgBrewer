using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(object))]
    public class PositiveIntToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IntValue = (int)value;
            CompositeCollection CollectionOfItems = parameter as CompositeCollection;

            return IntValue >= 0 ? CollectionOfItems[1] : CollectionOfItems[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
