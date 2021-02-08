using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace TiTorrent
{
    public static class Utils
    {
        public static async Task<Stream> OpenFile()
        {
            var filePick = new FileOpenPicker
            {
                FileTypeFilter =
                {
                    ".torrent"
                }
            };

            var file = await filePick.PickSingleFileAsync();
            if (file == null)
            {
                return null;
            }


            return await file.OpenStreamForReadAsync();
        }
    }
}
