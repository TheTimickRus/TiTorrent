using System;
using System.IO;
using Windows.UI.Xaml.Data;

namespace TiTorrent.Shared.Converters
{
    public class FullPathToFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string fullPath)
            {
                return Path.GetFileName(fullPath);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
