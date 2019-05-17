using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YoungMomsAssistant.UI.Infrastructure.Converters {
    public class BytesToImageConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var bytes = value as byte[] ?? throw new ArgumentException("value");

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();

            return bitmap as ImageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
