using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace TiTorrent.Shared
{
    public static class Utils
    {
        public static string ConvertComputerValues(dynamic value, int param = 0)
        {
            string[,] strings =
            {
                {"Byte", "KB", "MB", "GB", "TB", "PB", "EB"},
                {"Byte/s", "KB/s", "MB/s", "GB/s", "TB/s", "PB/s", "EB/s"}
            };

            if (value == 0)
            {
                return $"0 {strings[param, 0]}";
            }

            var bytes = Math.Abs(value);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return $"{Math.Sign(value) * num} {strings[param, place]}";
        }

        public static async Task<Stream> OpenFile()
        {
            var filePick = new FileOpenPicker
            {
                FileTypeFilter =
                {
                    ".torrent", "*"
                }
            };

            var file = await filePick.PickSingleFileAsync();
            if (file == null)
            {
                return null;
            }

            AppState.CopyTorrent(file);

            return await file.OpenStreamForReadAsync();
        }

        public static async Task<string> OpenFolder()
        {
            var folderPick = new FolderPicker
            {
                FileTypeFilter = { "*" }
            };

            var folder = await folderPick.PickSingleFolderAsync();

            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                return folder.Path;
            }

            return string.Empty;
        }
    }
}
