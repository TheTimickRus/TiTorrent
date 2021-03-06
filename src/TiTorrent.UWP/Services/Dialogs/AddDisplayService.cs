using MonoTorrent.Client;
using System;
using System.Threading.Tasks;
using TiTorrent.UWP.Helpers.Extensions;
using TiTorrent.UWP.Views.Dialogs;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;

namespace TiTorrent.UWP.Services.Dialogs
{
    internal class AddDisplayService
    {
        internal async Task<TorrentManager> ShowAsync()
        {
            TorrentManager manager = null;

            await CoreApplication.GetCurrentView().Dispatcher.RunTaskAsync(
                async () =>
                {
                    var addDialog = new AddDialog();
                    if (await addDialog.ShowAsync() == ContentDialogResult.Primary)
                        manager = addDialog.Manager;
                });

            return manager;
        }
    }
}
