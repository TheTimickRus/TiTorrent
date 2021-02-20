using System;
using GalaSoft.MvvmLight;
using MonoTorrent;
using MonoTorrent.Client;

namespace TiTorrent.Dialogs.Models
{
    public class AddDialogModel : ObservableObject
    {
        public static TorrentManager Manager { get; internal set; }
        public Torrent Torrent { get; set; }
        
        public string SavePath { get; set; }
        public bool IsSavePath { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public DateTime? Date { get; set; }
        public string Hash { get; set; }
        public string Comment { get; set; }

        public void Update(Torrent torrent)
        {
            Torrent = torrent;

            Name = torrent.Name;
            Size = torrent.Size;
            Date = torrent.CreationDate;
            Hash = torrent.InfoHash.ToHex();
            Comment = torrent.Comment;
        }
    }
}
