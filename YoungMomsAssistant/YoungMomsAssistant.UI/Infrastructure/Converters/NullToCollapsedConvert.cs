using System;
using System.Globalization;
using System.Windows.Data;

namespace YoungMomsAssistant.UI.Infrastructure.Converters {
    public class NullToCollapsedConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? "Collapsed" : "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
