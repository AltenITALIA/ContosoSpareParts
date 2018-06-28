using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpareParts.Mobile.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        private readonly ColorTypeConverter converter;

        public StringToColorConverter()
        {
            converter = new ColorTypeConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Color.Transparent;

            if (value is string input)
            {
                try
                {
                    color = (Color)converter.ConvertFromInvariantString(input);
                }
                catch
                {
                }
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
