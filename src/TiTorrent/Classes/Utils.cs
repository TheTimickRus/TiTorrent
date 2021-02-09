using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace TiTorrent.Classes
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
                Windows.Storage.AccessCache
                    .StorageApplicationPermissions
                    .FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            }

            return folder?.Path;
        }
    }
}
