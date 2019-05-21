using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YoungMomsAssistant.UI.Infrastructure.Converters {
    class StringToDoubleConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
           if (value is double num) {
                return num.ToString("0.00");
           }

            return "0.00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string str) {
                if (double.TryParse(str, out double num)) {
                    return num;
                }
            }

            return 0.0;
        }
    }
}
