using System;
using Windows.UI.Xaml.Data;

namespace TiTorrent.Shared.Converters
{
    public class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is long size)
            {
                if (parameter is string param)
                {
                    return Utils.ConvertComputerValues(size, int.Parse(param));
                }

                return Utils.ConvertComputerValues(size);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
