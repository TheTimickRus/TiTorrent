using ByteSizeLib;
using GalaSoft.MvvmLight;
using MonoTorrent.Client;

namespace TiTorrent.UWP.Models
{
    public class ListViewItemModel : ObservableObject
    {
        public static TorrentManager Manager { get; set; }


        public string Hash { get; set; }

        public string Name { get; set; }
        public string State { get; set; }
        public ByteSize TotalSize { get; set; }
        public double Progress { get; set; }
        public ByteSize DownloadedSize { get; set; }
        public ByteSize RemainingSize => TotalSize - DownloadedSize;
        public ByteSize DownloadSpeed { get; set; }
        public ByteSize UploadSpeed { get; set; }


        public ListViewItemModel(TorrentManager manager)
        {
            Manager = manager;

            Hash = manager.InfoHash.ToHex();
            Name = manager.Torrent.Name;
            State = manager.State.ToString();
            TotalSize = ByteSize.FromBytes(manager.Torrent.Size);
        }


        public void UpdateProp(TorrentManager manager)
        {
            Manager = manager;

            State = manager.State.ToString();
            Progress = manager.Progress;
            DownloadedSize = ByteSize.FromBytes(manager.Monitor.DataBytesDownloaded);
            DownloadSpeed = ByteSize.FromBytes(manager.Monitor.DownloadSpeed);
            UploadSpeed = ByteSize.FromBytes(manager.Monitor.UploadSpeed);
        }
    }
}
