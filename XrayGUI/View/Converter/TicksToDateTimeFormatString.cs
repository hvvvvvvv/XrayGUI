using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XrayGUI.View.Converter
{
    internal class TicksToDateTimeFormatString : IValueConverter
    {
        public string Format { get; set; }
        public TicksToDateTimeFormatString()
        {
            Format = "yyyy-MM-dd HH:mm:ss";
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is long ticks && ticks != 0)
            {
                return new DateTime(ticks).ToString(Format);
            }
            return string.Empty;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string str && DateTime.TryParseExact(str, Format, culture, DateTimeStyles.None, out var dt))
            {
                return dt.Ticks;
            }
            return null;
        }
    }
}
