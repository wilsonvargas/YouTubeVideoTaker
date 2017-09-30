using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubeVideoTaker.Converters
{
    public class FileTotalSizeToStringConverter : IValueConverter
    {
        private static readonly string[] Units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            double size = (long)value;
            int unit = 0;

            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return $" of {size:0.#} {Units[unit]}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
