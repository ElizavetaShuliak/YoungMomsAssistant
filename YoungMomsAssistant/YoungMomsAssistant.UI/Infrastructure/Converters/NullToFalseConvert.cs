using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YoungMomsAssistant.UI.Infrastructure.Converters {
    public class NullToFalseConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
