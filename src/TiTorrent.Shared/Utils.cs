using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace TiTorrent.Shared
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

            StorageApplicationPermissions.FutureAccessList.Add(file);

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

            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.Add(folder);
                return folder.Path;
            }

            return string.Empty;
        }

        public static async void CopyTorrent(StorageFile torrentFile)
        {
            if (!Directory.Exists(AppState.TorrentsFolderPath))
            {
                Directory.CreateDirectory(AppState.TorrentsFolderPath);
            }

            if (File.Exists(Path.Combine(AppState.TorrentsFolderPath, Path.GetFileName(torrentFile.Path))))
            {
                return;
            }

            await torrentFile.CopyAsync(await StorageFolder.GetFolderFromPathAsync(AppState.TorrentsFolderPath));
        }
    }
}
