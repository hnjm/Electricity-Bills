using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ElectricityBills.Converter
{
    public class NameToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value1 = System.Convert.ToInt64(values[0]);
            var value2 = System.Convert.ToInt64(values[1]);

            return value1 >= value2 ? Brushes.Red : null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}