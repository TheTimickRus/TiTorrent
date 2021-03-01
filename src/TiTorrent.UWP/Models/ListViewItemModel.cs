using ByteSizeLib;
using GalaSoft.MvvmLight;
using MonoTorrent.Client;

namespace TiTorrent.UWP.Models
{
    public class ListViewItemModel : ObservableObject
    {
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
            Hash = manager.InfoHash.ToHex();
            Name = manager.Torrent.Name;
            State = manager.State.ToString();
            TotalSize = ByteSize.FromBytes(manager.Size);
        }


        public void UpdateProp(TorrentManager torrentManager)
        {
            State = torrentManager.State.ToString();
            Progress = torrentManager.Progress;
            DownloadedSize = ByteSize.FromBytes(torrentManager.Monitor.DataBytesDownloaded);
            DownloadSpeed = ByteSize.FromBytes(torrentManager.Monitor.DownloadSpeed);
            UploadSpeed = ByteSize.FromBytes(torrentManager.Monitor.UploadSpeed);
        }
    }
}
