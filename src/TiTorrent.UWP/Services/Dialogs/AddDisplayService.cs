using GalaSoft.MvvmLight.Messaging;
using MonoTorrent.Client;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using TiTorrent.UWP.Views.Dialogs;

namespace TiTorrent.UWP.Services.Dialogs
{
    internal class AddDisplayService
    {
        internal async Task<TorrentManager> ShowAsync()
        {
            TorrentManager manager = null;

            var dialog = new AddDialog();

            Messenger.Default.Register<TorrentManager>(this, value => manager = value);

            if (await dialog.ShowAsync() != ContentDialogResult.Primary)
            {
                Messenger.Default.Unregister<TorrentManager>(this);
                return null;
            }

            Messenger.Default.Unregister<TorrentManager>(this);

            return manager;
        }
    }
}
