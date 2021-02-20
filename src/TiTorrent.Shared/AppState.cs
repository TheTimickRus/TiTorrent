using MonoTorrent;
using MonoTorrent.BEncoding;
using MonoTorrent.Client;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace TiTorrent.Shared
{
    public static class AppState
    {
        public static readonly string TorrentsFolderPath = Path.Combine($"{ApplicationData.Current.LocalFolder.Path}", "State", "Torrents");

        public static readonly string DhtNodesPath = $"{ApplicationData.Current.LocalFolder.Path}\\State\\DhtNodes.dat";
        public static readonly string FastResumePath = $"{ApplicationData.Current.LocalFolder.Path}\\State\\FastResume.dat";

        public static async Task<ClientEngine> Load()
        {
            var engine = new ClientEngine();

            //var nodes = Array.Empty<byte>();
            //if (File.Exists(DhtNodesPath))
            //{
            //    nodes = await File.ReadAllBytesAsync(DhtNodesPath);
            //}

            //await engine.DhtEngine.StartAsync(nodes);

            if (File.Exists(FastResumePath))
            {
                var fastResume = BEncodedValue.Decode<BEncodedDictionary>(await File.ReadAllBytesAsync(FastResumePath));

                foreach (var file in Directory.GetFiles(TorrentsFolderPath))
                {
                    if (file.EndsWith(".torrent", StringComparison.OrdinalIgnoreCase))
                    {
                        Torrent torrent;

                        try
                        {
                            torrent = await Torrent.LoadAsync(file);
                        }
                        catch (Exception e)
                        {
                            Debug.Fail(e.StackTrace);
                            continue;
                        }

                        if (fastResume.ContainsKey(torrent.InfoHash.ToHex()))
                        {
                            var manager = new TorrentManager(torrent, "");
                            manager.LoadFastResume(
                                new FastResume((BEncodedDictionary) fastResume[torrent.InfoHash.ToHex()]));

                            await engine.Register(manager);
                        }
                    }
                }
            }

            return engine;
        }

        public static async Task Save(ClientEngine engine)
        {
            try
            {
                var fastResume = new BEncodedDictionary();
                var torrents = engine.Torrents;
                if (torrents.Count == 0)
                {
                    return;
                }
                
                await engine.StopAllAsync();

                torrents.ToList().ForEach(manager =>
                {
                    if (manager.HashChecked)
                    {
                        fastResume.Add(manager.Torrent.InfoHash.ToHex(), manager.SaveFastResume().Encode());
                    }
                });

                await File.WriteAllBytesAsync(DhtNodesPath, await engine.DhtEngine.SaveNodesAsync());
                await File.WriteAllBytesAsync(FastResumePath, fastResume.Encode());
            }
            catch (Exception ex)
            {
                Debug.Fail($"\n{ex.Message}\n{ex.StackTrace}\n");
            }
            finally
            {
                engine.Dispose();
                Thread.Sleep(2000);
            }
        }

        public static async void CopyTorrent(StorageFile torrentFile)
        {
            if (!Directory.Exists(TorrentsFolderPath))
            {
                Directory.CreateDirectory(TorrentsFolderPath);
            }

            if (File.Exists(Path.Combine(TorrentsFolderPath, Path.GetFileName(torrentFile.Path))))
            {
                return;
            }
            
            await torrentFile.CopyAsync(await StorageFolder.GetFolderFromPathAsync(TorrentsFolderPath));
        }
    }
}
