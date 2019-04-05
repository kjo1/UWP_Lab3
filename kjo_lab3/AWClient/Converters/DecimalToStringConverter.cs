using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AWClient.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        { 
            string s = value.ToString();

            if (decimal.TryParse(s, out decimal result))
                return result;
            else
                return 0;
        }
    }
}
