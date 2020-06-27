using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestApp
{
    public class BoolToShowTriangleSwitchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Show Wheel" : "Show Triangle";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Show Wheel";
        }
    }
}
