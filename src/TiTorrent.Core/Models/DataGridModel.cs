using GalaSoft.MvvmLight;
using MonoTorrent.Client;

namespace TiTorrent.Core.Models
{
    public class DataGridModel : ObservableObject
    {
        public string Hash { get; set; }

        public string Name { get; set; }
        public string State { get; set; }
        public long TotalSize { get; set; }
        public double Progress { get; set; }
        public long DownloadedSize { get; set; }
        public long RemainingSize => TotalSize - DownloadedSize;
        public long DownloadSpeed { get; set; }
        public long UploadSpeed { get; set; }


        public DataGridModel(TorrentManager manager)
        {
            Hash = manager.InfoHash.ToHex();
            Name = manager.Torrent.Name;
            State = manager.State.ToString();
            TotalSize = manager.Size;
        }


        public void UpdateProp(TorrentManager torrentManager)
        {
            State = torrentManager.State.ToString();
            Progress = torrentManager.Progress;
            DownloadedSize = torrentManager.Monitor.DataBytesDownloaded;
            DownloadSpeed = torrentManager.Monitor.DownloadSpeed;
            UploadSpeed = torrentManager.Monitor.UploadSpeed;
        }
    }
}
