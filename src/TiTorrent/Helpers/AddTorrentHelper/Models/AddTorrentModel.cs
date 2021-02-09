using System;
using MonoTorrent;
using TiTorrent.Models;

namespace TiTorrent.Helpers.AddTorrentHelper.Models
{
    public class AddTorrentModel : Model
    {
        public Torrent InTorrent { get; set; }

        public string SavePath { get; set; }
        public bool IsSavePath { get; set; }

        public string Name { get; set; }
        public long? Size { get; set; }
        public DateTime? Date { get; set; }
        public string Hash { get; set; }
        public string Comment { get; set; }

        public void Update(Torrent torrent)
        {
            InTorrent = torrent;

            Name = torrent.Name;
            Size = torrent.Size;
            Date = torrent.CreationDate;
            Hash = torrent.InfoHash.ToHex();
            Comment = torrent.Comment;
        }
    }
}
