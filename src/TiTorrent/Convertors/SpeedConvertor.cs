using System;
using Windows.UI.Xaml.Data;

namespace TiTorrent
{
    public class SpeedConvertor : IValueConverter
    {
        private static string ConvertComputerValues(dynamic value)
        {
            string[] suf = { "Byte/Sec", "KB/Sec", "MB/Sec", "GB/Sec", "TB/Sec", "PB/Sec", "EB/Sec" };

            if (value == 0) return "0 " + suf[0];

            var bytes = Math.Abs(value);
            var place = System.Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return Math.Sign(value) * num + $" {suf[place]}";
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConvertComputerValues(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
