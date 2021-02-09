using MonoTorrent.Client;

namespace TiTorrent.Models
{
    public class TorrentModel : Model
    {
        public TorrentManager Manager { get; set; }

        public int Number { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public TorrentState State { get; set; }
        public double Progress { get; set; }
        public long RemainingSize { get; set; }
        public long DownloadSpeed { get; set; }
        public long UploadSpeed { get; set; }


        public TorrentModel(TorrentManager tm, int number)
        {
            Number = number;
            Update(tm);
        }


        public void Update(TorrentManager tm)
        {
            Manager = tm;

            Name = tm.Torrent.Name;
            Size = tm.Size;
            State = tm.State;
            Progress = tm.Progress;
            RemainingSize = tm.Size - tm.Monitor.DataBytesDownloaded;
            DownloadSpeed = tm.Monitor.DownloadSpeed;
            UploadSpeed = tm.Monitor.UploadSpeed;
        }
    }
}
