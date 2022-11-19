using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace BotwSaveManager.Converters
{
    public class BrushConverter : IValueConverter
    {
        public static BrushConverter Instance = new();

        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string color && targetType != null && targetType.IsAssignableFrom(typeof(IBrush))) {
                return new SolidColorBrush(System.Convert.ToUInt32(color.Replace("#", "").PadLeft(8, 'F'), 16));
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
