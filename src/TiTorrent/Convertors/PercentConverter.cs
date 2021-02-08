using System;
using Windows.UI.Xaml.Data;

namespace TiTorrent
{
    public class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Math.Round(System.Convert.ToDecimal(value), 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
