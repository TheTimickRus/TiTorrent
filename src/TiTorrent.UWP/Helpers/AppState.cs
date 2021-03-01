using System;
using System.IO;
using Windows.Storage;

namespace TiTorrent.UWP.Helpers
{
    public static class AppState
    {
        public static readonly string LogDirectory = Path.Combine(ApplicationData.Current.LocalFolder.Path, "State", "Logs");
        public static readonly string TorrentsDirectory = Path.Combine(ApplicationData.Current.LocalFolder.Path, "State", "Torrents");

        public static void Init()
        {
            try
            {
                if (Directory.Exists(LogDirectory) is false)
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                if (Directory.Exists(TorrentsDirectory) is false)
                {
                    Directory.CreateDirectory(TorrentsDirectory);
                }
            }
            catch { /* ignore */ }
        }
    }
}
