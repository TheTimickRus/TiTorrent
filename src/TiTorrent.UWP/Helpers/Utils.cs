using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace TiTorrent.UWP.Helpers
{
    public static class Utils
    {
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

            CopyTorrent(file);

            return await file.OpenStreamForReadAsync();
        }

        public static async Task<string> OpenFolder()
        {
            var folderPick = new FolderPicker
            {
                FileTypeFilter = { "*" }
            };

            var folder = await folderPick.PickSingleFolderAsync();

            return folder != null
                ? folder.Path
                : string.Empty;
        }

        private static async void CopyTorrent(StorageFile torrentFile)
        {
            if (!Directory.Exists(AppState.TorrentsDirectory))
            {
                Directory.CreateDirectory(AppState.TorrentsDirectory);
            }

            if (File.Exists(Path.Combine(AppState.TorrentsDirectory, Path.GetFileName(torrentFile.Path))))
            {
                return;
            }

            await torrentFile.CopyAsync(await StorageFolder.GetFolderFromPathAsync(AppState.TorrentsDirectory));
        }
    }
}
