using System;
using System.IO;
using Windows.Storage;

namespace TiTorrent.Shared
{
    public static class AppState
    {
        public static readonly string TorrentsFolderPath = Path.Combine($"{ApplicationData.Current.LocalFolder.Path}", "State", "Torrents");
    }
}
