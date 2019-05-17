using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YoungMomsAssistant.UI.Infrastructure.Converters {
    public class ErrorToIsEnabledConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return true;
            }

            var error = value as string ?? throw new ArgumentException("value");

            return string.IsNullOrWhiteSpace(error);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
